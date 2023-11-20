using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specifications
{
    public class ProductWithFilterationForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterationForCountSpecification(ProductSpecParams specParams)
             : base (p =>
                    (string.IsNullOrEmpty(specParams.search) || p.Name.ToLower().Contains(specParams.search.ToLower())) &&
                    (!specParams.BrandId.HasValue || p.ProductBrandId == specParams.BrandId) &&
                    (!specParams.TypeId.HasValue  || p.ProductTypeId == specParams.TypeId))
        {

        }
    }
}
