using Auth.API.Models;

namespace Auth.API.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;

    public AuthService(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto request)
    {
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Role = request.Role
        };

        var token = _tokenService.GenerateToken(user);
        return await Task.FromResult(new AuthResponseDto(token, user.Username, user.Email, user.Role));
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto request)
    {
        var user = new User
        {
            Username = request.Email.Split('@')[0],
            Email = request.Email,
            Role = "User"
        };

        var token = _tokenService.GenerateToken(user);
        return await Task.FromResult(new AuthResponseDto(token, user.Username, user.Email, user.Role));
    }
}