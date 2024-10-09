using Crud.Models;

namespace Crud.Repository.IRepository
{
    public interface IUserRepository:IRepository<User>
    {
        public User GetByEmail(string email);
        public void update(User user);
    }
}
