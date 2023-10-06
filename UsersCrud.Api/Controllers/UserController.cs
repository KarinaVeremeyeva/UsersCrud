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
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            IRoleService roleService,
            IMapper mapper)
        {
            _userService = userService;
            _roleService = roleService;
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
        public async Task<UserDto> AddUserAsync(UpdateUserDto userDto)
        {
            var user = _mapper.Map<UserModel>(userDto);
            var addedUser = await _userService.AddUserAsync(user);
            var result = _mapper.Map<UserDto>(addedUser);

            return result;
        }

        [HttpPut("{userId}/role/{roleId}")]
        public async Task<IActionResult> AddRoleToUserAsync(Guid userId, Guid roleId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound(userId);
            }

            var checkRole = user.Roles.Any(x => x.Id == roleId);
            if (checkRole)
            {
                return BadRequest("Role already exists");
            }

            var role = await _roleService.GetRoleByIdAsync(roleId);
            if (role == null)
            {
                return NotFound(roleId);
            }

            var userModel = await _userService.AddRoleToUserAsync(userId, roleId);
            var result = _mapper.Map<UserDto>(userModel);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, UpdateUserDto userDto)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userModel = _mapper.Map<UserModel>(userDto);
            var updatedUser = await _userService.UpdateUserAsync(userModel);
            var result = _mapper.Map<UserDto>(updatedUser);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.RemoveUserAsync(id);

            return Ok();
        }
    }
}
