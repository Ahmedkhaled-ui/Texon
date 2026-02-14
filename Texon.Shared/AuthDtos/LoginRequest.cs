namespace Texon.Shared.AuthDtos
{
    public record LoginRequest(
        string Email,
        string Password
    )
    {
    }
}
