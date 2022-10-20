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
    public class PricesController : Controller
    {
        private readonly ApplicationDbContext db;

        public PricesController(DbContextOptions<ApplicationDbContext> dbContext) => db = new ApplicationDbContext(dbContext);

        [HttpGet]
        public async Task<IEnumerable> List()
        {
            var qry = @"select i.itemsID, i.itemName, ISNULL(p.Price, 0) price
                            from Items i 
                            outer apply(
	                            select top 1 Price 
	                            from Prices p 
	                            where p.ItemsID = i.ItemsID
	                            order by DateSet desc
                            )p
                            ";
            return await db.Database.GetDbConnection().QueryAsync(qry);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<Prices> prices)
        {

            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            prices.ForEach(p =>
            {
                p.DateSet = DateTime.UtcNow;
                p.Setter = User.Identity.Name;
                db.Prices.Add(p);
            });
            await db.SaveChangesAsync();
            return Created("", prices);
        }
    }
}
