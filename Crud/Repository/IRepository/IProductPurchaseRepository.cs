using Crud.Models;

namespace Crud.Repository.IRepository
{
    public interface IProductPurchaseRepository: IRepository<ProductPurchase>
    {
        public ICollection<ProductPurchase> GetAllByIdUser(int userId);
    }
}
