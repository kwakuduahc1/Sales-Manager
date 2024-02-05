using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesManager.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManager.Controllers
{
    [EnableCors("bStudioApps")]
    //[Authorize(Roles = "Power, Sales")]
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext db;

        public SalesController(DbContextOptions<ApplicationDbContext> dbContext) => db = new ApplicationDbContext(dbContext);

        [HttpGet]
        public async Task<IEnumerable> List(byte num) => await db.Sales.OrderByDescending(x => x.DateAdded).Take(num).Select(x => new
        {
            x.DateAdded,
            x.Concurrency,
            //x.Prices.ItemName,
            x.PricesID,
            x.Quantity,
            x.SalesID
        }).ToListAsync();

        /*
         * 
         * const string qry = "spSalesLedger";
            return await db.Database.GetDbConnection().QueryAsync<SalesLedgerVm>(qry, param: new { start, end }, commandType: CommandType.StoredProcedure);

        */

        [HttpGet]
        public async Task<IEnumerable> SalesByDate(DateTime start, DateTime end)
        {
            const string qry = "spSalesLedger";
            var dset = await db.Database.GetDbConnection().QueryAsync<SalesLedgerVm>(qry, param: new { start = start.Date, end = end.Date }, commandType: CommandType.StoredProcedure);
            var recs = dset.GroupBy(v => new
            {
                v.Total,
                v.Receipt,
                DatePaid = v.DatePaid.Date,
                v.Customer,
                v.Telephone
            }, (k, v) => new ReceiptVm
            {
                Telephone = k.Telephone,
                Customer = k.Customer,
                Receipt = k.Receipt,
                Total = k.Total,
                DatePaid = k.DatePaid,
                Sales = []
            }).ToList();
            if (recs.Count < 1)
                return null; // NotFound(new { Message = "No transactions were found the dates provided" });
            recs.ForEach(x =>
            {
                x.Sales = dset.Where(t => t.Receipt == x.Receipt).Select(t => new SalesVm { ItemsID = t.PricesID, Quantity = t.Quantity, ItemName = t.ItemName, Cost = t.Cost }).ToList();
            });
            return recs;
        }

        [HttpGet]
        public async Task<IActionResult> Receipt(string id)
        {
            const string qry = "[dbo].[spReceipts]";
            var res = await db.Database.GetDbConnection().QueryAsync<PaymentVm>(qry, param: new { receipt = id });
            if (res is null)
                return NotFound(new { Message = "Receipt not found" });
            var pay = res.GroupBy(x => new
            {
                x.Customer,
                x.Receipt,
                x.SalesType,
                x.Telephone

            }, (k, v) => new CustomerVm
            {
                Telephone = k.Telephone,
                Receipt = k.Receipt,
                Customer = k.Customer,
                SalesType = k.SalesType,
                Total = v.Where(o => o.Receipt == k.Receipt).Sum(o => o.Cost),
                Sales = v.Where(o => o.Receipt == k.Receipt).Select(o => new TransactionVm
                {
                    Cost = o.Cost,
                    ItemName = o.ItemName,
                    Quantity = o.Quantity
                }).ToList()
            }).FirstOrDefault();
            return Ok(pay);
        }

        [HttpGet]
        public async Task<IEnumerable> Balances() => await db.Items.Select(x => new { x.ItemName, x.ItemsID, x.Group, Quantity = x.Stockings.Sum(t => t.Quantity) }).ToListAsync();


        [HttpGet]
        public async Task<IEnumerable> Recent()
        {
            const string qry = @"SELECT TOP (10) Receipt, Cash + MobileMoney AS Cost, SalesType, DatePaid, Customer
                                FROM   Payments
                                ORDER BY DatePaid DESC";

            return await db.Database.GetDbConnection().QueryAsync<RecentSalesVm>(qry);
        }

        [HttpGet]
        public async Task<IActionResult> Find(int id)
        {
            var item = await db.Stockings.SingleOrDefaultAsync(x => x.StockingsID == id);
            return item == null ? NotFound(new { Message = "Cannot find the item" }) : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReceiptVm tran)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            var payments = new Payments
            {
                Receipt = PaymentsHelper.RandomString(new Random().Next(6, 8)),
                DatePaid = DateTime.UtcNow,
                Cash = tran.Cash,
                MobileMoney = tran.MobileMoney,
                CanContact = tran.CanContact,
                Customer = tran.Customer,
                Telephone = tran.Telephone,
                SalesType = tran.SalesType,
                Total = tran.MobileMoney + tran.Cash,
                Sales = new List<Sales>()
            };
            tran.Sales.ForEach(x =>
            {
                //var price = db.Prices.Find(x.PricesID);
                payments.Sales.Add(new Sales
                {
                    DateAdded = payments.DatePaid,
                    ItemsID = x.ItemsID,
                    PricesID = x.PricesID,
                    Quantity = x.Quantity,
                    Cost = x.Cost,
                    Receipt = payments.Receipt,
                    UserName = User.Identity.Name
                });
            });
            db.Add(payments);
            await db.SaveChangesAsync();
            return Created($"/Find?id={payments.Receipt}", new { payments.Receipt, payments.Total });
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit([FromBody] Stockings stock)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
        //    var _stock = await db.Stockings.FindAsync(stock.ItemsID);
        //    if (_stock == null)
        //        return BadRequest(new { Message = "Transaction does not exist" });
        //    _stock.Quantity = stock.Quantity;
        //    db.Entry(_stock).State = EntityState.Modified;
        //    await db.SaveChangesAsync();
        //    return Accepted(stock);
        //}

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] Stockings stock)
        {
            var _item = await db.Stockings.FindAsync(stock.StockingsID);
            if (_item == null)
                return BadRequest(new { Message = "Transaction does not exist" });
            db.Entry(_item).State = EntityState.Deleted;
            await db.SaveChangesAsync();
            return Accepted();
        }
    }

    public class PaymentVm
    {
        public string Customer { get; set; }

        public string Telephone { get; set; }

        public string Receipt { get; set; }

        public decimal Cost { get; set; }

        public string ItemName { get; set; }

        public string SalesType { get; set; }

        public int Quantity { get; set; }
    }

    public class CustomerVm
    {
        public string Customer { get; set; }

        public string Telephone { get; set; }

        public string Receipt { get; set; }

        public decimal Total { get; set; }

        public string SalesType { get; set; }

        public List<TransactionVm> Sales { get; set; }
    }

    public class TransactionVm
    {
        public decimal Cost { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }
    }

}
