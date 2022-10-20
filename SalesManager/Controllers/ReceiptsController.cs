using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesManager.Model;
using System.Collections;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManager.Controllers
{
    [EnableCors("bStudioApps")]
    //[Authorize(Roles = "Power, Stocker")]
    public class ReceiptsController : Controller
    {
        private readonly ApplicationDbContext db;

        public ReceiptsController(DbContextOptions<ApplicationDbContext> dbContext) => db = new ApplicationDbContext(dbContext);

        [HttpGet]
        public async Task<IEnumerable> List(string id) => await db.Stockings.Where(x => x.Receipt == id).Select(x => new ReceiptDetails { DateAdded = x.DateAdded, ItemName = x.Items.ItemName, Quantity = x.Quantity }).ToListAsync();

        [HttpGet]
        public async Task<IEnumerable> Received() => await db.Stockings.OrderBy(x => x.DateBought).Take(50).Select(x => new
        {
            x.DateAdded,
            x.Items.ItemName,
            x.Quantity,
            x.ItemsID
        }).ToListAsync();
    }
}
