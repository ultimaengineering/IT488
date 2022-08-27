using Inventory_Tracker.Entities;

namespace Inventory_Tracker.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Inventory_Tracker.Helpers;
    using Inventory_Tracker.Models;
    using Inventory_Tracker.Services;

    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private IInventoryService _inventoryService;
        private ILogger<ProductsController> _logger;

        public InventoryController(IInventoryService inventoryService, ILogger<ProductsController> logger)
        {
            _inventoryService = inventoryService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetProduct(Guid id)
        {
            var inventory = _inventoryService.GetInventory(id);
            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(inventory);
        }

        [Authorize]
        [HttpGet("all")]
        public IActionResult GetAllProduct()
        {
            var product = _inventoryService.GetInventory();
            return Ok(product);
        }


        [Authorize]
        [HttpPost]
        public IActionResult CreateProduct([FromBody] InventoryCreationRequest request)
        {
            var inventory = _inventoryService.CreateInventory(request);
            if (inventory == null)
            {
                return BadRequest("Item could not be created as described. Check requirements and try again.");
            }

            return Ok(inventory);
        }

        [Authorize]
        [HttpPut]
        public IActionResult UpdateProduct(Inventory inventory)
        {
            var updatedInventory = _inventoryService.UpdateInventory(inventory);
            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(updatedInventory);
        }


        [Authorize]
        [HttpDelete]
        public IActionResult DeleteProduct(Guid id)
        {
            _inventoryService.DeleteInventory(id);
            return Ok();
        }
    }
}
