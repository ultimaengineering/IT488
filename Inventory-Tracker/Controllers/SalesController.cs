using Inventory_Tracker.Entities;

namespace Inventory_Tracker.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Inventory_Tracker.Helpers;
    using Inventory_Tracker.Models;
    using Inventory_Tracker.Services;

    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {

        private ISalesService _salesService;
        private ILogger<SalesController> _logger;

        public SalesController(ISalesService salesService, ILogger<SalesController> logger)
        {
            _salesService = salesService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetSale(Guid id)
        {
            var product = _salesService.GetSale(id);
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
            var sales = _salesService.GetSales();
            return Ok(sales);
        }


        [Authorize]
        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateSaleRequest request)
        {
            var product = _salesService.CreateSale(request);
            if (product == null)
            {
                return BadRequest("Sale could not be created as described. Check requirements and try again.");
            }

            return Ok(product);
        }

        [Authorize]
        [HttpDelete]
        public IActionResult VoidSale(Guid id)
        {
            _salesService.VoidSale(id);
            return Ok();
        }

        [Authorize]
        [HttpGet("summary")]
        public IActionResult SalesSummary()
        {
            return Ok(_salesService.SalesSummary());
        }
    }
}
