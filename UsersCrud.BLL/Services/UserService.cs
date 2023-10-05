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

        public UserModel AddUser(UserModel user)
        {
            var userToAdd = _mapper.Map<User>(user);
            var addedUser = _userRepository.Add(userToAdd);
            var result = _mapper.Map<UserModel>(addedUser);

            return result;
        }

        public void AddRoleToUser(Guid userId, Guid roleId)
        {
            var userToUpdate = _userRepository.GetById(userId);
            if (userToUpdate == null)
            {
                throw new ArgumentException($"User {userId} was not found");
            }
            var role = _roleRepository.GetById(roleId);

            userToUpdate.Roles.Add(role);
            _userRepository.Update(userToUpdate);
        }

        public UserModel GetUserById(Guid id)
        {
            var user = _userRepository.GetById(id);

            return _mapper.Map<UserModel>(user);
        }

        public IEnumerable<UserModel> GetUsers()
        {
            var users = _userRepository.GetAll();
            var usersModels = _mapper.Map<List<UserModel>>(users);

            return usersModels;
        }

        public void RemoveUser(Guid id)
        {
            _userRepository.Remove(id);
        }

        public UserModel UpdateUser(UserModel userModel)
        {
            var userToUpdate = _userRepository.GetById(userModel.Id);
            if (userToUpdate == null)
            {
                throw new ArgumentException($"User {userModel.Id} was not found");
            }
            
            userToUpdate.Age = userModel.Age;
            userToUpdate.Name = userModel.Name;
            userToUpdate.Email = userModel.Email;
            
            var updatedUser = _userRepository.Update(userToUpdate);
            var result = _mapper.Map<UserModel>(updatedUser);

            return result;
        }
    }
}
