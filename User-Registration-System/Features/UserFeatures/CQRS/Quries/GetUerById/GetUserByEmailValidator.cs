using FluentValidation;

namespace User_Registration_System.Features.UserFeatures.CQRS.Quries.GetUerById
{
    public class GetUserByEmailValidator : AbstractValidator<GetUerByEmailQuery>
    {
        public GetUserByEmailValidator()
        {
            RuleFor(x => x.UserEmail)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }

    }
}
