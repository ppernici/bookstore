using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.WebUI.Models;
using BookStore.WebUI.HtmlHelpers;
using BookStore.Domain.Abstract;
using BookStore.Domain.Concrete;
using BookStore.Domain.Entities;
using BookStore.WebUI.Controllers;
using BookStore.WebUI.Infrastructure;
using Moq;

namespace BookStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Tests the page links.
        /// </summary>
        [TestMethod]
        public void PageLinks()
        {
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo()
            {
                CurrentPage = 2,
                TotalItems = 17,
                ItemsPerPage = 10
            };

            Func<int, string> pageDelegate = i => "Page" + i;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>  " + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>  ", result.ToString());
        }
        /// <summary>
        /// Tests the view model for listing items on multiple pages.
        /// </summary>
        [TestMethod]
        public void PageVM()
        {
            Mock<IProductRepository> products = new Mock<IProductRepository>();

            products.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { Name = "P1" },
                new Product { Name = "P2" },
                new Product { Name = "P3" },
                new Product { Name = "P4" },
                new Product { Name = "P5" },
                new Product { Name = "P6" }
            });

            ProductController controller = new ProductController(products.Object);
            controller.PageSize = 3;

            ProductListViewModel result = (ProductListViewModel)controller.List(null, null, 2).Model;

            PagingInfo pi = result.PagingInfo;

            Assert.AreEqual(pi.CurrentPage, 2);
            Assert.AreEqual(pi.ItemsPerPage, 3);
            Assert.AreEqual(pi.TotalItems, 6);
            Assert.AreEqual(pi.TotalPages, 2);
        }
        /// <summary>
        /// Tests the filtering by category functionality.
        /// </summary>
        [TestMethod]
        public void FilterCategory()
        {
            Mock<IProductRepository> products = new Mock<IProductRepository>();
            products.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                new Product { ProductID = 2, Name = "P2", Category = "Plums" },
                new Product { ProductID = 3, Name = "P3", Category = "Plums" },
                new Product { ProductID = 4, Name = "P4", Category = "Oranges" },
                new Product { ProductID = 5, Name = "P5", Category = "Apples" },
                new Product { ProductID = 6, Name = "P6", Category = "Oranges" },
                new Product { ProductID = 7, Name = "P7", Category = "Plums" },
            });

            NavController target = new NavController(products.Object);

            IEnumerable<string> results1 = ((NavigationViewModel)target.Menu().Model).Categories;
            List<string> results = new List<string>();

            int count = 0;
            foreach (string x in results1)
            {
                results.Insert(count, x);
                ++count;
            }

            Assert.AreEqual(results.Count, 3);
            Assert.AreEqual(results[0], "Apples");
            Assert.AreEqual(results[1], "Oranges");
            Assert.AreEqual(results[2], "Plums");
        }
        /// <summary>
        /// Tests if a category that is selected is actually selected.
        /// </summary>
        [TestMethod]
        public void SelectedCategory()
        {
            Mock<IProductRepository> repository = new Mock<IProductRepository>();
            repository.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { Name = "Book1", Category = "Apples" },
                new Product { Name = "Book2", Category = "Oranges" }
            });

            NavController target = new NavController(repository.Object);

            string selection = "Oranges";
            string result = ((NavigationViewModel)target.Menu(selection).Model).CurrentCategory;

            Assert.AreEqual(selection, result);
        }
        /// <summary>
        /// Tests to make sure the product count (or page count) is correct.
        /// </summary>
        [TestMethod]
        public void ProductCountCategory()
        {
            Mock<IProductRepository> products = new Mock<IProductRepository>();
            products.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                new Product { ProductID = 2, Name = "P2", Category = "Plums" },
                new Product { ProductID = 3, Name = "P3", Category = "Plums" },
                new Product { ProductID = 4, Name = "P4", Category = "Oranges" },
                new Product { ProductID = 5, Name = "P5", Category = "Apples" },
                new Product { ProductID = 6, Name = "P6", Category = "Apples" },
                new Product { ProductID = 7, Name = "P7", Category = "Apples" },
            });

            // get a new controller and send it repository.
            ProductController controller = new ProductController(products.Object);

            // Set page size to three.
            controller.PageSize = 3;

            // get results.
            int result1 = ((ProductListViewModel)controller.List("Apples", null).Model).PagingInfo.TotalPages;
            int result2 = ((ProductListViewModel)controller.List("Oranges", null).Model).PagingInfo.TotalPages;
            int result3 = ((ProductListViewModel)controller.List("Plums", null).Model).PagingInfo.TotalPages;
            int result4 = ((ProductListViewModel)controller.List(null, null).Model).PagingInfo.TotalPages;

            Assert.AreEqual(result1, 2);
            Assert.AreEqual(result2, 1);
            Assert.AreEqual(result3, 1);
            Assert.AreEqual(result4, 3);
        }
    }
}