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

        /// <summary>
        /// Return all products
        /// </summary>
        /// <returns>Products collection</returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _productsService.GetAllProductsDataAsync());
        }

        /// <summary>
        /// Return Product by an Id
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Product</returns>
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByIdAsync(int productId)
        {
            return Ok(await _productsService.GetProductByIdAsync(productId));
        }

        /// <summary>
        /// Update an article
        /// </summary>
        /// <param name="editedProduct">Edited product</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> EditAsync(ProductMessage editedProductMsg)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _productsService.UpdateProductAsync(editedProductMsg);
            return Ok();
        }

        /// <summary>
        /// Submits new product
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ProductMessage product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _productsService.AddProductAsync(product));
        }

        /// <summary>
        /// Remove product by an Id
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns></returns>
        [HttpDelete("{productId}")]
        public async Task RemoveByIdAsync(int productId)
        {
            await _productsService.RemoveProductByIdAsync(productId);
        }
    }
}