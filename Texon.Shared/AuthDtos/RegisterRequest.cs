namespace Texon.Shared.AuthDtos
{
    public record RegisterRequest(string Email,
        string Password,
        string phoneNumber,
        string FirstName,
        string LastName
    )
    {
    }
}
