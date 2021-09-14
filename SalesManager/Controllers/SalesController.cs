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
            x.Items.ItemName,
            x.ItemsID,
            x.Quantity,
            x.SalesID
        }).ToListAsync();

        [HttpGet]
        public async Task<IActionResult> SalesByDate(DateTime start, DateTime end)
        {
            var recs = await db.Payments.Where(x => x.DatePaid.Date >= start.Date && x.DatePaid.Date <= end.Date).Include(x => x.Sales).Select(v => new ReceiptVm
            {
                Cash = v.Cash,
                MobileMoney = v.MobileMoney,
                Receipt = v.Receipt,
                DatePaid = v.DatePaid.Date
            }).ToListAsync();
            if (recs.Count < 1)
                return NotFound(new { Message = "No transactions were found the dates provided" });
            recs.ForEach(x =>
            {
                x.Sales = db.Sales.Where(t => t.Receipt == x.Receipt).Select(t => new SalesVm { ItemsID = t.ItemsID, Quantity = t.Quantity, ItemName = t.Items.ItemName, Cost = t.Cost }).ToList();
            });
            return Ok(recs.OrderByDescending(x => x.DatePaid));
        }

        [HttpGet]
        public async Task<IActionResult> Receipt(string id)
        {
            var rec = await db.Payments.Where(x => x.Receipt == id).Select(x => new
            {
                x.Telephone,
                x.MobileMoney,
                x.Total,
                x.CanContact,
                x.Customer,
                x.DatePaid,
                x.Cash,
                Sales = x.Sales.Select(t => new { t.Quantity, t.Items.ItemName, t.Cost })
            }).SingleOrDefaultAsync();
            return rec is null ? NotFound(new { Message = "Receipt not found" }) : Ok(rec);
        }

        [HttpGet]
        public async Task<IEnumerable> Balances() => await db.Items.Select(x => new { x.ItemName, x.ItemsID, x.Group, Quantity = x.Stockings.Sum(t => t.Quantity) }).ToListAsync();

        [HttpGet]
        public async Task<IEnumerable> Recent() => await db.Sales.OrderByDescending(x => x.DateAdded).Take(20).Select(x => new
        {
            x.Items.ItemName,
            x.ItemsID,
            x.Cost,
            x.Quantity,
            x.Payments.Customer
        }).ToListAsync();

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
                Total = tran.MobileMoney + tran.Cash,
                Sales = new List<Sales>()
            };
            tran.Sales.ForEach(x =>
            {
                var price = db.Prices.Where(x => x.ItemsID == x.ItemsID).OrderByDescending(x => x.DateSet).FirstOrDefault();
                payments.Sales.Add(new Sales
                {
                    DateAdded = payments.DatePaid,
                    ItemsID = x.ItemsID,
                    Quantity = x.Quantity,
                    Cost = x.Quantity * price.Price,
                    Receipt = payments.Receipt,
                    UserName = User.Identity.Name,
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
}
