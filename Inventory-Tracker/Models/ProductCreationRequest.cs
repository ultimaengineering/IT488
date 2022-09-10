using System.ComponentModel.DataAnnotations;

namespace Inventory_Tracker.Models
{
    public class ProductCreationRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Isbn { get; set; }
        [Required]
        public int InStock { get; set; }
        [Required]
        public int Price { get; set; }
    }


}
