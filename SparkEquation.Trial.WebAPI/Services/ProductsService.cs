using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SparkEquation.Trial.WebAPI.Data;
using SparkEquation.Trial.WebAPI.Data.Factory;
using SparkEquation.Trial.WebAPI.Data.Models;

namespace SparkEquation.Trial.WebAPI.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IContextFactory _factory;
        
        public ProductsService(IContextFactory contextFactory)
        {
            _factory = contextFactory;
        }

        public List<Product> GetAllProductsData()
        {
            using (var context = _factory.GetContext())
            {
                return context.Products.ToList();
            }
        }
    }
}