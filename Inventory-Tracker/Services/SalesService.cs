using Inventory_Tracker.DAL;
using Inventory_Tracker.Entities;
using Inventory_Tracker.Models;

namespace Inventory_Tracker.Services
{
    public interface ISalesService
    {
        Sale GetSale(Guid id);
        IEnumerable<Sale> GetSales();
        Sale CreateSale (CreateSaleRequest request);
        void VoidSale(Guid id);
        IEnumerable<SalesSummary>? SalesSummary();
    }


    public class SalesService : ISalesService
    {
        private DbContext _db;
        private ILogger<SalesService> _logger;

        public SalesService(DbContext db, ILogger<SalesService> logger
        )
        {
            _db = db;
            _logger = logger;
        }

        public Sale? GetSale(Guid id)
        {
            _logger.LogTrace("Received product lookup request for {}", id);
            return _db.Sales.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Sale> GetSales()
        {
            return _db.Sales;
        }

        public Sale CreateSale(CreateSaleRequest request)
        {
            var saleEntity = new Sale
            {
                ProductId = request.ProductId,
                SalesPrice = request.SalesPrice,
            };

            _db.Sales.Add(saleEntity);
            _db.SaveChanges();
            return saleEntity;
        }

        public void VoidSale(Guid id)
        {
            var sale = _db.Sales.FirstOrDefault(x => x.Id == id);
            if (sale != null)
            {
                _db.Sales.Remove(sale);
                _db.SaveChanges();
            }
        }

        public IEnumerable<SalesSummary>? SalesSummary()
        {
            return _db.SalesSummaries;
        }
    }
}
