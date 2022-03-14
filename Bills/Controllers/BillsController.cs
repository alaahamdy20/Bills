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

namespace Bills.Controllers
{
    public class BillsController : Controller
    {
        private readonly DatabaseContext context;
        private readonly ICompositeViewEngine _viewEngine;
       
        public BillsController(DatabaseContext context, ICompositeViewEngine viewEngine)
        {
            this.context = context;
            _viewEngine = viewEngine;
        }


        public IActionResult Create()
        {
            ViewData["ClintId"] = new SelectList(context.Clients, "Id", "Name");
            ViewData["ItemId"] = new SelectList(context.Items, "Id", "Name");
            BillView billView = new BillView();
            billView.BillId = 1 + context.Bills.Count();

            var str = JsonConvert.SerializeObject(billView);
            HttpContext.Session.SetString("billItemView", str);

            return View(billView);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult saveDetails(BillView billView) 
        {
            var str2 = HttpContext.Session.GetString("billItemView");
            var billItemViews = JsonConvert.DeserializeObject<BillView>(str2);

            if (ModelState.IsValid)
            {
                #region add bill 

                #region create bill 
                Bill bill = new Bill();
                bill.BillDate = billItemViews.BillDate;
                bill.Id = billItemViews.BillId;
                bill.ClintId=billItemViews.ClintId;

                bill.BillsTotal = (float)billView.BillsTotal;
                bill.PercentageDiscount=billView.PercentageDiscount;
                bill.ValueDiscount=billView.ValueDiscount;
                bill.PaidUp = billView.PaidUp;
                bill.TheRest=billView.TheRest;
                bill.TheNet=billView.TheNet;
                #endregion

                #region add Items to this bill 
                foreach (BillItemView itemView in  billItemViews.listItemView)
                {
                    BillItem billItem = new BillItem();
                    billItem.billId = bill.Id;
                    billItem.ItemId = itemView.ItemCode;
                    billItem.SellingPrice = itemView.SellingPrice;
                    billItem.Quantity = itemView.Quantity;
                    billItem.TotalBalance = itemView.TotalBalance;
                    bill.BillItems.Add(billItem);
                }
                #endregion

                #region update Quantity Rest for items
                foreach (var newQuantity in billItemViews.ItemsQuantity)
                {
                    Item item = context.Items.Where(s => s.Id == newQuantity.Key).FirstOrDefault();
                    item.QuantityRest = newQuantity.Value;
                    context.Update(item);
                
                }
                #endregion

                context.Add(bill);
                context.SaveChanges();

                #endregion

                TempData["alert"] = "  the invoice has been sucessfully added";
                return RedirectToAction("Create", "Bills");
            }
            billView.listItemView = billItemViews.listItemView;
            return View("Create", billView);
        }

        #region AJAX method 

        public IActionResult showSellingPrice(int id ) 
        {
           int sellingPrice = context.Items.Where(s => s.Id == id).Select(s => s.SellingPrice).FirstOrDefault();
            return Json(sellingPrice);
        }
        public IActionResult addToTotal(int price , int qu)
        {
            return Json(price*qu);
        }
        public IActionResult addBillItem(BillView billView)
        {
            BillItemView billItemView = new BillItemView();
            int itemQuantityRest = context.Items.Where(s => s.Id == billView.ItemCode).Select(s => s.QuantityRest).FirstOrDefault();
            var str2 = HttpContext.Session.GetString("billItemView");
            var billItemViews = JsonConvert.DeserializeObject<BillView>(str2);
          
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
                foreach (var restQuantitys in billItemViews.ItemsQuantity)
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
                #region add billItemview to listItemView in billView in session 
                ajaxvalidation.status = true;
                ajaxvalidation.BillView = billView;
             
                billItemView.ItemCode = billView.ItemCode;
                billItemView.SellingPrice = billView.SellingPrice;
                billItemView.Quantity  = billView.Quantity;
                billItemView.TotalBalance = billView.Quantity * billView.SellingPrice;
                Item item= context.Items.Where(s => s.Id == billView.ItemCode).FirstOrDefault();
                billItemView.ItemName = item.Name;
                billItemView.Unit = item.Unit.Name;

                billItemViews.BillId = billView.BillId;
                billItemViews.BillDate= billView.BillDate;
                billItemViews.ClintId = billView.ClintId;

                billItemViews.listItemView.Add(billItemView);
                billItemViews.ItemsQuantity[billView.ItemCode] = itemQuantityRest - billView.Quantity;
                #endregion

                #region calc Bills Total
                int BillsTotal = 0;
                foreach (BillItemView view in billItemViews.listItemView)
                {
                    BillsTotal += view.TotalBalance;
                }
                billView.BillsTotal = BillsTotal;
                #endregion

                #region send partial view as string 
                PartialViewResult partialViewResult = PartialView("_ItemTable", billItemViews.listItemView);
                string viewContent = ConvertViewToString(this.ControllerContext, partialViewResult, _viewEngine);
                #endregion

                #region update object in session 
                  var str = JsonConvert.SerializeObject(billItemViews);
                  HttpContext.Session.SetString("billItemView", str);
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
