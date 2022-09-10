using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Tracker.Entities
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string? Name { get; set; }
        
        [Required]
        public string? Description { get; set; }
        
        [Required]
        public string? Isbn { get; set; }
        
        [Required]
        public int? InStock { get; set; }
        
        [Required]
        public int? Price { get; set; }


    }
}
