using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersCrud.Api.DTOs;
using UsersCrud.BLL.Models;
using UsersCrud.BLL.Services;

namespace UsersCrud.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task<IActionResult> UsersAsync([FromQuery] FilterUsersDto filterUsersDto)
        {
            var filterUsers = _mapper.Map<FilterUsersModel>(filterUsersDto);
            var users = await _userService.GetUsersAsync(filterUsers);
            var usersDto = _mapper.Map<List<UserDto>>(users);

            return Ok(usersDto);
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
        public async Task<IActionResult> AddUserAsync(UpdateUserDto userDto)
        {
            var userByEmail = await _userService.GetUserByEmailAsync(userDto.Email);
            if (userByEmail != null)
            {
                return BadRequest();
            }

            var user = _mapper.Map<UserModel>(userDto);
            var addedUser = await _userService.AddUserAsync(user);
            var result = _mapper.Map<UserDto>(addedUser);

            return Ok(result);
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
            var updatedUser = await _userService.UpdateUserAsync(id, userModel);
            var result = _mapper.Map<UserDto>(updatedUser);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            await _userService.RemoveUserAsync(id);

            return Ok();
        }
    }
}
