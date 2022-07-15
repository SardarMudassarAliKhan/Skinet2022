using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skinet.Core.Specifications
{
    public class ProductWithFilterCountSpecification : BaseSpecification<Products>
    {
        public ProductWithFilterCountSpecification(ProductSpecPrams productSpecPrams) :
            base      
            (x =>
            (string.IsNullOrEmpty(productSpecPrams.Search) || x.Name == productSpecPrams.Search) &&
            (!productSpecPrams.brandId.HasValue || x.ProductBrandId == productSpecPrams.brandId)
             && (!productSpecPrams.typeId.HasValue || x.ProductTypeId == productSpecPrams.typeId))
        {

        }
    }
}
