using Microsoft.Extensions.Logging;
using Skinet.Core.Entities;
using Skinet.Infrastracture.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Skinet.Infrastracture.SeedData
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext storeContext, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!storeContext.ProductBrands.Any())
                {
                    var branddata = File.ReadAllText("../Skinet.Infrastracture/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(branddata);
                    foreach (var brandItem in brands)
                    {
                        storeContext.ProductBrands.AddAsync(brandItem);
                    }
                    await storeContext.SaveChangesAsync();
                }
                if (!storeContext.ProductTypes.Any())
                {
                    var Typesdata = File.ReadAllText("../Skinet.Infrastracture/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(Typesdata);

                    foreach (var BrandTypes in types)
                    {
                        storeContext.ProductTypes.AddAsync(BrandTypes);
                    }
                    await storeContext.SaveChangesAsync();
                }
                if (!storeContext.Products.Any())
                {
                    var productsdata = File.ReadAllText("../Skinet.Infrastracture/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Products>>(productsdata);

                    foreach (var productsItems in products)
                    {
                        storeContext.Products.AddAsync(productsItems);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, "Something went wrong with your request");
            }
        }
    }
}
