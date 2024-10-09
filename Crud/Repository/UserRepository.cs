using Crud.Data;
using Crud.Models;
using Crud.Repository.IRepository;

namespace Crud.Repository
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db):base(db) 
        {         
            _db = db;
        }
        public User GetByEmail(string email)
        {
            var objFromDb = _db.users.FirstOrDefault(u => u.Email == email);
            return objFromDb;
        }
        public void update(User user)
        {
            var objFromDb = _db.users.FirstOrDefault(u => u.Id == user.Id);
            if (objFromDb != null)
            {
                objFromDb.Money = user.Money;
            }
        }
    }
}
