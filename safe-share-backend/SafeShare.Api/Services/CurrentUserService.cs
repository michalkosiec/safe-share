using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using SafeShare.Application.Common.Interfaces;

namespace SafeShare.Api.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor): ICurrentUserService
{
    public Guid? UserId
    {
        get
        {
            var idClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return idClaim != null ? Guid.Parse(idClaim) : null;
        }
    }

    public string? UserName =>
        httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.UniqueName);
    
    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}