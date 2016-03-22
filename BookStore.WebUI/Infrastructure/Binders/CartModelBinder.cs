using System;
using System.Web.Mvc;
using BookStore.Domain.Entities;

namespace BookStore.WebUI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Get cart from the session.
            Cart cart = null;
            if (controllerContext.HttpContext.Session != null)
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            
            // If no cart exists, then create a new one.
            if(cart == null)
            {
                cart = new Cart();
                if(controllerContext.HttpContext.Session != null)
                    controllerContext.HttpContext.Session[sessionKey] = cart;
            }

            return cart;
        }
    }
}
