using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Ninject;
using Moq;
using System.Web.Mvc;
using BookStore.Domain.Abstract;
using BookStore.Domain.Concrete;
using BookStore.Domain.Entities;

namespace BookStore.WebUI.Infrastructure
{
    /// <summary>
    /// Dependency resolver binds implementations to interfaces.
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        /// <summary>
        /// Default constructor declares dependency on IKernel type.
        /// </summary>
        /// <param name="kernel"></param>
        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }
        /// <summary>
        /// Binds concrete types to interfaces. Called in constructor.
        /// </summary>
        public void AddBindings()
        {
            // Bind the fake repository to the IProductRepository interface.
            kernel.Bind<IProductRepository>().To<EFProductRepository>();
            EmailSettings emailSettings = new EmailSettings()
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "true")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("emailSettings", emailSettings);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}
