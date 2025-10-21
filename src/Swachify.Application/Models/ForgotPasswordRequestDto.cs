namespace Swachify.Application.Models
{
    public record ForgotPasswordRequestDto(
        string Email,
        string Password,
        string ConfirmPassword
    );
}
