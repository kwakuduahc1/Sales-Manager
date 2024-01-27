using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesManager.Models;
using SalesManager.Model;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SalesManager.Model.ViewModels;
using SalesManager.Helper;

namespace SalesManager.Controllers
{
    [EnableCors("bStudioApps")]
    //[AutoValidateAntiforgeryToken]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public IWebHostEnvironment Env { get; }
        private readonly ApplicationDbContext db;
        private readonly AppFeatures appFeatures;
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, DbContextOptions<ApplicationDbContext> contextOptions, IWebHostEnvironment environment, AppFeatures app)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = new ApplicationDbContext(contextOptions);
            Env = environment;
            appFeatures = app;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginVm user)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            var _user = await _userManager.FindByNameAsync(user.UserName);
            if (_user == null)
                return Unauthorized(new { Message = "Invalid user name or password" });
            if (!await _userManager.CheckPasswordAsync(_user, user.Password))
                return Unauthorized();
            await _signInManager.SignInAsync(_user, false);
            var claims = await _userManager.GetClaimsAsync(_user);
            var token = new AuthHelper(claims, Env, appFeatures).Key;
            return Ok(new { Token = token });
        }

        [HttpPost]
        //[Authorize(Roles = "Power")]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterVm reg)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            if (reg.Password != reg.ConfirmPassword)
                return BadRequest(new { Error = "The confirmation password must match" });
            ApplicationUser user = reg.Transform();
            var result = await _userManager.CreateAsync(user, user.Password);
            if (!result.Succeeded)
                return BadRequest(new { Message = result.Errors.First().Description });
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName));
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
            if (await _userManager.Users.CountAsync() == 1)
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Power"));
            if (await _userManager.Users.CountAsync() == 2)
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Stocker"));
            await db.SaveChangesAsync();
            return Created("", new { user.UserName, user.PhoneNumber, user.Email, user.Id });
        }

        [HttpPost]
        [Authorize]
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Accepted();
        }
    }
}