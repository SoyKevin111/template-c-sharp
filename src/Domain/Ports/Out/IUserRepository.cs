
using templatebase.src.auth.Dtos;
using templatebase.src.User.Infraestructure.Adapters.Out.Entities;

namespace templatebase.src.Domain.Ports.Out
{
    public interface IUserRepository
    {
        Task<ICollection<UserEntity>> FindAll();
        Task<UserEntity> FindById(string id);
        Task<bool> IsUniqueUser(string username);
        Task<UserLoginResponseDTO> Login(UserLoginDTO dto);
        Task<UserDataDTO> Register(UserRegisterDTO dto);
    }
}