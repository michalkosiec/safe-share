using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using SafeShare.Application.Common.Interfaces;

namespace SafeShare.Api.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor): ICurrentUserService
{
    public Guid UserId
    {
        get
        {
            var principal = httpContextAccessor.HttpContext?.User;
            var idClaim = principal?.FindFirstValue(JwtRegisteredClaimNames.Sub)
                ?? principal?.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(idClaim, out var userId)
                ? userId
                : throw new InvalidOperationException("Critical Auth Error: Identity claim (Sub) is missing or invalid in the token.");
        }
    }

    public string Username
    {
        get
        {
            var nameClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.UniqueName);
            return nameClaim != null ? nameClaim : throw new InvalidOperationException("Critical Auth Error: UniqueName claim is missing.");
        }
    }

    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}