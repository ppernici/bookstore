// General stuff ------ ------------ ------------------ -----------------------
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

// Domain project ------------    ---------------- ----------------- ----------------------
using BookStore.Domain.Abstract;
using BookStore.Domain.Concrete;
using BookStore.Domain.Entities;

// WebUI project ------------ ------------ -------------- -------------
using BookStore.WebUI.Controllers;
using BookStore.WebUI.Infrastructure;
using BookStore.WebUI.Infrastructure.Binders;
using BookStore.WebUI.Models;
using BookStore.WebUI.HtmlHelpers;

namespace BookStore.UnitTests
{
    // ******************************************************************************************
    // SHOPPING CART TESTS
    // *******************
    [TestClass]
    public class CartTests
    {
        /// <summary>
        /// Tests the add item method.
        ///</summary>
        [TestMethod]
        public void TestCartAdd()
        {
            Product p1 = new Product { Name = "Book1", ProductID = 1 };
            Product p2 = new Product { Name = "Book2", ProductID = 2 };

            Cart myCart = new Cart();

            myCart.AddItem(p1, 2);
            myCart.AddItem(p2, 1);
            CartLine[] results = ((List<CartLine>)myCart.Lines).ToArray();

            Assert.AreEqual(results[0].Product.Name, "Book1");
            Assert.AreEqual(results[0].Quantity, 2);
            Assert.AreEqual(results[1].Product.Name, "Book2");
            Assert.AreEqual(results[1].Quantity, 1);

            myCart.AddItem(p1, 3);
            results = ((List<CartLine>)myCart.Lines).ToArray();

            Assert.AreEqual(results[0].Quantity, 5);
            Assert.AreEqual(results.Length, 2);
        }
        /// <summary>
        /// Tests the remove item method.
        /// </summary>
        [TestMethod]
        public void TestCartRemove()
        {
            Product p1 = new Product { Name = "Book1", ProductID = 1 };
            Product p2 = new Product { Name = "Book2", ProductID = 2 };

            Cart myCart = new Cart();

            myCart.AddItem(p1, 2);
            myCart.AddItem(p2, 1);
            myCart.RemoveLine(p1);

            CartLine[] results = ((List<CartLine>)myCart.Lines).ToArray();

            Assert.AreEqual(results.Length, 1);
            Assert.AreEqual(results[0].Product.Name, "Book2");
            Assert.AreEqual(results[0].Quantity, 1);
        }
        /// <summary>
        /// Tests the calculation of the total items in cart.
        /// </summary>
        [TestMethod]
        public void TestCartTotal()
        {
            // Create cart.
            Cart myCart = new Cart();

            // Create 10 products at $10.
            Product[] prods = new Product[10];
            for (int i = 0; i < 10; ++i)
            {
                prods[i] = new Product { Price = 10M, ProductID = i };
                myCart.AddItem(prods[i], 1);
            }

            decimal total = myCart.ComputeTotal();

            Assert.AreEqual(total, 100M);
        }
        /// <summary>
        /// Test that it is possible to clear cart.
        /// </summary>
        [TestMethod]
        public void TestCartClear()
        {
            // Get new cart.
            Cart myCart = new Cart();

            // Put ten items in cart.
            Product[] prods = new Product[10];
            for (int i = 0; i < 10; ++i)
            {
                prods[i] = new Product { ProductID = i };
                myCart.AddItem(prods[i], 1);
            }

            // Try to clear cart.
            myCart.ClearCart();

            // Get results.
            CartLine[] result = ((List<CartLine>)myCart.Lines).ToArray();

            // Check that results is empty.
            Assert.AreEqual(result.Length, 0);

        }
        /// <summary>
        /// Tests the add functionality using the cart controller instead of the cart class.
        /// </summary>
        [TestMethod]
        public void AddAgain()
        {
            Mock<IProductRepository> repository = new Mock<IProductRepository>();
            repository.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product { Name = "P1", ProductID = 1, Category = "Apples" }
                }.AsQueryable()
            );

            // Create a new cart.
            Cart cart = new Cart();

            // Create controller.
            CartController controller = new CartController(repository.Object);

            // Add item to cart.
            controller.AddToCart(cart, 1, null);

            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ElementAt(0).Product.ProductID, 1);
            Assert.AreEqual(cart.Lines.ElementAt(0).Product.Name, "P1");
            Assert.AreEqual(cart.Lines.ElementAt(0).Product.Category, "Apples");
        }
        /// <summary>
        /// Tests that when an item is added it goes to the cart screen.
        /// </summary>
        [TestMethod]
        public void CartScreen()
        {
            // Setup repo.
            Mock<IProductRepository> repository = new Mock<IProductRepository>();
            repository.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product() {Name = "P1", ProductID = 1, Category = "Apples" }
                }.AsQueryable()
            );

            // Setup cart and controller.
            Cart cart = new Cart();
            CartController controller = new CartController(repository.Object);

            // call function to add to cart.
            RedirectToRouteResult result = controller.AddToCart(cart, 1, "myURL");

            // Check result.
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myURL");
        }
        /// <summary>
        /// Tests that contents of cart can be viewed.
        /// </summary>
        [TestMethod]
        public void ViewContents()
        {
            // Create new cart and controller with no repository.
            Cart cart = new Cart();
            CartController controller = new CartController(null);

            CartIndexViewModel model = (CartIndexViewModel)controller.Index(cart, "myURL").ViewData.Model;

            Assert.AreSame(model.Cart, cart);
            Assert.AreEqual(model.ReturnURL, "myURL");
        }
    }
}
