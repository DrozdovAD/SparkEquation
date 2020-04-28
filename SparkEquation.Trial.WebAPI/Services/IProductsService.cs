using System.Collections.Generic;
using SparkEquation.Trial.WebAPI.Data.Models;

namespace SparkEquation.Trial.WebAPI.Services
{
    public interface IProductsService
    {
        List<Product> GetAllProductsData();
    }
}