using Microsoft.AspNetCore.Mvc;
using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Skinet.Core.Specifications
{
    public class ProductWithSpecificationTypesAndBrand : BaseSpecification<Products>
    {
        
        public ProductWithSpecificationTypesAndBrand([FromQuery]ProductSpecPrams productSpecPrams)
            : base
            (x =>
            (string.IsNullOrEmpty(productSpecPrams.Search) || x.Name == productSpecPrams.Search) &&
            (!productSpecPrams.brandId.HasValue || x.ProductBrandId == productSpecPrams.brandId)
             && (!productSpecPrams.typeId.HasValue || x.ProductTypeId == productSpecPrams.typeId))
        {
          AddInclude(Y=>Y.ProductType);
          AddInclude(Y=>Y.ProductType);
          AddInclude(Z=>Z.ProductBrand);
          AddOrderBy(x=>x.Name);
          ApplyPagging(productSpecPrams.pageSize*(productSpecPrams.pageIndex),productSpecPrams.pageSize);
            if(string.IsNullOrEmpty(productSpecPrams.sort))
            {
                switch(productSpecPrams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDecending(p=> p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public ProductWithSpecificationTypesAndBrand(int Id) 
            : base(x=>x.Id==Id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
