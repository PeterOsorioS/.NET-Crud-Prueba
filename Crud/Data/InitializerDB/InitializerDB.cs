using Crud.Data.InitializerDB.IInitializerDB;
using Microsoft.EntityFrameworkCore;

namespace Crud.Data.InitializerDB
{
    public class InitializerDB: IInitializerDB.IInitializerDB
    {
        private readonly ApplicationDbContext _db;
        public InitializerDB(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Initialization()
        {
            try
            {
                await _db.Database.EnsureCreatedAsync();
            }
            catch (Exception)
            {
            }
        }
    }
}
