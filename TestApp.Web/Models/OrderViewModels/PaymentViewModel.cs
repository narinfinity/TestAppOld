using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestApp.Core.Entity.Domain;

namespace TestApp.Web.Models.OrderViewModels
{
    public class PaymentViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(128)]
        public string Street { get; set; }
        [Required]
        [StringLength(128)]
        public string City { get; set; }
        [Required]
        [StringLength(128)]
        public string State { get; set; }
        [Required]
        [StringLength(128)]
        public string Country { get; set; }
        
        [StringLength(128)]
        public string PostalCode { get; set; }

        public ICollection<OrderedProduct> OrderedProducts { get; set; }
    }
}
