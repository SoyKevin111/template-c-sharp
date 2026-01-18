using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using templatebase.src.Common.Exception;
using templatebase.src.User.Contract;
using templatebase.src.User.Response;

namespace templatebase.src.Infraestructure.Adapters.In.Rest
{
    [Route("api/user")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            IMapper mapper)
        {
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
            if (id == null)
            {
                throw new BadRequestException("Error, id no enviado.");
            }
            var user = await _userService.FindById(id);
            if (user == null) return NotFound();

            return Ok(_mapper.Map<UserResponse>(user));
        }
    }

}