using Microsoft.AspNetCore.Mvc;
using Net9RestApi.DTOs;
using Net9RestApi.Services;
using Net9RestApi.Shared; // <-- ApiResponse sınıfını buradan tanıyacak

namespace Net9RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Adres: api/users
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDto>>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<UserDto>>.SuccessResponse(users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(ApiResponse<UserDto>.ErrorResponse("Kullanıcı bulunamadı"));
            }
            return Ok(ApiResponse<UserDto>.SuccessResponse(user));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<UserDto>>> Create(CreateUserDto createDto)
        {
            var createdUser = await _userService.CreateAsync(createDto);
            // 201 Created kodu ile dönüyoruz
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, ApiResponse<UserDto>.SuccessResponse(createdUser, "Kullanıcı oluşturuldu"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Update(int id, UpdateUserDto updateDto)
        {
            try
            {
                await _userService.UpdateAsync(id, updateDto);
                // Güncelleme işleminde data dönmeye gerek yok, null dönüyoruz.
                return Ok(ApiResponse<object>.SuccessResponse(null, "Kullanıcı güncellendi"));
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            // Silme işleminde data dönmeye gerek yok, null dönüyoruz.
            return Ok(Shared.ApiResponse<object>.SuccessResponse(null, "Kullanıcı silindi"));
        }
    }
}