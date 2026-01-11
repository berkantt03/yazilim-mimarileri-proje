using System.ComponentModel.DataAnnotations;

namespace Net9RestApi.DTOs
{
    public class CreateArticleDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Content { get; set; } = string.Empty;
        
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int UserId { get; set; } // İleride bunu token'dan alacağız, şimdilik elle giriyoruz
    }
}