namespace SafeShare.Application.Features.Auth.DTOs;

public record LoginResponse(string Token, string Username, Guid UserId);
