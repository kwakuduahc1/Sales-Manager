using Dapper;
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
    //[Authorize(Roles = "Power")]
    //[AutoValidateAntiforgeryToken]
    public class SupplierPaymentsController : Controller
    {
        private readonly ApplicationDbContext db;

        public SupplierPaymentsController(DbContextOptions<ApplicationDbContext> dbContext) => db = new ApplicationDbContext(dbContext);

        [HttpGet]
        public async Task<IEnumerable> ListBySupplier(int id)
        {
            return await db.SupplierPayments
                .Where(x => x.SuppliersID == id)
                .OrderByDescending(x => x.DatePaid)
                .Take(10)
                .Select(x => new
                {
                    x.SuppliersID,
                    x.DatePaid,
                    x.SupplierPaymentsID,
                    x.PaymentTypesID,
                    x.PaymentTypes.PaymentType,
                    x.Amount,
                    x.Reference
                })
                .ToListAsync();
        }

        [HttpGet]
        public async Task<decimal> Balance(int id)
        {
            string qry = @"select st.Cost - sp.Payments [Amount]
                            from Suppliers s
                            cross apply
                            (
	                            select SUM(sp.Amount) Payments
	                            from SupplierPayments sp
	                            where sp.SuppliersID = s.SuppliersID
                            )sp
                            cross apply(
	                            select SUM(sk.Quantity * sk.UnitCost) [Cost]
	                            from Stockings sk 
	                            where sk.SuppliersID = s.SuppliersID
                            ) st
                            where SuppliersID = @id";
            return await db.Database.GetDbConnection().ExecuteScalarAsync<decimal>(qry, param: new { id });
        }

        [HttpGet]
        public async Task<IEnumerable> List(DateTime start, DateTime end)
        {
            return await db.SupplierPayments.Where(x => x.DatePaid >= start && x.DatePaid <= end).Select(x => new
            {
                x.SuppliersID,
                x.DatePaid,
                x.SupplierPaymentsID,
                x.PaymentTypesID,
                x.PaymentTypes.PaymentType,
                x.Amount
            }).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupplierPayments pay)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid data was submitted", Message = ModelState.Values.First(x => x.Errors.Count > 0).Errors.Select(t => t.ErrorMessage).First() });
            pay.DatePaid = DateTime.UtcNow;
            db.SupplierPayments.Add(pay);
            await db.SaveChangesAsync();
            return Created("", pay);
        }
    }
}
