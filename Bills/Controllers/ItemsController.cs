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
    public class ItemsController : Controller
    {
        private readonly DatabaseContext context;

        public ItemsController(DatabaseContext context)
        {
            this.context = context;
        }

  
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(context.CompanyDatas, "Id", "Name");
            ViewData["typeId"] = new SelectList(context.TypeDatas, "Id", "Name");
            ViewData["unitId"] = new SelectList(context.Units, "Id", "Name");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Item item)
        {
            if (ModelState.IsValid)
            {
              
                
                    item.QuantityRest = item.BalanceOfTheFirstDuration;
                    context.Add(item);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Create", "Clients");
          
              
            }
            ViewData["CompanyId"] = new SelectList(context.CompanyDatas, "Id", "Name", item.CompanyId);
            ViewData["typeId"] = new SelectList(context.TypeDatas, "Id", "Name");
            ViewData["unitId"] = new SelectList(context.Units, "Id", "Name");
            return View(item);
        }
        public IActionResult ItemNameUniqe(string Name , int CompanyId)
        {

            Item item  = context.Items.Where(s => s.Name == Name).FirstOrDefault();
            //Item item = context.Items.Where(s => s.Name == Name && s.CompanyId== CompanyId).FirstOrDefault(); // if he need validation on buth item and company 
            if (item != null)
            {
                return Json(false);

            }
            else
            {
                return Json(true);
            }

        }

        #region AJAX method
        public IActionResult UpdateSelector(int id ) {
           List<TypeData> typeDatas = context.CompanyTypes.Where(s => s.CompanyDataId==id).Select(s=>s.TypeData).ToList();
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
