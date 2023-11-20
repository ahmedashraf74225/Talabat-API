using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.APIs.Dtos
{
    public class OrderToReturnDto
    {

        public int Id { get; set; }
        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; }

        public Address ShippingAddress { get; set; }

        public string DeliveryMethodName { get; set; } 
        public decimal DeliveryMethodCost { get; set; } 

        public ICollection<OrderItemDto> Items { get; set; }

        public decimal Subtotal { get; set; } 

        public string PaymentIntendId { get; set; } = string.Empty;

        public decimal Total { get; set; } 
    }
}
