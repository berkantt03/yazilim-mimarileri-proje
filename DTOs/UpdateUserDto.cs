namespace Net9RestApi.DTOs
{
    public class UpdateUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // Şifre güncelleme genelde ayrı endpoint olur, buraya koymadık.
    }
}