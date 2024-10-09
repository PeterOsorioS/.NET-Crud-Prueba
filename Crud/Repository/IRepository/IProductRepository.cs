using Crud.Models;

namespace Crud.Repository.IRepository
{
    public interface IProductRepository: IRepository<Product>
    {
        public void update(Product product);
        public Product GetProductsByNames(string name);
    }
}
