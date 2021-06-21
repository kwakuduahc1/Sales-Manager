using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesManager.Model;
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManager.Controllers
{
    [EnableCors("bStudioApps")]
    [Authorize(Roles = "Power, Stocker")]
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext db;

        public StocksController(DbContextOptions<ApplicationDbContext> dbContext) => db = new ApplicationDbContext(dbContext);

        [HttpGet]
        public async Task<IEnumerable> List(byte num) => await db.Stockings.OrderByDescending(x => x.DateAdded).Take(num).Select(x => new
        {
            x.DateAdded,
            x.Concurrency,
            x.Items.ItemName,
            x.ItemsID,
            x.Quantity,
            x.StockingsID
        }).ToListAsync();


        [HttpGet]
        public async Task<IEnumerable> Ledger(int id, DateTime start)
        {
            string qry = $"[dbo].[spLedger]";
            using var ddb = db.Database.GetDbConnection();
            var results = await ddb.QueryAsync<Ledger>(qry, new { item = id, start }, commandType: CommandType.StoredProcedure);
            return results;
        }

        [HttpGet]
        public async Task<IEnumerable> Balances() => await db.Items.Select(x => new { x.ItemName, x.ItemsID, x.Group, Quantity = x.Stockings.Sum(t => t.Quantity) }).ToListAsync();


        [HttpGet]
        public async Task<IActionResult> Find(int id)
        {
            var item = await db.Stockings.SingleOrDefaultAsync(x => x.StockingsID == id);
            return item == null ? NotFound(new { Message = "Cannot find the item" }) : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Stockings stock)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            stock.DateAdded = DateTime.UtcNow;
            stock.UserName = User.Identity.Name;
            db.Add(stock);
            await db.SaveChangesAsync();
            return Created($"/Find?id={stock.ItemsID}", stock);
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
