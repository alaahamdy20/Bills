using Bills.Models;
using Bills.Models.ModelView;
using Bills.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace Bills.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
          _reportService = reportService;
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
            if (viewModel.ToDate==null) { viewModel.ToDate = System.DateTime.Now; }
                var model = _reportService.GetByDate(viewModel.FromDate, (System.DateTime)viewModel.ToDate);

                return PartialView(model);
            }
    }
}
