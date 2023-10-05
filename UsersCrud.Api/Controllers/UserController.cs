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
        public IEnumerable<UserDto> GetUsers()
        {
            var users = _userService.GetUsers();
            var usersDto = _mapper.Map<List<UserDto>>(users);

            return usersDto;
        }

        [HttpGet("{id}")]
        public UserDto GetUser(Guid id)
        {
            var user = _userService.GetUserById(id);
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        [HttpPost]
        public UserDto AddUser(CreateUserDto userDto)
        {
            var user = _mapper.Map<UserModel>(userDto);
            var addedUser = _userService.AddUser(user);
            var result = _mapper.Map<UserDto>(addedUser);

            return result;
        }

        [HttpPut("{userId}/role/{roleId}")]
        public IActionResult AddRoleToUser(Guid userId, Guid roleId)
        {
            _userService.AddRoleToUser(userId, roleId);

            return Ok();
        }

        [HttpPut]
        public UserDto UpdateUser(UpdateUserDto userDto)
        {
            var user = _mapper.Map<UserModel>(userDto);
            var updatedUser = _userService.UpdateUser(user);
            var result = _mapper.Map<UserDto>(updatedUser);

            return result;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            _userService.RemoveUser(id);

            return Ok();
        }
    }
}
