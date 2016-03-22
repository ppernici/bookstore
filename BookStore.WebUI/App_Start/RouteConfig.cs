using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Simple list with no constraints route.
            routes.MapRoute(null,
                "",
                new
                {
                    controller = "Product", action = "List",
                    category = (string)null, author = (string)null,
                    page = 1
                }
            );

            // Page route.
            routes.MapRoute(
                null,
                "Page{page}",
                new
                {
                    controller = "Product", action = "List",
                    category = (string)null, author = (string)null
                },
                new { page = @"\d+" }
            );

            // Category route.
            routes.MapRoute(null,
                "{category}",
                new { controller = "Product", action = "List", author = (string)null, page = 1 }
            );

            // Category multiple pages.
            routes.MapRoute(null,
                "{category}/Page{page}",
                new { controller = "Product", action = "List", author = (string)null },
                new { category = @"[a-zA-z]+", page = @"\d+" }
            );

            // Category and author.
            routes.MapRoute(null,
                "{category}/{author}",
                new { controller = "Product", action = "List", page = 1 },
                new { category = @"[a-zA-z]+", author = @"[a-zA-Z]" }
            );

            // Category and author over multiple pages.
            routes.MapRoute(null,
                "{category}/{author}/Page{page}",
                new { controller = "Product", action = "List" },
                new { category = @"[a-zA-z]+", author = @"[a-zA-Z]", page = @"\d+" }
            );

            // Author route.
            routes.MapRoute(null,
                "{author}",
                new { controller = "Product", action = "List", category = (string)null, page = 1 }
            );

            // Author multiple pages.
            routes.MapRoute(null,
                "{author}/Page{page}",
                new { controller = "Product", action = "List", category = (string)null },
                new { author = @"[a-zA-Z]+", page = @"\d+" }
            );

            // Default route.
            routes.MapRoute("Default", "{controller}/{action}");
        }
    }
}
