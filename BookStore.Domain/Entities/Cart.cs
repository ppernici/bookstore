using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    /// <summary>
    /// Class representing the shopping cart.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Collection of items in the cart.
        /// </summary>
        private List<CartLine> lineCollection = new List<CartLine>();

        // **************************************************************************************
        // PROPERTIES
        // **********

        /// <summary>
        /// Returns the collection of items.
        /// </summary>
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

        // **************************************************************************************
        // METHODS
        // *******

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Cart()
        { }
        /// <summary>
        /// Adds an item to the cart or adds quantity to an already present item.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void AddItem(Product product, int quantity)
        {
            // Get the appropriate line from the cart.
            CartLine thisLine = lineCollection
                                .Where(p => p.Product.ProductID == product.ProductID)
                                .FirstOrDefault();

            // Check if null, and if so, create a new line with that product.
            if (thisLine == null)
                lineCollection.Add(new CartLine(product, quantity));
            else // Add quantity to current item.
                thisLine.Quantity += quantity;
        }
        /// <summary>
        /// Removes a line from the cart completely.
        /// </summary>
        /// <param name="product"></param>
        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(p => p.Product.ProductID == product.ProductID);
        }
        /// <summary>
        /// Computes the total cost of items in the cart.
        /// </summary>
        /// <returns></returns>
        public decimal ComputeTotal()
        {
            decimal total = 0.00M;

            foreach (CartLine c in lineCollection)
                total += c.Quantity * c.Product.Price;

            return total;
        }
        /// <summary>
        /// Clears the cart of all items.
        /// </summary>
        public void ClearCart()
        {
            lineCollection.Clear();
        }
    }


    /// <summary>
    /// Class representing a single line item in the cart.
    /// </summary>
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public CartLine(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
