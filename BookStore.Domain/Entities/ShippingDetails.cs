using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities
{
    /// <summary>
    /// Class that holds shipping details for an order.
    /// 
    /// Exceptions Thrown: none.
    /// </summary>
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Error: you must enter your name.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Error: you must enter an address.")]
        [Display(Name = "Address 1")]
        public string AddressLine1 { get; set; }
        [Display(Name = "Address 2")]
        public string AddressLine2 { get; set; }
        [Display(Name = "Address 3")]
        public string AddressLine3 { get; set; }

   
        [Required(ErrorMessage = "Error: you must enter a city.")]
        public string City { get; set; }


        [Required(ErrorMessage = "Error: you must enter a state or province.")]
        public string State { get; set; }

        
        public string Zip { get; set; }


        [Required(ErrorMessage = "Error: please enter a country.")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
