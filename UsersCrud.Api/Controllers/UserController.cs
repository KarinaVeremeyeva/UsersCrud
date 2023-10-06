using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UsersCrud.Api.DTOs;
using UsersCrud.BLL.Models;
using UsersCrud.BLL.Services;

namespace UsersCrud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await _userService.GetUsersAsync();
            var usersDto = _mapper.Map<List<UserDto>>(users);

            return usersDto;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<UserDto> AddUserAsync(CreateUserDto userDto)
        {
            var user = _mapper.Map<UserModel>(userDto);
            var addedUser = await _userService.AddUserAsync(user);
            var result = _mapper.Map<UserDto>(addedUser);

            return result;
        }

        [HttpPut("{userId}/role/{roleId}")]
        public async Task<IActionResult> AddRoleToUserAsync(Guid userId, Guid roleId)
        {
            var user = await _userService.AddRoleToUserAsync(userId, roleId);
            var result = _mapper.Map<UserDto>(user);

            return Ok(result);
        }

        [HttpPut]
        public async Task<UserDto> UpdateUserAsync(UpdateUserDto userDto)
        {
            var user = _mapper.Map<UserModel>(userDto);
            var updatedUser = await _userService.UpdateUserAsync(user);
            var result = _mapper.Map<UserDto>(updatedUser);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            await _userService.RemoveUserAsync(id);

            return Ok();
        }
    }
}
