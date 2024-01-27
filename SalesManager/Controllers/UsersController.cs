using System.Collections;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesManager.Model;
using SalesManager.Models;

namespace SalesManager.Controllers
{
    //[Authorize(Roles = "Power")]
    [EnableCors("bStudioApps")]
    public class UsersController(UserManager<ApplicationUser> userManager, DbContextOptions<ApplicationDbContext> options) : Controller
    {
        readonly ApplicationDbContext db = new ApplicationDbContext(options);
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        [HttpGet]
        public async Task<IActionResult> Find(string id)
        {
            var user = await db.ApplicationUsers.Where(x => x.Id == id).Select(x => new { usersID = x.Id, x.UserName }).SingleOrDefaultAsync();
            return user == null ? NotFound() : Ok(user);
        }


        [HttpGet]
        public async Task<IEnumerable> List()
        {
            return await db.ApplicationUsers.Select(x => new
            {
                x.UserName,
                x.Id,
                x.FullName
            }).ToListAsync();
        }

        [HttpGet]
        public async Task<IEnumerable> Roles(string id) => await db.UserClaims.Where(x => x.UserId == id).Select(x => new { x.UserId, x.Id, x.ClaimValue, x.ClaimType }).ToListAsync();

        [HttpPost]
        public async Task<IActionResult> AddToRole([FromBody] URoles urole)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            var user = await _userManager.FindByIdAsync(urole.ID);
            if (user == null)
                return BadRequest(new { Message = "The user was not found" });
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, urole.Role));
            return Accepted("");
        }
        public async Task<IActionResult> RemoveRole([FromBody] URoles urole)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            var user = await _userManager.FindByIdAsync(urole.ID);
            if (user == null)
                return BadRequest(new { Message = "The user was not found" });
            var claims = await _userManager.GetClaimsAsync(user);
            var ucms = claims.FirstOrDefault(x => x.Value == urole.Role);
            await _userManager.RemoveClaimAsync(user, ucms);
            return Accepted();
        }

        [HttpDelete()]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> RemoveUser(string id)
        {
            var req = Request;
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return BadRequest(new { Message = "The user was not found" });
            await _userManager.DeleteAsync(user);
            return Accepted();
        }
    }
}
