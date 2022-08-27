using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Tracker.Entities
{
    public class Inventory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        [Required]
        [InverseProperty("Id")]
        public Product Product { get; set; }

        public Int32 Stock { get; set; }
    }
}
