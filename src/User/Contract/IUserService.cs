using templatebase.src.User.Infraestructure.Adapters.Out.Entities;

namespace templatebase.src.User.Contract
{
    public interface IUserService
    {

        public Task<List<UserEntity>> FindAll();
        public Task<UserEntity> FindById(string id);

    }
}