using Net9RestApi.Models;
using Net9RestApi.Repositories;
using Net9RestApi.DTOs;

namespace Net9RestApi.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _repository;

        public ArticleService(IRepository<Article> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ArticleDto>> GetAllAsync()
        {
            var articles = await _repository.GetAllAsync();
            return articles.Select(a => new ArticleDto 
            { 
                Id = a.Id, 
                Title = a.Title, 
                Content = a.Content,
                CreatedAt = a.CreatedAt,
                CategoryId = a.CategoryId,
                UserId = a.UserId
            });
        }

        public async Task<ArticleDto?> GetByIdAsync(int id)
        {
            var a = await _repository.GetByIdAsync(id);
            if (a == null) return null;

            return new ArticleDto 
            { 
                Id = a.Id, 
                Title = a.Title, 
                Content = a.Content,
                CreatedAt = a.CreatedAt,
                CategoryId = a.CategoryId,
                UserId = a.UserId
            };
        }

        public async Task<ArticleDto> CreateAsync(CreateArticleDto createDto)
        {
            var article = new Article
            {
                Title = createDto.Title,
                Content = createDto.Content,
                CategoryId = createDto.CategoryId,
                UserId = createDto.UserId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.CreateAsync(article);

            return new ArticleDto 
            { 
                Id = article.Id, 
                Title = article.Title, 
                Content = article.Content,
                CreatedAt = article.CreatedAt,
                CategoryId = article.CategoryId,
                UserId = article.UserId
            };
        }

        public async Task UpdateAsync(int id, UpdateArticleDto updateDto)
        {
            var article = await _repository.GetByIdAsync(id);
            if (article == null) throw new Exception("Makale bulunamadı");

            article.Title = updateDto.Title;
            article.Content = updateDto.Content;
            article.CategoryId = updateDto.CategoryId;
            article.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(article);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}