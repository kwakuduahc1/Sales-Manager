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
        public async Task<IEnumerable> SalesByDate(DateTime start, DateTime end)
        {
            return await db.Sales.Where(x => x.DateAdded.Date >= start.Date && x.DateAdded <= end.Date).GroupBy(x => new { x.DateAdded.Date, x.ItemsID, x.Items.ItemName }, (k, v) => new
            {
                k.Date,
                k.ItemName,
                k.ItemsID,
                Quantity = v.Sum(b => b.Quantity),
                Cost = v.Sum(b => b.Cost)
            }).ToListAsync();
        }

        [HttpGet]
    }

    public async Task<IEnumerable> Receipt(string id) => await db.Sales.Where(x => x.Receipt == id).Select(x => new
    {
        x.Quantity,
        x.DateAdded,
        x.Customer,
        x.Cost,
        x.ItemsID,
        x.Items.ItemName,
        x.SalesID
    }).ToListAsync();

    [HttpGet]
    public async Task<IEnumerable> Balances() => await db.Items.Select(x => new { x.ItemName, x.ItemsID, x.Group, Quantity = x.Stockings.Sum(t => t.Quantity) }).ToListAsync();

    [HttpGet]
    public async Task<IEnumerable> Recent() => await db.Sales.OrderByDescending(x => x.DateAdded).Take(20).Select(x => new
    {
        x.Items.ItemName,
        x.ItemsID,
        x.Cost,
        x.Quantity,
        x.Customer
    }).ToListAsync();

    [HttpGet]
    public async Task<IActionResult> Find(int id)
    {
        var item = await db.Stockings.SingleOrDefaultAsync(x => x.StockingsID == id);
        return item == null ? NotFound(new { Message = "Cannot find the item" }) : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] List<Sales> sales)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
        sales.ForEach(x =>
        {
            var price = db.Prices.Where(x => x.ItemsID == x.ItemsID).OrderByDescending(x => x.DateSet).FirstOrDefault();
            x.DateAdded = DateTime.UtcNow;
            x.UserName = User.Identity.Name ?? "";
            x.Cost = x.Quantity * price.Price;
            x.Receipt = PaymentsHelper.RandomString(new Random().Next(6, 8));
            db.Add(x);
        });
        await db.SaveChangesAsync();
        return Created($"/Find?id={sales[0].Receipt}", sales.FirstOrDefault());
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] Stockings stock)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
        var _stock = await db.Stockings.FindAsync(stock.ItemsID);
        if (_stock == null)
            return BadRequest(new { Message = "Transaction does not exist" });
        _stock.Quantity = stock.Quantity;
        db.Entry(_stock).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return Accepted(stock);
    }

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
