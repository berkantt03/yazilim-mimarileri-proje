using Net9RestApi.Models;
using Net9RestApi.Repositories;
using Net9RestApi.DTOs;

namespace Net9RestApi.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            // Entity -> DTO Dönüşümü (Elle mapping)
            return users.Select(u => new UserDto 
            { 
                Id = u.Id, 
                UserName = u.UserName, 
                Email = u.Email,
                CreatedAt = u.CreatedAt
            });
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDto 
            { 
                Id = user.Id, 
                UserName = user.UserName, 
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserDto> CreateAsync(CreateUserDto createDto)
        {
            var user = new User
            {
                UserName = createDto.UserName,
                Email = createDto.Email,
                Password = createDto.Password, // Gerçek hayatta burada hashlenmeli!
                CreatedAt = DateTime.UtcNow
            };

            await _repository.CreateAsync(user);

            return new UserDto 
            { 
                Id = user.Id, 
                UserName = user.UserName, 
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task UpdateAsync(int id, UpdateUserDto updateDto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) throw new Exception("Kullanıcı bulunamadı");

            user.UserName = updateDto.UserName;
            user.Email = updateDto.Email;
            user.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}