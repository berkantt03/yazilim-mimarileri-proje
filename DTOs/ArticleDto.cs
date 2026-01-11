namespace Net9RestApi.DTOs
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        
        // Hangi kategoriye ve kime ait olduğunu ID olarak dönüyoruz
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}