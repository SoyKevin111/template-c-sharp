using templatebase.src.User.Infraestructure.Adapters.Out.Entities;

namespace templatebase.src.Domain.Ports.Out
{
    public interface IUserRepository
    {
        public Task<List<UserEntity>> FindAll();
        public Task<UserEntity> FindById(string id);
        public Task<bool> IsUniqueUser(string username);
        public Task<UserEntity> FindByUsername(string username);
        //Task<UserLoginResponse> Login(UserLoginRequest dto);
        //Task<UserResponse> Register(UserRegisterRequest dto);
    }
}