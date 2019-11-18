using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }
        //PAGINATION
        //public ViewResult List(int productPage = 1)
        // => View(repository.Products.OrderBy(p => p.ProductID)
        // .Skip((productPage - 1) * PageSize).Take(PageSize));

        //Call the List.cshtl and passing Product+Paging Info
        public ViewResult List(string category, int productPage = 1)
        => View(new ProductsListViewModel
        {
            //LinQ select product 
            Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                //LinQ show number of product in a category
                TotalItems = category == null ? repository.Products.Count() 
                :repository.Products.Where(e =>e.Category == category).Count()
            }
        });
        public RedirectResult Login(string loginUrl = "/Account/Login")
        {
            return Redirect(loginUrl);
        }
        [Authorize]
        public ViewResult AdminList() => View(repository.Products);
        //View action for tag-helper asp-action="Edit"
        [Authorize]
        public ViewResult Edit(int productId) => View(repository
            .Products.FirstOrDefault(p => p.ProductID == productId));
        //
        [Authorize]
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
        [Authorize]
        public ViewResult Create() => View("Edit", new Product());
        [Authorize]
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
    }
}