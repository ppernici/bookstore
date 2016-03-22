using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Domain.Entities;
using BookStore.Domain.Abstract;
using BookStore.Domain.Concrete;
using BookStore.WebUI.Models;

namespace BookStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        /// <summary>
        /// Constructor declares dependency on IProductRepository type.
        /// </summary>
        /// <param name="repository"></param>
        public NavController(IProductRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Generates a menu with categories. 
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Menu(string category = null, bool horizontalLayout = false)
        {
            IEnumerable<string> categories = repository.Products
                                                       .Select(x => x.Category)
                                                       .Distinct()
                                                       .OrderBy(x => x);

            NavigationViewModel navigator = new NavigationViewModel()
            {
                CurrentCategory = category,
                Categories = categories
            };

            // Send the correct name so that layout will be correct for device.
            string viewName = horizontalLayout ? "MenuHorizontal" : "Menu";

            return PartialView(viewName, navigator);
        }
    }
}