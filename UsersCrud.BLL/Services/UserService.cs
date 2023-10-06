using AutoMapper;
using UsersCrud.BLL.Models;
using UsersCrud.DAL.Entities;
using UsersCrud.DAL.Repositories;

namespace UsersCrud.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<UserModel> AddUserAsync(UserModel user)
        {
            var userToAdd = _mapper.Map<User>(user);
            var addedUser = await _userRepository.AddAsync(userToAdd);
            var result = _mapper.Map<UserModel>(addedUser);

            return result;
        }

        public async Task<UserModel> AddRoleToUserAsync(Guid userId, Guid roleId)
        {
            var userToUpdate = await _userRepository.GetByIdAsync(userId);
            if (userToUpdate == null)
            {
                throw new ArgumentException($"User {userId} was not found");
            }
            var role = await _roleRepository.GetByIdAsync(roleId);

            userToUpdate.Roles.Add(role);

            var user = await _userRepository.UpdateAsync(userToUpdate);
            var result = _mapper.Map<UserModel>(user);

            return result;
        }

        public async Task<UserModel> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return _mapper.Map<UserModel>(user);
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var usersModels = _mapper.Map<List<UserModel>>(users);

            return usersModels;
        }

        public async Task RemoveUserAsync(Guid id)
        {
            await _userRepository.RemoveAsync(id);
        }

        public async Task<UserModel> UpdateUserAsync(UserModel userModel)
        {
            var userToUpdate = await _userRepository.GetByIdAsync(userModel.Id);
            if (userToUpdate == null)
            {
                throw new ArgumentException($"User {userModel.Id} was not found");
            }
            
            userToUpdate.Age = userModel.Age;
            userToUpdate.Name = userModel.Name;
            userToUpdate.Email = userModel.Email;
            
            var updatedUser = _userRepository.UpdateAsync(userToUpdate);
            var result = _mapper.Map<UserModel>(updatedUser);

            return result;
        }
    }
}
