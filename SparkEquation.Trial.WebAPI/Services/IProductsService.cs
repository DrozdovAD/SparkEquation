using System.Collections.Generic;
using System.Threading.Tasks;
using SparkEquation.Trial.WebAPI.Data.Models;

namespace SparkEquation.Trial.WebAPI.Services
{
    public interface IProductsService
    {
        Task<List<Product>> GetAllProductsDataAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task UpdateProductAsync(ProductMessage editedProductMsg);
        Task<int> AddProductAsync(ProductMessage productMsg);
        Task RemoveProductByIdAsync(int productId);
    }
}