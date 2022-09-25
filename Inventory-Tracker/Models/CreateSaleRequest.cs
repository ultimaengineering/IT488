namespace Inventory_Tracker.Models
{
    public class CreateSaleRequest
    {
        public Guid? ProductId { get; set; }

        public Decimal? SalesPrice { get; set; }
    }
}
