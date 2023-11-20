using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product>
    {
        // get all products with Brands and Types
        public ProductWithBrandAndTypeSpecifications(ProductSpecParams specParams)
            :base(p =>
                (string.IsNullOrEmpty(specParams.search) || p.Name.ToLower().Contains(specParams.search.ToLower())) &&
                (!specParams.BrandId.HasValue || p.ProductBrandId == specParams.BrandId) &&
                (!specParams.TypeId.HasValue || p.ProductTypeId == specParams.TypeId)
            )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            AddOrderBy(p => p.Name);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch(specParams.Sort) 
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

            // totalProducts = 100
            // pageSize = 10
            // page index = 3

            ApplyPagination((specParams.PageSize * (specParams.PageIndex - 1)), specParams.PageSize);
        }

        // get a product with Id
        public ProductWithBrandAndTypeSpecifications(int id):base(p=>p.Id==id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

        }
    }
}
