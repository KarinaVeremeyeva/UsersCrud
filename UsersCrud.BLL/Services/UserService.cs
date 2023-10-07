using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UsersCrud.BLL.Enums;
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

        public async Task<UserModel> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetQuery()
                .SingleOrDefaultAsync(u => u.Email == email);
            var userModel = _mapper.Map<UserModel>(user);

            return userModel;
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync(FilterUsersModel filterUsers)
        {
            var query = _userRepository.GetQuery();

            if (filterUsers.MinAge.HasValue)
            {
                query = query.Where(q => q.Age >= filterUsers.MinAge.Value);
            }

            if (filterUsers.MaxAge.HasValue)
            {
                query = query.Where(q => q.Age <= filterUsers.MaxAge.Value);
            }

            if (!string.IsNullOrEmpty(filterUsers.NamePart))
            {
                query = query.Where(q => q.Name.Contains(filterUsers.NamePart));
            }

            if (!string.IsNullOrEmpty(filterUsers.EmailPart))
            {
                query = query.Where(q => q.Email.Contains(filterUsers.EmailPart));
            }

            if (filterUsers.UserOrder.HasValue)
            {
                query = filterUsers.UserOrder.Value switch
                {
                    UserOrder.NameDescending => query.OrderByDescending(q => q.Name),
                    UserOrder.AgeDescending => query.OrderByDescending(q => q.Age),
                    UserOrder.EmailDescending => query.OrderByDescending(q => q.Email),
                    UserOrder.NameAscending => query.OrderBy(q => q.Name),
                    UserOrder.AgeAsceding => query.OrderBy(q => q.Age),
                    UserOrder.EmailAscending => query.OrderBy(q => q.Email),
                    _ => query
                };
            }

            if (filterUsers.RoleIds?.Any() == true)
            {
                var roleCount = filterUsers.RoleIds.Distinct().Count();

                query = query.Where(q => q.Roles
                    .Where(role => filterUsers.RoleIds.Contains(role.Id)).Count() == roleCount);
            }

            const int pageSize = 3;
            var page = filterUsers.Page >= 0 ? filterUsers.Page : 0;

            var users = await query
                .Skip(page * pageSize)
                .Take(pageSize)
                .Include(u => u.Roles)
                .ToListAsync();

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
