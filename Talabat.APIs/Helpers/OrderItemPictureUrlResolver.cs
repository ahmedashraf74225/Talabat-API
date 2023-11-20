using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.APIs.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!String.IsNullOrEmpty(source.Product.ProductUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.Product.ProductUrl}";
            return String.Empty;
        }
    }
}
