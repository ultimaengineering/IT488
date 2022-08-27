using Inventory_Tracker.DAL;
using Inventory_Tracker.Entities;
using Inventory_Tracker.Models;

namespace Inventory_Tracker.Services
{

    public interface IInventoryService
    {
        Inventory GetInventory(Guid id);

        IEnumerable<Inventory> GetInventory();
        Inventory CreateInventory(InventoryCreationRequest product);
        Inventory UpdateInventory(Inventory product);
        void DeleteInventory(Guid id);
    }
    public class InventoryService : IInventoryService
    {

        private DbContext _db;
        private ILogger<InventoryService> _logger;

        public InventoryService(DbContext db, ILogger<InventoryService> logger
        )
        {
            _db = db;
            _logger = logger;
        }


        public Inventory GetInventory(Guid id)
        {
            _logger.LogTrace("Received product lookup request for {}", id);
            return _db.Inventory.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Inventory> GetInventory()
        {
            return _db.Inventory;
        }

        public Inventory CreateInventory(InventoryCreationRequest inventory)
        {
            var inventoryEntity = new Inventory
            {
                ProductId = inventory.ProductId,
                Stock = inventory.Stock,
            };

            _db.Inventory.Add(inventoryEntity);
            _db.SaveChanges();
            return inventoryEntity;
        }

        public Inventory UpdateInventory(Inventory inventory)
        {
            _db.Inventory.Add(inventory);
            _db.SaveChanges();
            return inventory;
        }

        public void DeleteInventory(Guid id)
        {
            var inventory = _db.Inventory.FirstOrDefault(x => x.Id == id);
            if (inventory != null)
            {
                _db.Inventory.Remove(inventory);
                _db.SaveChanges();
            }
        }
    }
}
