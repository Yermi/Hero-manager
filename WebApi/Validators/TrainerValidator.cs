using System;
using FluentValidation;
using WebApi.Infrastructure;
using WebApi.Models;

namespace WebApi.Validators
{
    public class TrainerValidator : AbstractValidator<Trainer>
    {
        public TrainerValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .OnAnyFailure(x => throw new WebApiException(400, nameof(x.Name) + " is not valid"));

            RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(@"^(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
            .OnAnyFailure(x => throw new WebApiException(400, nameof(x.Password) + " is not in correct format"));

            RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .OnAnyFailure(x => throw new WebApiException(400, nameof(x.Email) + " is not valid"));

            RuleFor(x => x.StartTraining)
            .NotEmpty()
            .Must(x => x < DateTime.Now)
            .OnAnyFailure(x => throw new WebApiException(400, nameof(x.StartTraining) + " is not valid"));
        }
    }
}