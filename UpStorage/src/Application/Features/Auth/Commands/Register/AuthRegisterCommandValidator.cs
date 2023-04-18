using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register
{
    public class AuthRegisterCommandValidator:AbstractValidator<AuthRegisterCommand>
    {
        private readonly IAuthentificationService _authentificationService;
        public AuthRegisterCommandValidator(IAuthentificationService authentificationService)
        {
            _authentificationService = authentificationService;

            RuleFor(x => x.Email)
                .MustAsync(CheckIfUserExist)
                .WithMessage("There is already an user with given email");
        }

        private async Task<bool> CheckIfUserExist(string email, CancellationToken cancellationToken)
        {
            var doesExist = await _authentificationService.CheckIfUserExists(email, cancellationToken);

            if (doesExist) return false;
            return true;

        }
    }
}
