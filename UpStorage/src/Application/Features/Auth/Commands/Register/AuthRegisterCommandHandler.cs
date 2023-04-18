using Application.Common.Interfaces;
using Application.Common.Models.Auth;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.Register
{
    public class AuthRegisterCommandHandler : IRequestHandler<AuthRegisterCommand, AuthRegisterDto>
    {
        private IAuthentificationService _authentificationService;

        public AuthRegisterCommandHandler(IAuthentificationService authentificationService)
        {
            _authentificationService = authentificationService;
        }

        public async Task<AuthRegisterDto> Handle(AuthRegisterCommand request, CancellationToken cancellationToken)
        {
           var createUserDto = new CreateUserDto(request.FirstName, request.LastName, request.Email, request.Password);

            var userId = await _authentificationService.CreateUserAsync(createUserDto, cancellationToken);

            var emailToken = await _authentificationService.GenerateEmailActivationTokenAsync(userId, cancellationToken);


            var fullName = $"{request.FirstName} {request.LastName}";
            return new AuthRegisterDto(request.Email, fullName, emailToken);
        }
    }
}
