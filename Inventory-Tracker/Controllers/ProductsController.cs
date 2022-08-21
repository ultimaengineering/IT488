using Inventory_Tracker.Entities;

namespace Inventory_Tracker.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Inventory_Tracker.Helpers;
    using Inventory_Tracker.Models;
    using Inventory_Tracker.Services;

    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {

        private IProductService _productService;
        private ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetProduct(Guid id)
        {
            var product = _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [Authorize]
        [HttpGet("all")]
        public IActionResult GetAllProduct()
        {
            var product = _productService.GetProducts();
            return Ok(product);
        }


        [Authorize]
        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductCreationRequest request)
        {
            var product = _productService.CreateProduct(request);
            if (product == null)
            {
                return BadRequest("Item could not be created as described. Check requirements and try again.");
            }

            return Ok(product);
        }

        [Authorize]
        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            var updatedProduct = _productService.UpdateProduct(product);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(updatedProduct);
        }


        [Authorize]
        [HttpDelete]
        public IActionResult DeleteProduct(Guid id)
        { 
            _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
