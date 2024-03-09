using Dapper;
using Microsoft.AspNetCore.Authorization;
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

namespace SalesManager.Areas.Stores.Controllers
{
    [EnableCors("bStudioApps")]
    //[Authorize(Roles = "Power")]
    //[AutoValidateAntiforgeryToken]
    public class PricesController(DbContextOptions<ApplicationDbContext> dbContext) : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(dbContext);

        [HttpGet]
        public async Task<IEnumerable> ItemPrices()
        {
            string qry = @"with cte as (select i.ItemsID, i.ItemName, i.[Group], ISNULL(p.Price, st.UnitCost*1.20) Price , s.SupplierName, st.UnitCost, u.UnitsID, u.Unit, RANK() OVER(PARTITION BY i.ItemsID ORDER BY st.DateAdded desc) [Rank]
                            from Items i
                            inner join Stockings st on st.ItemsID = i.ItemsID
                            inner join Units u on u.ItemsID = i.ItemsID
                            full join Prices p on p.UnitsID = u.UnitsID
                            inner join Suppliers s on s.SuppliersID = st.SuppliersID)
                            select ItemsID, ItemName, Unit, UnitsID, [Group], SupplierName, UnitCost, Price, [Rank]
                            from cte
                            where [Rank] < 4
                            order by ItemsID";
            return await db.Database.GetDbConnection().QueryAsync<ItemSupplierPricesVM>(qry);
        }

        [HttpGet("{id:required:int}")]
        public async Task<IEnumerable> List(int id)
        {
            return await db.Prices.Where(x => x.UnitsID == id).Select(x => new
            {
                x.Setter,
                x.UnitsID,
                x.PricesID,
                x.Price,
                x.DateSet
            })
                .OrderByDescending(x => x.DateSet)
            .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Prices price)
        {

            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            price.DateSet = DateTime.UtcNow;
            price.Setter = User.Identity.Name;
            db.Prices.Add(price);
            await db.SaveChangesAsync();
            return Created("", price);
        }

        [HttpPost]
        public async Task<IActionResult> SetPrices([FromBody] Prices[] prices)
        {
            List<Prices> _prices = []; 
            var date = DateTime.UtcNow;
            foreach (var price in prices)
            {
                _prices.Add(new Prices
                {
                    DateSet = date,
                    Price = price.Price,
                    Setter = User.Identity.Name,
                    UnitsID = price.UnitsID,
                });
            }
            db.AddRange(_prices);
            await db.SaveChangesAsync();
            return Ok();
        }
    }

    public class ItemSupplierPricesVM
    {
        public int ItemsID { get; set; }

        public decimal Price { get; set; }
        public string ItemName { get; set; }
        public string Group { get; set; }
        public string SupplierName { get; set; }
        public decimal UnitCost { get; set; }
        public byte Rank { get; set; }

        public int UnitsID { get; set; }

        public string Unit { get; set; }
    }

}
