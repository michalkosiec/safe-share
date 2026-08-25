using System.Globalization;
using SafeShare.Application.Common.Interfaces;
using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Auth.Login;

public class LoginCommandHandler(IPasswordHasher passwordHasher, IUserRepository repo, IJwtTokenGenerator jwtTokenGenerator)
{
    public async Task<string> HandleAsync(LoginCommand command, CancellationToken cancellationToken = default)
    {
        var user = await repo.GetByUserNameAsync(command.Name, cancellationToken);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid username or password");

        var isPasswordValid = passwordHasher.Verify(user.PasswordHash, command.Password);
        return isPasswordValid ? jwtTokenGenerator.GenerateToken(user.Id, user.Name) : throw new UnauthorizedAccessException("Invalid username or password");
    }
}