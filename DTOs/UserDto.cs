namespace Net9RestApi.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // Password göndermiyoruz, güvenlik gerekçesiyle.
        public DateTime CreatedAt { get; set; }
    }
}