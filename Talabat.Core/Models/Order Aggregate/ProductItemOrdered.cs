namespace Talabat.Core.Models.Order_Aggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
            
        }

        public ProductItemOrdered(int productId, string productUrl, string productName)
        {
            ProductId = productId;
            ProductUrl = productUrl;
            ProductName = productName;
        }

        public int ProductId { get; set; }
        public string ProductUrl { get; set; }
        public string ProductName { get; set; }
    }
}