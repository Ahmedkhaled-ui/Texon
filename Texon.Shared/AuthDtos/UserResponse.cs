namespace Texon.Shared.AuthDtos
{
    public record UserResponse (
        string Email,
        string UserName,
        string Token
    );
 
}
