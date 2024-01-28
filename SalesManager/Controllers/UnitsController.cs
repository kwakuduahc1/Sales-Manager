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
    public class UnitsController : Controller
    {
        private readonly ApplicationDbContext db;

        public UnitsController(DbContextOptions<ApplicationDbContext> dbContext) => db = new ApplicationDbContext(dbContext);

        [HttpGet]
        public async Task<IEnumerable> List(int id) => await db.Units.Where(x => x.ItemsID == id && x.Active).Select(x => new { x.UnitsID, x.ItemsID, x.Unit, x.Quantity, x.Items.ItemName }).ToListAsync();

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUnitsVM unit)
        {

            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            if (await db.Units.AnyAsync(x => x.ItemsID == unit.ItemsID && x.Unit == unit.Unit && x.Active))
                return BadRequest(new { Message = $"{unit.Unit} already exists for this item" });
            var _unit = unit.Convert();
            db.Units.Add(_unit);
            await db.SaveChangesAsync();
            unit.UnitsID = _unit.UnitsID;
            return Created("", unit);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Units unit)
        {

            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            var ut = await db.Units.FindAsync(unit.UnitsID);
            if (ut is null)
                return BadRequest(new { Message = $"The unit was not found" });
            ut.Unit = unit.Unit;
            ut.Active = true;
            db.Entry(ut).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Created("", unit);
        }

        [HttpPut]
        public async Task<IActionResult> Delete([FromBody] Units unit)
        {

            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            var ut = await db.Units.FindAsync(unit.UnitsID);
            if (ut is null)
                return BadRequest(new { Message = $"The unit was not found" });
            ut.Active = false;
            db.Entry(ut).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Created("", unit);
        }
    }
}
