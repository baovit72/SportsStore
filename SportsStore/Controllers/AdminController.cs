using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //View action 
        public ViewResult Index() => View();
        //Seed data from Azure SQL db to deploy
        [HttpPost]
        public IActionResult SeedDatabase()
        {
            SeedData.EnsurePopulated(HttpContext.RequestServices);
            return RedirectToAction(nameof(Index));
        }
    }
}
