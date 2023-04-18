using Application.Common.Interfaces;
using Application.Common.Models.Auth;
using MediatR;

namespace Application.Features.Auth.Commands.Register
{
    public class AuthRegisterCommandHandler : IRequestHandler<AuthRegisterCommand, AuthRegisterDto>
    {
        private IAuthentificationService _authentificationService;

        private readonly IJwtService _jwtService;

        public AuthRegisterCommandHandler(IAuthentificationService authentificationService, IJwtService jwtService)
        {
            _authentificationService = authentificationService;

            _jwtService = jwtService;
            
        }

        public async Task<AuthRegisterDto> Handle(AuthRegisterCommand request, CancellationToken cancellationToken)
        {
           var createUserDto = new CreateUserDto(request.FirstName, request.LastName, request.Email, request.Password);

            var userId = await _authentificationService.CreateUserAsync(createUserDto, cancellationToken);

            var emailToken = await _authentificationService.GenerateEmailActivationTokenAsync(userId, cancellationToken);


            var fullName = $"{request.FirstName} {request.LastName}";

            var jwtDto = _jwtService.Generate(userId, request.FirstName, request.LastName, fullName);   
            return new AuthRegisterDto(request.Email, fullName, jwtDto.AccessToken);
        }
    }
}
