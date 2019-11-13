using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    //public class CartController : Controller
    //{
    //    private IProductRepository repository;
    //    public CartController(IProductRepository repo)
    //    {
    //        repository = repo;
    //    }
    //    public ViewResult Index(string returnUrl)
    //    {
    //        return View(new CartIndexViewModel
    //        {
    //            // retrieves the Cart object from the session state
    //            Cart = GetCart(),
    //            ReturnUrl = returnUrl
    //        });
    //    }
    //    //RedirectToActionResult sending an HTTP redirect instruction to the client browser, asking the browser to request a new URL
    //    public RedirectToActionResult AddToCart(int productId, string returnUrl)
    //    {
    //        Product product = repository.Products
    //        .FirstOrDefault(p => p.ProductID == productId);
    //        if (product != null)
    //        {
    //            Cart cart = GetCart();
    //            cart.AddItem(product, 1);
    //            SaveCart(cart);
    //            //cart = GetCart();
    //        }
    //        return RedirectToAction("Index", new { returnUrl });
    //    }
    //    public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
    //    {
    //        Product product = repository.Products
    //        .FirstOrDefault(p => p.ProductID == productId);
    //        if (product != null)
    //        {
    //            Cart cart = GetCart();
    //            cart.RemoveLine(product);
    //            SaveCart(cart);
    //        }
    //        return RedirectToAction("Index", new { returnUrl });
    //    }
    //    // store and retrieve Cart objects using Sesstion State: associates data with as ession
    //    // each user have own session => cart persistent between request
    //    private Cart GetCart()
    //    {
    //        Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
    //        return cart;
    //    }
    //    private void SaveCart(Cart cart)
    //    {
    //        HttpContext.Session.SetJson("Cart", cart);
    //    }
    //}
    public class CartController : Controller
    {
        private IProductRepository repository;
        private Cart cart;
        //using Dependency Injection to hide Cart object implement
        public CartController(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int productId,
        string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
