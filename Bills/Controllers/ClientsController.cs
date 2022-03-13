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
    public class ClientsController : Controller
    {
        private readonly DatabaseContext context;

        public ClientsController(DatabaseContext context)
        {
            this.context = context;
        }

        public IActionResult Create()
        {
            Client client = new Client();
            client.Id = 1+context.Clients.Count();
            return View(client);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                client.Id = 1 + context.Clients.Count();
                context.Add(client);
                await context.SaveChangesAsync();
                TempData["alert"] = "  Client added successfully";
                return RedirectToAction("Create", "Clients");
            }
            return View(client);
        }

        public IActionResult ClientNameUniqe (string Name)
        {

            Client  client = context.Clients.Where(s => s.Name == Name).FirstOrDefault();
            if (client != null)
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
