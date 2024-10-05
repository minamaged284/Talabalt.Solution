using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public class StoreDbContextSeed
    {
        public static async Task seedAsync(StoreDbContext _dbContext)
        {
            var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            brands=brands.Select(b => new ProductBrand() {Name=b.Name}).ToList();
            if (brands.Count>0)
            {
                if (_dbContext.Set<ProductBrand>().Count() == 0)
                {
                    foreach (var brand in brands)
                    {
                        await _dbContext.Set<ProductBrand>().AddAsync(brand);
                        await _dbContext.SaveChangesAsync();

                    }
                }

            }



           
            var categoriesData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
            var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
            categories = categories.Select(b => new ProductCategory() { Name = b.Name }).ToList();
            if (categories.Count > 0)
            {
                if (_dbContext.Set<ProductCategory>().Count() == 0)
                {
                    foreach (var categorie in categories)
                    {
                        await _dbContext.Set<ProductCategory>().AddAsync(categorie);
                        await _dbContext.SaveChangesAsync();
                    }
                }

            }




            var productsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
            var products = JsonSerializer.Deserialize<List<Products>>(productsData);
            products = products.Select(p => new Products() { Name = p.Name, BrandId = p.BrandId, Description = p.Description, PictureUrl = p.PictureUrl, Price = p.Price, CategoryId = p.CategoryId, Brand = p.Brand, Category = p.Category }).ToList();
            if (products.Count > 0)
            {
                if (_dbContext.Set<Products>().Count() == 0)
                {
                    foreach (var product in products)
                    {
                        await _dbContext.Set<Products>().AddAsync(product);
                        await _dbContext.SaveChangesAsync();
                    }
                }

            }

        }
    }
}
