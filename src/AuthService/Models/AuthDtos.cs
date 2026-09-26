namespace Auth.API.Models;

public record RegisterDto(string Username, string Email, string Password, string Role = "User");
public record LoginDto(string Email, string Password);
public record AuthResponseDto(string Token, string Username, string Email, string Role);