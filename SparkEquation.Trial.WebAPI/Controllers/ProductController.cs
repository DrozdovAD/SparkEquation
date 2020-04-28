using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SparkEquation.Trial.WebAPI.Data.Models;
using SparkEquation.Trial.WebAPI.Services;

namespace SparkEquation.Trial.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _productsService;
        
        public ProductController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = _productsService.GetAllProductsData();
            return new JsonResult(results);
        }
    }
}