using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using templatebase.src.Auth.Request;
using templatebase.src.Auth.Response;
using templatebase.src.Common.Exception;
using templatebase.src.User.Contract;
using templatebase.src.User.Entity;
using templatebase.src.User.Response;

namespace templatebase.src.Auth
{
    public class AuthService
    {
        private readonly IUserRepository _repo;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly string _SECRET_KEY;

        public AuthService(
            IUserRepository repo,
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration cfg,
            IMapper mapper)
        {
            _repo = repo;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _SECRET_KEY = cfg.GetValue<string>("ApiSettings:Secret_Key");
        }

        public async Task<UserLoginResponse> Login(UserLoginRequest dto)
        {
            var username = dto.Username.Trim();

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new NotFoundException("Username no encontrado.");
            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new AuthenticationException(); //valida credenciales invalidas

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            UserResponse userResponse = _mapper.Map<UserResponse>(user);
            userResponse.Role = role;

            return new UserLoginResponse
            {
                Token = GenerateJwt(user, roles),
                User = userResponse,
            };
        }

        public async Task<UserResponse> Register(UserRegisterRequest dto)
        {
            //como param despues
            string role = "User"; //Admin o User

            var username = dto.Username.Trim();

            if (!await _repo.IsUniqueUser(username))
                throw new ConflictException("El usuario ya existe");

            //verificar role
            await EnsureRoleExists(role);

            var user = new UserEntity
            {
                UserName = username,
                Name = dto.Name,
                CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Role = role
            };


            var result = await _userManager.CreateAsync(user, dto.Password);

            //asignar role
            await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded) throw new BadRequestException("Error en los campos, no se pudo registrar el usuario.");

            return new UserResponse
            {
                Id = user.Id,
                Username = user.UserName,
                Name = user.Name,
                Role = role
            };
        }

        private string GenerateJwt(UserEntity user, IList<string> roles)
        {
            var key = Encoding.ASCII.GetBytes(_SECRET_KEY);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, roles.First())
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private async Task EnsureRoleExists(string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                throw new NotFoundException("Rol no existente.");
            }
        }



    }
}