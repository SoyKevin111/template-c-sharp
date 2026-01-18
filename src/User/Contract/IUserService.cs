

using templatebase.src.User.Entity;

namespace templatebase.src.User.Contract
{
    public interface IUserService
    {

        public Task<List<UserEntity>> FindAll();
        public Task<UserEntity> FindById(string id);

    }
}