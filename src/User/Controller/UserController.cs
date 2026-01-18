using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using templatebase.src.auth.Dtos;
using templatebase.src.Auth;
using templatebase.src.Domain.Ports.Out;
using templatebase.src.Infraestructure.Adapters.In.Dto;
using templatebase.src.User.Contract;

namespace templatebase.src.Infraestructure.Adapters.In.Rest
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AuthService _authService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            AuthService authService,
            IMapper mapper)
        {
            _userService = userService;
            _authService = authService;
            _mapper = mapper;
        }

        // USERS (ADMIN)

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.FindAll();
            return Ok(_mapper.Map<List<UserResponse>>(users));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.FindById(id);
            if (user == null) return NotFound();

            return Ok(_mapper.Map<UserResponse>(user));
        }

        // AUTH

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest dto)
        {
            try
            {
                var user = await _authService.Register(dto);
                return Created("", user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest dto)
        {
            try
            {
                var result = await _authService.Login(dto);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }

}