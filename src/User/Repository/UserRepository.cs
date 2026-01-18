using Microsoft.EntityFrameworkCore;
using templatebase.src.Domain.Ports.Out;
using templatebase.src.User.Infraestructure.Adapters.Out.Entities;

namespace templatebase.src.Infraestructure.Adapters.Out.Respositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AplicationDbContext _db;

        public UserRepository(AplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<UserEntity>> FindAll()
        {
            return await _db.User
                .OrderBy(u => u.UserName)
                .ToListAsync();
        }

        public async Task<UserEntity> FindById(string id)
        {
            return await _db.User.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserEntity> FindByUsername(string username)
        {
            return await _db.User.FirstOrDefaultAsync(u => u.UserName == username);
        }


        public async Task<bool> IsUniqueUser(string username)
        {
            var userDB = await _db.User
                .FirstOrDefaultAsync(u => u.UserName == username);

            return userDB == null;
        }


        public async Task Add(UserEntity user)
        {
            _db.User.Add(user);
            await _db.SaveChangesAsync();
        }



    }

}