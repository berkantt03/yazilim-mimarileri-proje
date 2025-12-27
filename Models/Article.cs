namespace Net9RestApi.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        
        public int UserId { get; set; }
        public User? User { get; set; }
        
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        
        public List<Comment> Comments { get; set; } = new();
    }
}