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
            return View(billView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult saveDetails(BillView billView) 
        {
            if (ModelState.IsValid)
            {
                BillDetails billDetails = new BillDetails();
                billDetails.BillId = billView.BillId;
                billDetails.BillsTotal = billView.BillDetails.BillsTotal;
                billDetails.PaidUp = (int)billView.BillDetails.PaidUp;

                if (billView.BillDetails.PercentageDiscount != 0)
                {
                    billDetails.PercentageDiscount = (int)billView.BillDetails.PercentageDiscount;
                    billDetails.ValueDiscount = (int)(billView.BillDetails.PercentageDiscount / 100) * billDetails.BillsTotal;
                    billDetails.TheNet = (int)(billDetails.BillsTotal - (billView.BillDetails.PercentageDiscount / 100) * billDetails.BillsTotal);
                    billDetails.TheRest = (int)billDetails.TheNet - billDetails.PaidUp;
                }
                if (billView.BillDetails.PercentageDiscount == 0)
                {
                    billDetails.ValueDiscount = (int)billView.BillDetails.ValueDiscount;
                    billDetails.PercentageDiscount = (int)(billView.BillDetails.ValueDiscount / billDetails.BillsTotal) * 100;
                    billDetails.TheNet = (int) (billDetails.BillsTotal - billView.BillDetails.ValueDiscount);
                    billDetails.TheRest = billDetails.TheNet - billDetails.PaidUp;
                }
                context.BillDetails.Add(billDetails);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else {
                ViewData["ClintId"] = new SelectList(context.Clients, "Id", "Name");
                ViewData["ItemId"] = new SelectList(context.Items, "Id", "Name");
                return View("Create", billView);
            }
          
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(BillView billView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Bill bill = new Bill();
        //        bill.Id = billView.Id;
        //        bill.BillDate = billView.BillDate;
        //        bill.ClintId = billView.ClintId;


        //        context.Add(bill);
        //        await context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));


        //    }
        //    ViewData["ClintId"] = new SelectList(context.Clients, "Id", "Address", billView.ClintId);
        //    return View(billView);
        //}

        //#region remot method 

        //public IActionResult RequiredBillDate(DateTime BillDate)
        //{
        //    if (BillDate == DateTime.Parse("1/1/0001 12:00:00 AM"))
        //    {

        //        return Json(false);

        //    }
        //    else
        //    {
        //        return Json(true);
        //    }

        //}
        //public IActionResult RequiredClint(int ClintId)
        //{
        //    if (ClintId <= 0)
        //    {

        //        return Json(false);

        //    }
        //    else
        //    {
        //        return Json(true);
        //    }

        //}

        //public IActionResult RequiredItemId(BillItemView billItemView)
        //{
        //    if (billItemView.ItemCode <= 0)
        //    {

        //        return Json(false);

        //    }
        //    else
        //    {
        //        return Json(true);
        //    }

        //}
        //public IActionResult comparisonQuantity(BillItemView billItemView)
        //{

        //    int QuantityNow = context.Items.Where(s => s.Id == billItemView.ItemCode).Select(s => s.QuantityRest).FirstOrDefault();
        //    if (billItemView.Quantity <= QuantityNow)
        //    {


        //        return Json(true);

        //    }
        //    else
        //    {
        //        return Json(false);
        //    }

        //}



        //#endregion



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
        /*public IActionResult addBillItem(BillView billView)
        {

        }*/
        public IActionResult addBillItem(string BillDate, int BillId, int ClintId, int ItemCode, int price, int Quantity)
        {
            BillView billView = new BillView();
            BillItemView billItemView = new BillItemView();
            Ajaxvalidation ajaxvalidation = new Ajaxvalidation();
            ajaxvalidation.status = true;

            #region custom validation using Ajaxvalidation
            if (BillDate == "0001-01-01T00:00:00.000")
            {
                ajaxvalidation.status = false;
                ajaxvalidation.Date = " Bill Date is Required ";
            }
            if (ClintId <= 0)
            {
                ajaxvalidation.status = false;
                ajaxvalidation.clintError = "CLIENT NAME is Required ";
            }
            if (ItemCode <= 0)
            {
                ajaxvalidation.status = false;
                ajaxvalidation.ItemCode = "ITEM NAME is Required ";
            }
            if (Quantity <= 0)
            {
                ajaxvalidation.status = false;
                ajaxvalidation.Quantity = "Quantity Must be Greater than Zero ";
            }
            int QuantityNow = context.Items.Where(s => s.Id == ItemCode).Select(s => s.QuantityRest).FirstOrDefault();
            if (Quantity > QuantityNow)
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
                ajaxvalidation.status = true;
                ajaxvalidation.BillView = billView;
                Bill bill = context.Bills.Where(s => s.Id == BillId).SingleOrDefault();
                if (bill == null)
                {
                    bill = new Bill();
                    billView.BillId = bill.Id = BillId;
                    billView.BillDate = bill.BillDate = DateTime.Parse(BillDate);
                    billView.ClintId = bill.ClintId = ClintId;
                    context.Add(bill);
                    context.SaveChanges();
                }
                BillItem billItem = new BillItem();

                billItem.billId = BillId;

                billItemView.ItemCode = billItem.ItemId = ItemCode;
                billItemView.SellingPrice = billItem.SellingPrice = price;
                billItemView.Quantity = billItem.Quantity = Quantity;
                billItemView.TotalBalance = billItem.TotalBalance = Quantity * price;
                context.Add(billItem);
                context.SaveChanges();
                Item item = context.Items.Where(x => x.Id == ItemCode).FirstOrDefault();
                billItemView.ItemName = item.Name;
                billItemView.Unit = item.Unit.Name;
                item.QuantityRest = item.QuantityRest - Quantity;
                context.SaveChanges();


                List<BillItem> billItems = context.BillItems.Where(s => s.billId == BillId).ToList();
                int BillsTotal = 0;

                foreach (BillItem billItem1 in billItems)
                {
                    BillItemView view = new BillItemView();
                    view.ItemName = billItem1.Item.Name;
                    view.ItemCode = billItem1.Item.Id;
                    view.Unit = billItem1.Item.Unit.Name;
                    view.SellingPrice = billItem1.SellingPrice;
                    view.Quantity = billItem1.Quantity;
                    view.TotalBalance = billItem1.TotalBalance;
                    BillsTotal += view.TotalBalance;
                    billView.listItemView.Add(view);
                }
                billView.billsTotal = BillsTotal;
                #region send partial view as string 
                PartialViewResult partialViewResult = PartialView("_ItemTable", billView.listItemView);
                string viewContent = ConvertViewToString(this.ControllerContext, partialViewResult, _viewEngine);
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
