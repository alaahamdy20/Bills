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
    public class ItemsController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly ITypeDataService _typeDataService;
        private readonly IUnitService _unitService;
        private readonly IItemService _itemService;

        public ItemsController(ICompanyService companyService, ITypeDataService typeDataService, IUnitService unitService , IItemService itemService)
        {
            _companyService = companyService;
            _typeDataService = typeDataService;
            _unitService = unitService;
            _itemService = itemService;
        }



        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_companyService.getAll(), "Id", "Name");
            ViewData["typeId"] = new SelectList(_typeDataService.getAll(), "Id", "Name");
            ViewData["unitId"] = new SelectList(_unitService.getAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Item item)
        {
            if (ModelState.IsValid)
            {
                    item.QuantityRest = item.BalanceOfTheFirstDuration;
                   _itemService.create(item);
                    return RedirectToAction("Create", "Clients");
            }
            ViewData["CompanyId"] = new SelectList(_companyService.getAll(), "Id", "Name");
            ViewData["typeId"] = new SelectList(_typeDataService.getAll(), "Id", "Name");
            ViewData["unitId"] = new SelectList(_unitService.getAll(), "Id", "Name");
            return View(item);
        }
        public IActionResult ItemNameUniqe(string Name , int CompanyId)
        {

            return Json(_itemService.Unique(Name));

        }

        #region AJAX method
        public IActionResult UpdateSelector(int id ) {
            List<TypeData> typeDatas = _typeDataService.getByCompanyId(id);
            return Json(typeDatas);
        }
        #endregion

        #region remot method
        public IActionResult BuyingPriceCondition(int BuyingPrice, int SellingPrice)
        {
            if (BuyingPrice <= SellingPrice)
            {

                return Json(true);

            }
            else
            {
                return Json(false);
            }

        }
        public IActionResult RequiredCompany(int CompanyId)
        {
            if (CompanyId <= 0)
            {

                return Json(false);

            }
            else
            {
                return Json(true);
            }

        }
        public IActionResult RequiredType(int typeId)
        {
            if (typeId <= 0)
            {

                return Json(false);

            }
            else
            {
                return Json(true);
            }

        }
        public IActionResult RequiredUnit(int unitId)
        {
            if (unitId <= 0)
            {

                return Json(false);

            }
            else
            {
                return Json(true);
            }

        }
        #endregion
    }
}
