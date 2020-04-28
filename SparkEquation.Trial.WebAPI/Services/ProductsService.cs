using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SparkEquation.Trial.WebAPI.Data;
using SparkEquation.Trial.WebAPI.Data.Factory;
using SparkEquation.Trial.WebAPI.Data.Models;

namespace SparkEquation.Trial.WebAPI.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IContextFactory _factory;
        private readonly IMapper _mapper;

        public ProductsService(IContextFactory contextFactory)
        {
            _factory = contextFactory;

            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<ProductMessage, Product>()
                .ForMember(dest => dest.CategoryProducts, opt =>
                    opt.MapFrom(pm => pm.CategoryIds.Select(id =>
                        new CategoryProduct() { ProductId = pm.Id, CategoryId = id })))
            );

            _mapper = config.CreateMapper();
        }

        public async Task<List<Product>> GetAllProductsDataAsync()
        {
            using (var context = _factory.GetContext())
            {
                return await context.Products.ToListAsync();
            }
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            using (var context = _factory.GetContext())
            {
                var result = await context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.CategoryProducts)
                        .ThenInclude(c => c.Category)
                    .SingleOrDefaultAsync(p => p.Id == productId);

                result.Brand.Products = null;

                foreach (CategoryProduct cp in result.CategoryProducts)
                {
                    cp.Product = null;
                    cp.Category.CategoryProducts = null;
                }

                return result;
            }
        }

        public async Task UpdateProductAsync(ProductMessage editedProductMsg)
        {
            using (var context = _factory.GetContext())
            {
                if (editedProductMsg == null)
                {
                    throw new ArgumentNullException();
                }

                var product = await context.Products.SingleOrDefaultAsync(a => a.Id == editedProductMsg.Id);

                if (product == null)
                {
                    throw new ArgumentException($"{editedProductMsg.Id} is wrong Id");
                }

                var editedProduct = _mapper.Map<Product>(editedProductMsg);
                context.Entry(product).CurrentValues.SetValues(editedProduct);
                await context.SaveChangesAsync();
            }
        }

        public async Task<int> AddProductAsync(ProductMessage productMsg)
        {
            using (var context = _factory.GetContext())
            {
                if (productMsg == null)
                {
                    throw new ArgumentNullException();
                }

                var product = _mapper.Map<Product>(productMsg);

                var e = context.Products.Add(product);
                await context.SaveChangesAsync();
                return product.Id;
            }
        }

        public async Task RemoveProductByIdAsync(int productId)
        {
            using (var context = _factory.GetContext())
            {
                var product = await context.Products.SingleOrDefaultAsync(a => a.Id == productId);

                if (product == null)
                {
                    throw new ArgumentException($"{productId} is wrong Id");
                }

                context.Products.Remove(product);
                
                var brandsToRemove = context.Brands.Where(b => b.Products.Any(p => p.Id == productId));
                var categoriesToRemove = context.CategoryProducts.Where(cp => cp.ProductId == productId);
                context.RemoveRange(brandsToRemove, categoriesToRemove);

                await context.SaveChangesAsync();
            }
        }
    }
}