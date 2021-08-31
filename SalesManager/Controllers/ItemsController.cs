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

namespace SalesManager.Areas.Stores.Controllers
{
    [EnableCors("bStudioApps")]
    //[Authorize(Roles = "Power")]
    //[AutoValidateAntiforgeryToken]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext db;

        public ItemsController(DbContextOptions<ApplicationDbContext> dbContext) => db = new ApplicationDbContext(dbContext);

        //[HttpGet]
        //public async Task<IEnumerable> List()
        //{
        //    string qry = $"[dbo].[spStockItems]";
        //    using var ddb = db.Database.GetDbConnection();
        //    var results = await ddb.QueryAsync<ItemBalances>(qry, commandType: CommandType.StoredProcedure);
        //    return results;
        //}

        [HttpGet]
        public async Task<IEnumerable> List() => await db.Items.ToListAsync();

        [HttpGet]
        public async Task<IEnumerable> Prices() => await db.Items.Where(x => x.Prices.Count > 0).Select(x => new { x.ItemName, x.MinimumStock, x.ItemsID, x.Prices.OrderBy(v=>v.DateSet).LastOrDefault().Price }).ToListAsync();

        [HttpGet]
        public async Task<IEnumerable> ToSell()
        {
            string qry = $"[dbo].[spItemBalances]";
            using var ddb = db.Database.GetDbConnection();
            var results = await ddb.QueryAsync<SellingItems>(qry, commandType: CommandType.StoredProcedure);
            return results.ToList();
        }

        [HttpGet]
        public async Task<IEnumerable> Balances() => await db.Items
            .Select(x => new
            {
                x.ItemName,
                x.ItemsID,
                x.Group,
                x.MinimumStock,
                Received = x.Stockings.Sum(t => t.Quantity),
                Issued = x.Sales.Sum(t => t.Quantity)
            })
            .ToListAsync();


        [HttpGet]
        public async Task<IActionResult> Find(int id)
        {
            var item = await db.Items.FindAsync(id);
            return item == null ? NotFound(new { Message = "Cannot find the item" }) : Ok(item);
        }

        [HttpGet]
        public async Task<IEnumerable> Ledger(int id, DateTime start)
        {
            string qry = $"[dbo].[spLedger]";
            using var ddb = db.Database.GetDbConnection();
            var results = await ddb.QueryAsync<Ledger>(qry, new { item = id, start }, commandType: CommandType.StoredProcedure);
            return results;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Items item)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            if (await db.Items.AnyAsync(x => x.ItemName == item.ItemName))
                return BadRequest(new { Message = $"{item.ItemName} already exists" });
            db.Add(item);
            await db.SaveChangesAsync();
            return Created($"/Items/Find?id={item.ItemsID}", item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Items item)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            var _item = await db.Items.FindAsync(item.ItemsID);
            if (item == null)
                return BadRequest(new { Message = $"{item.ItemName} does not exist" });
            _item.ItemName = item.ItemName;
            db.Entry(_item).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Created($"/Items/Find?id={item.ItemsID}", item);
        }
    }
}