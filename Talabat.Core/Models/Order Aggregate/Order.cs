using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
        }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; } // forigen key will be generated automatic by EntityFrameWork.
        public ICollection<OrderItem> Items { get; set;} = new HashSet<OrderItem>();

        public decimal Subtotal { get; set; }  // order fees Except Delivery Cost.


        public string PaymentIntendId { get; set; } = string.Empty;

        public decimal GetTotal() => Subtotal + DeliveryMethod.Cost; // derived will not mapped in database.


    }
}
