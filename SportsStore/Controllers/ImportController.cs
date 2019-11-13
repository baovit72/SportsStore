using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace SportsStore.Controllers
{
    public class ImportController : Controller
    {
        private IImportOrderRepository repository;
        private IProductRepository productRepo;
        private ImportItems importItems;
        public int PageSize = 4;
        public ImportController(IImportOrderRepository repoService, 
            IProductRepository productRepo,
            ImportItems importItemsService)
        {
            repository = repoService;
            this.productRepo = productRepo;
            importItems = importItemsService;
        }
        // Done
        public ViewResult Index() => View(repository.ImportOrders);
        public ViewResult PlaceOrder(string category, int productPage = 1) 
            => View(new ProductsListViewModel
        {
            //LinQ select product 
            Products = productRepo.Products
                 .Where(p => category == null || p.Category == category)
                 .OrderBy(p => p.ProductID)
                 .Skip((productPage - 1) * PageSize)
                 .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                //LinQ show number of product in a category
                TotalItems = category == null ? productRepo.Products.Count()
                    : productRepo.Products.Where(e => e.Category == category).Count()
            }
        });
        [HttpPost]
        public IActionResult PlaceOrder()
        {
            //Use ModelState to show error in Checkout
            if (importItems.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your order is empty!");
            }
            //use ModelState to check validation in the Order.cs
            if (ModelState.IsValid)
            {
                ImportOrder importOrder = new ImportOrder(importItems);
                repository.SaveOrder(importOrder);
                return RedirectToAction("Index");
            }
            else
            {
                return View(null);
            }
        }
        // Doing
        public RedirectToActionResult AddToOrder(int productId, string returnUrl)
        {
            Product product = productRepo.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                importItems.AddItem(product, 1);
            }
            return RedirectToAction("Details", new { returnUrl });
        }
        public ViewResult Details(string returnUrl)
        {
            return View(new ImportDetailsViewModel
            {
                ImportItems = importItems,
                ReturnUrl = returnUrl
            });
        }
        // Need to do
        public ViewResult RemoveFromOrder() => View();
    }
}
