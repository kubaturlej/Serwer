using FluentValidation;
using Football.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Football.API.DTOs.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator(FootballDbContext dbContext)
        {
            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });

            RuleFor(x => x.Nickname)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.NickName == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Nickname", "That nickname is taken");
                    }
                });
        }
    }
}
