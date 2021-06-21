using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using SalesManager.Model;

namespace SalesManager.Controllers
{
    [Authorize(Roles = "Power")]
    [EnableCors("bStudioApps")]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext db;

        public RolesController(DbContextOptions<ApplicationDbContext> options) => db = new ApplicationDbContext(options);

        [HttpGet]
        public async Task<IEnumerable> List() => await db.Roles.Select(x => new { x.Id, x.Name, x.NormalizedName }).ToListAsync();

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IdentityRole role)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            if (await db.Roles.AnyAsync(x => x.Name == role.Name))
                return BadRequest(new { Message = "Role already exists" });
            role.Id = Guid.NewGuid().ToString();
            _ = db.Add(role);
            _ = await db.SaveChangesAsync().ConfigureAwait(false);
            return Created("", role);
        }

    }
}