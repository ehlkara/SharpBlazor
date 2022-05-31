using System.ComponentModel.DataAnnotations;

namespace Sharp_Models
{
    public class OrderHeaderDto
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        // add navigation property : #TODO

        [Required]
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Shipping Date")]
        public DateTime ShippingDate { get; set; }

        [Required]
        public string Status { get; set; }

        //stripe payment
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string? Tracking { get; set; }
        public string? Carrier { get; set; }
    }
}
