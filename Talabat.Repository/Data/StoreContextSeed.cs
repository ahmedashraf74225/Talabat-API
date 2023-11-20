using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext storeContext)
        {
            if (!storeContext.ProductBrands.Any())
            {
                var productBrands = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var brandsSerialized = JsonSerializer.Deserialize<List<ProductBrand>>(productBrands);

                if (brandsSerialized?.Count > 0)
                {
                    foreach (var item in brandsSerialized)
                    {
                        await storeContext.Set<ProductBrand>().AddAsync(item);
                    }
                }
                await storeContext.SaveChangesAsync();
               
            }
            if (!storeContext.ProductTypes.Any())
            {
                var productTypes = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var typesSerialized = JsonSerializer.Deserialize<List<ProductType>>(productTypes);

                if (typesSerialized?.Count > 0)
                {
                    foreach (var item in typesSerialized)
                    {
                        await storeContext.Set<ProductType>().AddAsync(item);
                    }
                }
                await storeContext.SaveChangesAsync();

            }
            if (!storeContext.Products.Any()) 
            {
                var products= File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var productsSerialized = JsonSerializer.Deserialize<List<Product>>(products);

                if (productsSerialized?.Count > 0)
                {
                    foreach (var item in productsSerialized)
                    {
                        await storeContext.Set<Product>().AddAsync(item);
                    }
                }
                await storeContext.SaveChangesAsync();
            }
            if (!storeContext.DeliveryMethods.Any())
            {
                var deliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                if (deliveryMethods?.Count > 0)
                {
                    foreach (var deliveryMethod in deliveryMethods)
                    {
                        await storeContext.Set<DeliveryMethod>().AddAsync(deliveryMethod);
                    }
                }
                await storeContext.SaveChangesAsync();
            }

        }
    }
}
