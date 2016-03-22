using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entities;

namespace BookStore.Domain.Abstract
{
    /// <summary>
    /// Abstract repository
    /// </summary>
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
}
