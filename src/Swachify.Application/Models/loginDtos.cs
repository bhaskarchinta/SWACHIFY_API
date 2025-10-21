namespace Swachify.Application.DTOs;

public record loginDtos
(
    string email,
    string password
);

public record ForgotPasswordRequestDto(
        string Email,
        string Password,
        string ConfirmPassword
    );