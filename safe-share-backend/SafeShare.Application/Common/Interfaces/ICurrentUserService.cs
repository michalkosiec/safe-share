namespace SafeShare.Application.Common.Interfaces;

public class ICurrentUserService
{
    public Guid? UserId { get; }
    public string? Username { get; }
    public bool IsAuthenticated { get; }
}