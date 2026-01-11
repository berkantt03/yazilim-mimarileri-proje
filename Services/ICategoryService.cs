using Net9RestApi.DTOs;

namespace Net9RestApi.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CreateCategoryDto createDto);
        Task UpdateAsync(int id, UpdateCategoryDto updateDto);
        Task DeleteAsync(int id);
    }
}