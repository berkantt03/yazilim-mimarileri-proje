using System.ComponentModel.DataAnnotations;

namespace Net9RestApi.DTOs
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}