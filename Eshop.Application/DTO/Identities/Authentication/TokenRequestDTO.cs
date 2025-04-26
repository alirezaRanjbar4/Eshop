namespace Eshop.Application.DTO.Identities.Authentication;

public class TokenRequestDTO
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}