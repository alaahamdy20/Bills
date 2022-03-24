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

namespace Bills.Controllers
{
    public class UnitsController : Controller
    {
        private readonly IUnitService _unitService;

        public UnitsController( IUnitService unitService)
        {
            _unitService = unitService;
        }



        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Unit unit)
        {
            if (ModelState.IsValid)
            {
               _unitService.create(unit);
                return RedirectToAction("Create", "Items");
               
            }
            return View(unit);
        }

        public IActionResult UnitNameUniqe(string Name)
        {
            
           return Json(_unitService.Unique(Name));

        }
    }
}
