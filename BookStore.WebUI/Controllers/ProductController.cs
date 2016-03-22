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
    public class ProductController : Controller
    {
        private IProductRepository repository;
        private int pageSize = 4;

        public int PageSize
        {
            get { return pageSize; }
            set
            {
                if (value >= 1)
                    pageSize = value;
                else
                    throw new ArgumentOutOfRangeException("Error: page size must be at least 1.");
            }
        }

        /// <summary>
        /// Default constructor takes an IRepository type.
        /// </summary>
        /// <param name="repository"></param>
        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Returns a view with Products.
        /// </summary>
        /// <returns></returns>
        public ViewResult List(string category, string author, int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repository.Products
                                     .Where(p => (category == null || p.Category == category))
                                     .Where(p => (author == null || author == p.Author))
                                     .OrderBy(p => p.Price)
                                     .Skip((page - 1) * PageSize)
                                     .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = 
                            (category == null) ? 
                            repository.Products.Count() : 
                            repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }
    }
}