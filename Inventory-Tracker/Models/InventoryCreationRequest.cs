using System.ComponentModel.DataAnnotations;

namespace Inventory_Tracker.Models
{
    public class InventoryCreationRequest
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public Int32 Stock { get; set; }

    }
}
