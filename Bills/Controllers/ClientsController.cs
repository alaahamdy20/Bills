using Microsoft.AspNetCore.Mvc;
using Bills.Models.Entities;
using Bills.Services.Interfaces;

namespace Bills.Controllers
{
    public class ClientsController : Controller
    {


        private readonly IClientService _clientService;


        public ClientsController( IClientService clientService)
        {

            _clientService = clientService;
        }
        public IActionResult Create()
        {
            Client client = new ();
            client.Id = 1+_clientService.getAll().Count;
            return View(client);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                client.Id = 1 + _clientService.getAll().Count;
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
