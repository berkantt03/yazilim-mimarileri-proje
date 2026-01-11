using Net9RestApi.DTOs;

namespace Net9RestApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(CreateUserDto createDto);
        Task UpdateAsync(int id, UpdateUserDto updateDto);
        Task DeleteAsync(int id);
    }
}