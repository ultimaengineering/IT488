using Inventory_Tracker.DAL;
using Inventory_Tracker.Entities;
using Inventory_Tracker.Models;

namespace Inventory_Tracker.Services
{
    public interface IProductService
    {
        Product GetProduct(Guid id);

        IEnumerable<Product> GetProducts();
        Product CreateProduct(ProductCreationRequest product);
        Product UpdateProduct(Product product);
        void DeleteProduct(Guid id);
    }


    public class ProductService : IProductService
    {
        private DbContext _db;
        private ILogger<ProductService> _logger;

        public ProductService(DbContext db, ILogger<ProductService> logger
        )
        {
            _db = db;
            _logger = logger;
        }

        public Product? GetProduct(Guid id)
        {
            _logger.LogTrace("Received product lookup request for {}", id);
            return _db.Products.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _db.Products;
        }

        public Product CreateProduct(ProductCreationRequest product)
        {
            var productEntity = new Product
            {
                Name = product.Name,
                Description = product.Description,

            };

            _db.Products.Add(productEntity);
            _db.SaveChanges();
            return productEntity;
        }

        public Product UpdateProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public void DeleteProduct(Guid id)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
        }
    }
}
