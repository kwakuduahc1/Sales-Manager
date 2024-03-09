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
    public class ExpenditureController(DbContextOptions<ApplicationDbContext> dbContext) : Controller
    {
        private readonly ApplicationDbContext db = new(dbContext);

        [HttpGet]
        public async Task<IEnumerable> List(DateOnly start, DateOnly end)
        {
            const string qry = @"select e.ExpenditureID, ei.ExpenditureItemsID, ei.Item, e.Amount, e.Receiver, CAST(e.DateAdded as date) [Date]
                                from Expenditures e 
                                inner join ExpenditureItems ei on ei.ExpenditureItemsID = e.ExpenditureItemsID
                                where cast(e.DateAdded as date) between @start and @end";
            return await db.Database.GetDbConnection().QueryAsync<ExpenditureDetails>(qry, param: new { start, end });
        }

        [HttpGet]
        public async Task<IEnumerable> ListById(int id, DateOnly start, DateOnly end)
        {
            const string qry = @"select e.ExpenditureID, ei.ExpenditureItemsID, ei.Item, e.Amount, e.Receiver, CAST(e.DateAdded as date) [Date]
                                from Expenditures e 
                                inner join ExpenditureItems ei on ei.ExpenditureItemsID = e.ExpenditureItemsID
                                where ei.ExpenditureItemsID = @id and cast(e.DateAdded as date) between @start and @end";
            return await db.Database.GetDbConnection().QueryAsync<ExpenditureDetails>(qry, param: new { id, start, end });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Expenditure exp)
        {

            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            exp.DateAdded = DateTime.UtcNow;
            db.Expenditures.Add(exp);
            await db.SaveChangesAsync();
            return Created("", exp);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Expenditure exp)
        {
            db.Update(exp);
            await db.SaveChangesAsync();
            return Ok();
        }
    }

    public class ExpenditureDetails
    {
        public int ExpenditureItemsID { get; set; }

        public int ExpendituresID { get; set; }
        public string Item { get; set; }
        public decimal Amount { get; set; }
        public string Receiver { get; set; }
        public DateTime Date { get; set; }
    }

}
