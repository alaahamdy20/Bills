using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bills.Models.ModelView;
using Bills.Services.Interfaces;

namespace Bills.Controllers
{
    public class TypeDatasController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly ITypeDataService _typeDataService;

        public TypeDatasController(ICompanyService companyService, ITypeDataService typeDataService)
        {
            _companyService = companyService;
            _typeDataService = typeDataService;
        }

      
        public IActionResult Create()
        {
            SelectList companysList = new (_companyService.getAll(), "Id", "Name");
            ViewData["companys"]= companysList;
            return View(new TypeView());
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TypeView typeView)
        {
            if (ModelState.IsValid)
            {
                _typeDataService.create(typeView);
                return RedirectToAction("Create", "Units");
              
            }
            SelectList companysList = new (_companyService.getAll(), "Id", "Name");
            ViewData["companys"] = companysList;
            return View(typeView);
        }

        public IActionResult TypeNameUniqe(string Name ,int  CompanyId) {
            return Json(_typeDataService.Unique(Name,CompanyId));
        }



    }
}
