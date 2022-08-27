using System.ComponentModel.DataAnnotations;

namespace Inventory_Tracker.Models
{
    public class ProductCreationRequest
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public String Description { get; set; }
    }


}
