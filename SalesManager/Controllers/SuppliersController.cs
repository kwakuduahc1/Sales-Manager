using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesManager.Model;
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SuppliersManager.Controllers
{
    [EnableCors("bStudioApps")]
    //[Authorize(Roles = "Power, Suppliers")]
    public class SuppliersController : Controller
    {
        private readonly ApplicationDbContext db;

        public SuppliersController(DbContextOptions<ApplicationDbContext> dbContext) => db = new ApplicationDbContext(dbContext);

        [HttpGet]
        public async Task<IEnumerable> List() => await db.Suppliers.Where(x => x.IsActive).Select(x => new
        {
            x.SuppliersID,
            x.SupplierName,
            x.Address,
            x.DateAdded
        }).ToListAsync();

        [HttpGet]
        public async Task<IEnumerable> Ledger(int id, byte num = 20) => await db.Stockings.Where(x => x.SuppliersID == id)
            .Select(x => new
            {
                x.SuppliersID,
                x.StockingsID,
                x.Quantity,
                x.Receipt,
                x.DateAdded,
                x.DateBought,
                x.UnitCost,
                x.Items.ItemName
            })
            .Take(num)
            .ToListAsync();

        [HttpGet]
        public async Task<IActionResult> Find(int id)
        {
            var item = await db.Suppliers.FindAsync(id);
            return item == null ? NotFound(new { Message = "Cannot find the supplier" }) : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Suppliers sup)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            if (await db.Suppliers.AnyAsync(x => x.SupplierName == sup.SupplierName && x.IsActive))
                return BadRequest(new { Message = "The supplier name already exists" });
            sup.DateAdded = DateTime.UtcNow;
            sup.IsActive = true;
            db.Add(sup);
            await db.SaveChangesAsync();
            return Created("", sup);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] EditSuppliersVm sup)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            var _sup = await db.Suppliers.FindAsync(sup.SuppliersID);
            if (_sup == null)
                return BadRequest(new { Message = "Transaction does not exist" });
            _sup.SupplierName = sup.SupplierName;
            _sup.Address = sup.Address;
            _sup.IsActive = true;
            db.Entry(_sup).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Accepted(_sup);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var _item = await db.Suppliers.FindAsync(id);
            if (_item == null)
                return BadRequest(new { Message = "The supplier does not exist" });
            _item.IsActive = false;
            db.Entry(_item).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Accepted();
        }
    }
}
