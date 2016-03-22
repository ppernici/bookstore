using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using BookStore.WebUI.Models;

namespace BookStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        /// <summary>
        /// Constructor declaring dependence upon IPRoductRepository type.
        /// </summary>
        /// <param name="repository"></param>
        public CartController(IProductRepository repository, IOrderProcessor orderProcessor)
        {
            this.repository = repository;
            this.orderProcessor = orderProcessor;
        }
        /// <summary>
        /// Adds an item to the cart.
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="returnURL"></param>
        /// <returns></returns>
        public RedirectToRouteResult AddToCart(Cart cart, int productID, string returnURL)
        {
            Product product = repository.Products
                                        .Where(x => x.ProductID == productID)
                                        .FirstOrDefault();
            if (product != null)
                cart.AddItem(product, 1);

            //return RedirectToAction("Index", new { ReturnURL = "debugging" });
            return RedirectToAction("Index", new { returnURL });
        }
        /// <summary>
        /// Method to remove an item from the cart.
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="returnURL"></param>
        /// <returns></returns>
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productID, string returnURL)
        {
            // First figure out the product.
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);

            // Then remove from the cart.
            if (product != null)
                cart.RemoveLine(product);

            return RedirectToAction("Index", new { returnURL });
        }
        /// <summary>
        /// Page returned to after messing with cart.
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        public ViewResult Index(Cart cart, string returnURL)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnURL = returnURL
            });
        }
        /// <summary>
        /// Used for summarizing the cart.
        /// </summary>
        /// <param name="cart"></param>
        /// <returns>View(cart)</returns>
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        /// <summary>
        /// Used for checkout.
        /// </summary>
        /// <returns></returns>
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        /// <summary>
        /// Checks cart and if it is not empty and if shipping details are good, sends email with order. Otherwise gives error message.
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="shippingDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
                ModelState.AddModelError("", "You have an empty cart! Go order something!");

            // Check that all shipping details are okay.
            if (ModelState.IsValid)
            {
                // Send order.
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.ClearCart();
                // Send message to user.
                return View("Completed");
            }
            else
                return View(shippingDetails);
        }
    }
}