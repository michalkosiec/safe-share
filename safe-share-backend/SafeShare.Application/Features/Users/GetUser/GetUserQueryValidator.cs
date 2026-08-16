using FluentValidation;

namespace SafeShare.Application.Features.Users.GetUser;

public class GetUserQueryValidator: AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
    }
}