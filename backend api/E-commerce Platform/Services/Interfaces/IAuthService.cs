using ECommercePlatform.DTOs;

namespace ECommercePlatform.Services.Interfaces
{
    public interface IAuthService
    {
        Task <UserDto?> RegisterAsync(RegisterUserDto registerUserDto);
        Task <UserDto?> LoginAsync(LoginDto dto);
    }
}
