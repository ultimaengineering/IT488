using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Tracker.Entities
{
    public class SalesSummary
    {
        public int year { get; set; }
        public Decimal sales { get; set; }
        public int items_sold {get; set;}
        public string? closing_month_name { get; set; }
    }
}
