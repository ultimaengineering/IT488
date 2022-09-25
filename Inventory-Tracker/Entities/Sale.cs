using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Tracker.Entities
{
    public class Sale
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid? ProductId { get; set; }
        
        [Required]
        public Decimal? SalesPrice { get; set; }
        
        [Required]
        public DateTime? TimeOfSale { get; set; }
    }
}
