﻿using Bills.Models.Entities;
using Bills.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Bills.Controllers
{
    public class ItemReportController : Controller
    {
       
        private readonly IItemService _itemService;
        private readonly IBillItemService _billItemService;

        public ItemReportController(IBillItemService billItemService, IItemService itemService)
        {
            _billItemService = billItemService;
            _itemService = itemService;
        }
        public IActionResult Index()
        {
            return View("searchItem" ,_itemService.getAll().Take(10).ToList());
        }

        public IActionResult Search(string search) {
            if (search == string.Empty || search==null)
            { return PartialView("_Items", _itemService.getAll().Take(10).ToList()); }
            List<Item> items = _itemService.search(search);
              return PartialView("_Items", items);
        }
        
      public IActionResult ItemDetails(int id=1)
        {
            ViewData["itemData"] = _itemService.getById(id);
            List < BillItem > itemDetais = _billItemService.MoreDetails(id);
           return View("ItemDetails", _billItemService.MoreDetails(id));
        }
    }
}