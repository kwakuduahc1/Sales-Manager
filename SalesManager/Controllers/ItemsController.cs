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
        public async Task<IEnumerable> Prices()
        {
            const string qry = @"SELECT * FROM [dbo].[vwItemPrices]";
            return await db.Database.GetDbConnection().QueryAsync <ItemPricesVm> (qry);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable> ToSell()
        {
            string qry = @"EXECUTE [dbo].[spItemsToSell]";
            return await db.Database.GetDbConnection().QueryAsync<SellingItems>(qry);
        }

        [HttpGet]
        public async Task<IEnumerable> Balances()
        {
            var qry = "SELECT * FROM ItemBalances";
            return await db.Database.GetDbConnection().QueryAsync<BalancesVm>(qry);
        }

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
            item.DateAdded = DateTime.UtcNow;
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

    public class BalancesVm
    {
        public string ItemName { get; set; }
        public int ItemsID { get; set; }
        public string Group { get; set; }
        public int MinimumStock { get; set; }
        public int Received { get; set; }
        public int Issued { get; set; }
        public double Total { get; set; }
    }

    public class ItemPricesVm
    {
        public int ItemsID { get; set; }
        public string ItemName { get; set; }
        public int PricesID { get; set; }
        public string Group { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public int UnitsID { get; set; }
        public int Balance { get; set; }
    }
}