using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Products> GetProductByIdAsync(int Id);
        Task<IReadOnlyList<Products>> GetListOfproduct();
        Task<IReadOnlyList<ProductType>> GetProductsTypeAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync();

    }
}
