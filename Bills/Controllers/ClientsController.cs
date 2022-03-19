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
    public class ClientsController : Controller
    {

        private readonly ICompanyService _companyService;
        private readonly ITypeDataService _typeDataService;
        private readonly IUnitService _unitService;
        private readonly IItemService _itemService;
        private readonly IClientService _clientService;


        public ClientsController(ICompanyService companyService, ITypeDataService typeDataService, IUnitService unitService, IItemService itemService, IClientService clientService)
        {
            _companyService = companyService;
            _typeDataService = typeDataService;
            _unitService = unitService;
            _itemService = itemService;
            _clientService = clientService;
        }
        public IActionResult Create()
        {
            Client client = new Client();
            client.Id = 1+_clientService.getAll().Count();
            return View(client);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                client.Id = 1 + _clientService.getAll().Count();
               _clientService.create(client);
                TempData["alert"] = "  Client added successfully";
                return RedirectToAction("Create", "Clients");
            }
            return View(client);
        }

        public IActionResult ClientNameUniqe (string Name)
        {

        return Json(_clientService.Unique(Name));

        }
    }
}
