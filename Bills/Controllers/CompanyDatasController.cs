using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bills.Models;
using Bills.Models.Entities;

namespace Bills.Controllers
{
    public class CompanyDatasController : Controller
    {
        private readonly DatabaseContext context;

        public CompanyDatasController(DatabaseContext _context) { 
        context = _context;
        
        }



        // GET: CompanyDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CompanyData companyData)
        {
            if (ModelState.IsValid)
            {
                context.Add(companyData);
                await context.SaveChangesAsync();
                return RedirectToAction("Create", "TypeDatas");
            }
            return View(companyData);
        }
        
              public IActionResult CompanyNameUniqe(string Name)
        {
          
            CompanyData companyData = context.CompanyDatas.Where(s => s.Name == Name).FirstOrDefault();
            if (companyData != null)
            {
                return Json(false);

            }
            else
            {
                return Json(true);
            }

        }



    }
}
