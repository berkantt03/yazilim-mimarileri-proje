using Microsoft.AspNetCore.Mvc;
using Net9RestApi.DTOs;
using Net9RestApi.Services;
using Net9RestApi.Shared;

namespace Net9RestApi.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            // Minimal API Endpoint'leri
            // Çakışma olmasın diye adresini farklı yapıyoruz: api/minimal/users
            var group = app.MapGroup("api/minimal/users").WithTags("Users (Minimal API)");

            // GET All
            group.MapGet("/", async (IUserService userService) =>
            {
                var users = await userService.GetAllAsync();
                return Results.Ok(ApiResponse<IEnumerable<UserDto>>.SuccessResponse(users));
            });

            // GET By Id
            group.MapGet("/{id}", async (int id, IUserService userService) =>
            {
                var user = await userService.GetByIdAsync(id);
                return user is not null 
                    ? Results.Ok(ApiResponse<UserDto>.SuccessResponse(user)) 
                    : Results.NotFound(ApiResponse<UserDto>.ErrorResponse("Kullanıcı bulunamadı"));
            });

            // POST (Create)
            group.MapPost("/", async ([FromBody] CreateUserDto createDto, IUserService userService) =>
            {
                var newUser = await userService.CreateAsync(createDto);
                return Results.Created($"/api/minimal/users/{newUser.Id}", 
                       ApiResponse<UserDto>.SuccessResponse(newUser, "Kullanıcı oluşturuldu"));
            });

            // PUT (Update)
            group.MapPut("/{id}", async (int id, [FromBody] UpdateUserDto updateDto, IUserService userService) =>
            {
                try
                {
                    await userService.UpdateAsync(id, updateDto);
                    return Results.Ok(ApiResponse<object>.SuccessResponse(null, "Kullanıcı güncellendi"));
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
                }
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, IUserService userService) =>
            {
                await userService.DeleteAsync(id);
                return Results.Ok(ApiResponse<object>.SuccessResponse(null, "Kullanıcı silindi"));
            });
        }
    }
}