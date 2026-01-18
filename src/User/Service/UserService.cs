using Microsoft.AspNetCore.Identity;
using templatebase.src.Domain.Ports.Out;
using templatebase.src.User.Contract;
using templatebase.src.User.Infraestructure.Adapters.Out.Entities;

namespace templatebase.src.User.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly UserManager<UserEntity> _userManager;

        public UserService(IUserRepository userRepo, UserManager<UserEntity> userManager)
        {
            _userRepo = userRepo;
            _userManager = userManager;
        }

        public async Task<List<UserEntity>> FindAll()
        {
            return await _userRepo.FindAll();
        }

        public async Task<UserEntity> FindById(string id)
        {
            return await _userRepo.FindById(id);
        }
    }
}
