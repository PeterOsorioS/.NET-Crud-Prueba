using Crud.Data;
using Crud.Models;
using Crud.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Crud.Repository
{
    public class ProductPurchaseRepository: Repository<ProductPurchase>, IProductPurchaseRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductPurchaseRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public ICollection<ProductPurchase> GetAllByIdUser(int userId)
        {
            var purchases = _db.purchases
                .Include(u => u.User)
                .Where(p => p.Id == userId)
                .ToList();
            return purchases;
        }
    }
}
