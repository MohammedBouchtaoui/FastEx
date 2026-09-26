using Auth.API.Models;

namespace Auth.API.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}