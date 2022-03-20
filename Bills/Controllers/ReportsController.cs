using Bills.Models;
using Bills.Models.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bills.Controllers
{
    public class ReportsController : Controller
    {
        private readonly DatabaseContext context;

        public ReportsController(DatabaseContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(ReportViewModel viewModel)
            {
                if (!ModelState.IsValid)
                {
                
                    return RedirectToAction(nameof(Index),viewModel);
                }
                var model = context.Bills.Where(b => b.BillDate >= viewModel.FromDate && b.BillDate < viewModel.ToDate).ToList();

                return PartialView(model);
            }
    }
}
