using FluentValidation;

namespace SafeShare.Application.Features.Users.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator() 
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.PublicKey).NotEmpty().WithMessage("PublicKey is required.");
        RuleFor(x => x.EncryptedPrivateKey).NotEmpty().WithMessage("EncryptedPrivateKey is required.");
    }
}