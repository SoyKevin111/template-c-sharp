using templatebase.src.User.Entity;

namespace templatebase.src.User.Contract
{
    public interface IUserRepository
    {
        public Task<List<UserEntity>> FindAll();
        public Task<UserEntity> FindById(string id);
        public Task<bool> IsUniqueUser(string username);
        public Task<UserEntity> FindByUsername(string username);
    }
}