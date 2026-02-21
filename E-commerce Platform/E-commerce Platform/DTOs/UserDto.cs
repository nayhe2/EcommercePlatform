namespace ECommercePlatform.DTOs
{
    public record UserDto(Guid Id, string Email, string Role, string Token);
}
