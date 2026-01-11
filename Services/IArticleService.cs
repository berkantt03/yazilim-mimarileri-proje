using Net9RestApi.DTOs;

namespace Net9RestApi.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleDto>> GetAllAsync();
        Task<ArticleDto?> GetByIdAsync(int id);
        Task<ArticleDto> CreateAsync(CreateArticleDto createDto);
        Task UpdateAsync(int id, UpdateArticleDto updateDto);
        Task DeleteAsync(int id);
    }
}