using FluentValidation;
using WebApi.Infrastructure;
using WebApi.Models;

namespace WebApi.Validators
{
    public class HeroValidator : AbstractValidator<Hero>
    {
        public HeroValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .Must(x => x.Length >= 3)
            .OnAnyFailure(x => throw new WebApiException(400, nameof(x.Name) + " is not valid"));
        }
    }
}