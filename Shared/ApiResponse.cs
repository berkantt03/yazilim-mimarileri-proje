namespace Net9RestApi.Shared
{
    // T: Dönecek verinin tipi (UserDto, List<UserDto> vb.)
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        // Başarılı cevaplar için yardımcı metot
        public static ApiResponse<T> SuccessResponse(T data, string message = "İşlem başarılı")
        {
            return new ApiResponse<T> { Success = true, Message = message, Data = data };
        }

        // Hatalı cevaplar için yardımcı metot
        public static ApiResponse<T> ErrorResponse(string message)
        {
            return new ApiResponse<T> { Success = false, Message = message, Data = default };
        }
    }
}