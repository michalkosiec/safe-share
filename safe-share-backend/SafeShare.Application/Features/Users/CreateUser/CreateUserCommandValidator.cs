using FluentValidation;

namespace SafeShare.Application.Features.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(50).WithMessage("Name must not exceed 50 characters");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").MinimumLength(8).WithMessage("Password must have at least 8 characters").MaximumLength(50).WithMessage("Password must not exceed 50 characters");
        RuleFor(x => x.PublicKey).NotEmpty().WithMessage("PublicKey is required");
        RuleFor(x => x.EncryptedPrivateKey).NotEmpty().WithMessage("EncryptedPrivateKey is required");
    }
}