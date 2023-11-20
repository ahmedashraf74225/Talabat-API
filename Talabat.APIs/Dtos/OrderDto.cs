using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }

        public AddressDto ShippingAddress { get; set; }

        public int DeliveryMethodId { get; set; }
    }
}
