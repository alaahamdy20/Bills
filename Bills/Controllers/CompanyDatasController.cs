using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bills.Models;
using Bills.Models.Entities;
using Bills.Services.Interfaces;
using Bills.Services;

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
