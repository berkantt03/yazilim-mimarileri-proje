using Net9RestApi.Models;
using Net9RestApi.Repositories;
using Net9RestApi.DTOs;

namespace Net9RestApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name });
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return null;
            return new CategoryDto { Id = category.Id, Name = category.Name };
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto createDto)
        {
            var category = new Category { Name = createDto.Name };
            await _repository.CreateAsync(category);
            return new CategoryDto { Id = category.Id, Name = category.Name };
        }

        public async Task UpdateAsync(int id, UpdateCategoryDto updateDto)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) throw new Exception("Kategori bulunamadı");
            
            category.Name = updateDto.Name;
            await _repository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}