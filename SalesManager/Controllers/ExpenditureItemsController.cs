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
    public class ExpenditureItemsController(DbContextOptions<ApplicationDbContext> dbContext) : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(dbContext);

        [HttpGet()]
        public async Task<IEnumerable> List()
        {
            return await db.ExpenditureItems.ToListAsync();        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExpenditureItems item)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            db.ExpenditureItems.Add(item);
            await db.SaveChangesAsync();
            return Created("", item);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ExpenditureItems item)
        {
            db.ExpenditureItems.Update(item);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
