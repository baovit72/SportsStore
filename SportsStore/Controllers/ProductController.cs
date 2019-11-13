using System.Linq;
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
    }
}