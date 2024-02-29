using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesManager.Model;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace SalesManager.Controllers
{
    [EnableCors("bStudioApps")]
    public class HomeController(DbContextOptions<ApplicationDbContext> dbContextOptions) : Controller
    {
        private readonly ApplicationDbContext db = new (dbContextOptions);
        public IActionResult Index()=> Ok("Hello world");

        [HttpGet]
        public async Task<IEnumerable> Sales(DateTime date)
        {
            const string qry = "spSales";
            return await db.Database.GetDbConnection().QueryAsync<SalesReport>(qry, param: new {date}, commandType: System.Data.CommandType.StoredProcedure) ;
        }

        [HttpGet]
        public async Task<IEnumerable> Profits()
        {
            const string qry = "SELECT * FROM vwProfits";
            return await db.Database.GetDbConnection().QueryAsync<VwProfits>(qry);
        }
    }

public class SalesReport
    {
        public int ItemsID { get; set; }
        public string ItemName { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
    }

    public class VwProfits
    {
        public int ItemsID { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal Expected { get; set; }
    }

}
