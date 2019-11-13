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
        private IProductRepository repository;
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        //View action 
        public ViewResult Index() => View(repository.Products);
        //View action for tag-helper asp-action="Edit"
        public ViewResult Edit(int productId) => View(repository
            .Products.FirstOrDefault(p => p.ProductID == productId));
        //
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            //check validated
            if (ModelState.IsValid)
            {
                //if yes Save product and return View Index action
                repository.SaveProduct(product);
                // TempDaya similar to Session and Viewbag but it is temporary, persists until is read by View
                //ViewBag only persists in the current HTTP request=>go to the new URL ViewBag will be lost
                //Session persists until explicit removed
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }
        public ViewResult Create() => View("Edit", new Product());
        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }
        //Seed data from Azure SQL db to deploy
        [HttpPost]
        public IActionResult SeedDatabase()
        {
            SeedData.EnsurePopulated(HttpContext.RequestServices);
            return RedirectToAction(nameof(Index));
        }
    }
}
