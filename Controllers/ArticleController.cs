using Microsoft.AspNetCore.Mvc;
using Net9RestApi.DTOs;
using Net9RestApi.Services;

namespace Net9RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ArticleDto>>>> GetAll()
        {
            var articles = await _articleService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ArticleDto>>.SuccessResponse(articles));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ArticleDto>>> GetById(int id)
        {
            var article = await _articleService.GetByIdAsync(id);
            if (article == null) return NotFound(ApiResponse<ArticleDto>.ErrorResponse("Makale bulunamadı"));
            return Ok(ApiResponse<ArticleDto>.SuccessResponse(article));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ArticleDto>>> Create(CreateArticleDto createDto)
        {
            // İleride burada 'Bu UserId veritabanında var mı?' kontrolü yapılabilir.
            var article = await _articleService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = article.Id }, ApiResponse<ArticleDto>.SuccessResponse(article));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Update(int id, UpdateArticleDto updateDto)
        {
            try
            {
                await _articleService.UpdateAsync(id, updateDto);
                return Ok(ApiResponse<object>.SuccessResponse(null, "Makale güncellendi"));
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            await _articleService.DeleteAsync(id);
            return Ok(ApiResponse<object>.SuccessResponse(null, "Makale silindi"));
        }
    }
}