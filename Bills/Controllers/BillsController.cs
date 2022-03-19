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
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Bills.Services.Interfaces;

namespace Bills.Controllers
{
    public class BillsController : Controller
    {
       
        private readonly ICompositeViewEngine _viewEngine;
        private readonly IItemService _itemService;
        private readonly IClientService _clientService;
        private readonly IBillService _billService;
        public BillsController(IBillService billService, ICompositeViewEngine viewEngine, ICompanyService companyService, ITypeDataService typeDataService, IUnitService unitService, IItemService itemService, IClientService clientService)
        {
            _viewEngine = viewEngine;
            _itemService = itemService;
            _clientService = clientService;
            _billService = billService;
        }

        public IActionResult Create()
        {
            ViewData["ClintId"] = new SelectList(_clientService.getAll(), "Id", "Name");
            ViewData["ItemId"] = new SelectList(_itemService.getAll(), "Id", "Name");
            BillView billView = new BillView();
            billView.BillId = 1 + _billService.getAll().Count();

            var str = JsonConvert.SerializeObject(billView);
            HttpContext.Session.SetString("SessionBillView", str);

            return View(billView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BillView billView) 
        {
            var str2 = HttpContext.Session.GetString("SessionBillView");
            var SessionBillView = JsonConvert.DeserializeObject<BillView>(str2);

            if (ModelState.IsValid)
            {
                #region add bill 
                _billService.create(billViewModel: billView, billViewSession: SessionBillView) ;
                #endregion
                TempData["alert"] = "  the invoice has been sucessfully added";
                return RedirectToAction("Create", "Bills");
            }
            billView.listItemView = SessionBillView.listItemView;
            return View("Create", billView);
        }

        #region AJAX method 
        public IActionResult showSellingPrice(int id ) 
        {
            return Json(_itemService.getById(id).SellingPrice);
        }
        public IActionResult addToTotal(int price , int qu)
        {
            return Json(price*qu);
        }
        public IActionResult addBillItem(BillView billView)
        {
            Item item = _itemService.getById(billView.ItemCode);
            int itemQuantityRest = (item != null) ? item.QuantityRest : 0; 
            var str2 = HttpContext.Session.GetString("SessionBillView");
            var SessionBillView = JsonConvert.DeserializeObject<BillView>(str2);
          
            Ajaxvalidation ajaxvalidation = new Ajaxvalidation();
            ajaxvalidation.status = true;

            #region custom validation using Ajaxvalidation class
            if (billView.BillDate == DateTime.Parse("0001-01-01T00:00:00.000"))
            {
                ajaxvalidation.status = false;
                ajaxvalidation.Date = " Bill Date is Required ";
            }
            if (billView.ClintId <= 0)
            {
                ajaxvalidation.status = false;
                ajaxvalidation.clintError = "CLIENT NAME is Required ";
            } 
            if (billView.ItemCode <= 0)
            {
                ajaxvalidation.status = false;
                ajaxvalidation.ItemCode = "ITEM NAME is Required ";
            }
            else
            {
                #region check on item Quantity Rest
                foreach (var restQuantitys in SessionBillView.ItemsQuantity)
                {
                    if (restQuantitys.Key == billView.ItemCode)
                    {
                        itemQuantityRest = restQuantitys.Value;
                        break;
                    }
                }
                #endregion
            }
            if (billView.Quantity <= 0)
            {
                ajaxvalidation.status = false;
                ajaxvalidation.Quantity = "Quantity Must be Greater than Zero ";
            }
            if (billView.Quantity > itemQuantityRest)
            {
                ajaxvalidation.status = false;
                ajaxvalidation.Quantity = "We don't have this quantity  ";
            }
            if (ajaxvalidation.status == false)
            {
                return Json(ajaxvalidation);
            }
            #endregion

            else
            {
                #region add billItemview to listItemView in SessionBillView in session 
                ajaxvalidation.status = true;

                SessionBillView.BillId = billView.BillId;
                SessionBillView.BillDate = billView.BillDate;
                SessionBillView.ClintId = billView.ClintId;
                SessionBillView.listItemView.Add(_billService.transferToBillItemView(billView));
                SessionBillView.ItemsQuantity[billView.ItemCode] = itemQuantityRest - billView.Quantity;
                #endregion

                #region calc Bills Total
                int BillsTotal = 0;
                foreach (BillItemView view in SessionBillView.listItemView)
                {
                    BillsTotal += view.TotalBalance;
                }
                billView.BillsTotal = BillsTotal;
                #endregion

                #region send partial view as string 
                PartialViewResult partialViewResult = PartialView("_ItemTable", SessionBillView.listItemView);
                string viewContent = ConvertViewToString(this.ControllerContext, partialViewResult, _viewEngine);
                #endregion

                #region update object in session 
                  var str = JsonConvert.SerializeObject(SessionBillView);
                  HttpContext.Session.SetString("SessionBillView", str);
                #endregion

                return Json(new { billView= billView , viewContent= viewContent });
            }
        }
        #endregion

        #region Convert View To String
        private string ConvertViewToString(ControllerContext controllerContext, PartialViewResult pvr, ICompositeViewEngine _viewEngine)
        {
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = _viewEngine.FindView(controllerContext, pvr.ViewName, false);
                ViewContext viewContext = new ViewContext
                    (controllerContext, vResult.View, pvr.ViewData, pvr.TempData, writer, new HtmlHelperOptions());

                vResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }

        #endregion


       
    }
}
