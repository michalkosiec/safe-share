namespace SafeShare.Application.Features.Auth.DTOs;

public record LoginResponse(string token, string Username, Guid UserId);
