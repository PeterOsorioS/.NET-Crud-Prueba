using Crud.Data;
using Crud.Models;
using Crud.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Crud.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void update(Product product)
        {
            var objFromDb = _db.products.FirstOrDefault(p => p.Id == product.Id);
            objFromDb.Name = product.Name;
            objFromDb.Price = product.Price;
            objFromDb.Quantity = product.Quantity;
        }
        public Product GetProductsByNames(string name)
        {
            var product = _db.products.AsNoTracking().FirstOrDefault(p => p.Name == name);
            return product;
        }
    }
}
