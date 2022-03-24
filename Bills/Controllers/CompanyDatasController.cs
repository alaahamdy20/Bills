using Microsoft.AspNetCore.Mvc;
using Bills.Models.Entities;
using Bills.Services.Interfaces;


namespace Bills.Controllers
{
    public class CompanyDatasController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyDatasController(ICompanyService companyService) {
            _companyService = companyService;
        
        }



        // GET: CompanyDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( CompanyData companyData)
        {
            if (ModelState.IsValid)
            {
                _companyService.create(companyData);
                return RedirectToAction("Create", "TypeDatas");
            }
            return View(companyData);
        }
        
        public IActionResult CompanyNameUniqe(string Name)
            {
          
               return Json( _companyService.Unique(Name));
           

            }



    }
}
