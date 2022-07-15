using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Infrastracture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastracture.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public StoreContext _StoreContext;
        public ProductRepository(StoreContext storeContext)
        {
            _StoreContext = storeContext;
        }
        public async Task<IReadOnlyList<Products>> GetListOfproduct()
        { 
            return  _StoreContext.Products
                .Include(p=>p.ProductType)
                .Include(b=>b.ProductBrand)
                .ToList();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
            return _StoreContext.ProductBrands.ToList();
        }

        public async Task<Products> GetProductByIdAsync(int Id)
        {
            var obj = await _StoreContext.Products
                .Include(p => p.ProductType)
                .Include(b => b.ProductBrand)
                 .FirstOrDefaultAsync(x=>x.Id==Id);
            return obj;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductsTypeAsync()
        {
            return _StoreContext.ProductTypes.ToList();
        }
    }
}
