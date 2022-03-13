using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bills.Models;
using Bills.Models.Entities;
using Bills.Models.ModelView;

namespace Bills.Controllers
{
    public class TypeDatasController : Controller
    {
        private readonly DatabaseContext context;

        public TypeDatasController(DatabaseContext context)
        {
            this.context = context;
        }

      
        public IActionResult Create()
        {
            SelectList companysList = new SelectList(context.CompanyDatas.ToList(), "Id", "Name");
            ViewData["companys"]= companysList;
            return View(new TypeView());
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TypeView typeView)
        {
            if (ModelState.IsValid)
            {
                if (typeView.CompanyId == 0)
                {

                    ModelState.AddModelError("CompanyId", "COMPANY NAME is Required");

                }
                else
                {
                    TypeData OldtypeData = context.TypeDatas.Where(s => s.Name == typeView.Name).SingleOrDefault();
                    if (OldtypeData == null)
                    {
                        TypeData typeData = new TypeData();
                        typeData.Name = typeView.Name;
                        typeData.Notes = typeView.Notes;
                        context.Add(typeData);
                        context.SaveChanges();


                    }
                    TypeData NewTypeData = context.TypeDatas.Where(s => s.Name == typeView.Name).SingleOrDefault();
                        CompanyType companyType = new CompanyType();
                        companyType.CompanyDataId = typeView.CompanyId;
                        companyType.TypeDataId = NewTypeData.Id;
                        context.CompanyTypes.Add(companyType);
                        context.SaveChanges();
                 

                    return RedirectToAction("Create", "Units");
                }
            }
            SelectList companysList = new SelectList(context.CompanyDatas.ToList(), "Id", "Name");
            ViewData["companys"] = companysList;
            return View(typeView);
        }

        public IActionResult TypeNameUniqe(string Name ,int  CompanyId) {
            int typeDataID = context.TypeDatas.Where(s=>s.Name==Name).Select(s=>s.Id).SingleOrDefault();
            CompanyType companyType = context.CompanyTypes.Where(s=>s.TypeDataId==typeDataID && s.CompanyDataId==CompanyId).FirstOrDefault();
            if (companyType != null )
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
