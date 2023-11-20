using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Repository.Data.Config
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o=>o.Subtotal).HasColumnType("decimal(18,2)");

            builder.OwnsOne(o => o.ShippingAddress, s => s.WithOwner()); // mapping

            // {type of store in db , type of return}
            builder.Property(o => o.Status).HasConversion(
                oStatus => oStatus.ToString(),
                oStatus => (OrderStatus) Enum.Parse(typeof(OrderStatus), oStatus)
                );


        }
    }
}
