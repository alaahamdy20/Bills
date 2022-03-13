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
    public class UnitsController : Controller
    {
        private readonly DatabaseContext context;

        public UnitsController(DatabaseContext context)
        {
            this.context = context;
        }

     
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                context.Add(unit);
                await context.SaveChangesAsync();
                return RedirectToAction("Create", "Items");
               
            }
            return View(unit);
        }

        public IActionResult unitNameUniqe(string Name)
        {
            
            Unit unit = context.Units.Where(s => s.Name == Name).FirstOrDefault();
            if (unit != null)
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
