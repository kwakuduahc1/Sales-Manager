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
    [Authorize(Roles = "Power")]
    //[AutoValidateAntiforgeryToken]
    public class PricesController : Controller
    {
        private readonly ApplicationDbContext db;

        public PricesController(DbContextOptions<ApplicationDbContext> dbContext) => db = new ApplicationDbContext(dbContext);

        [HttpGet]
        public async Task<IEnumerable> List(int id) => await db.Prices.Where(x => x.ItemsID == id).OrderByDescending(x => x.DateSet).ToListAsync();

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
    }
}
