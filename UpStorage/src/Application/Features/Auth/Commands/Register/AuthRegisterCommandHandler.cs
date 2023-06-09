using Application.Common.Interfaces;
using Application.Common.Models.Auth;
using Application.Common.Models.Email;
using MediatR;

namespace Application.Features.Auth.Commands.Register
{
    public class AuthRegisterCommandHandler : IRequestHandler<AuthRegisterCommand, AuthRegisterDto>
    {
        private IAuthenticationService _authentificationService;

        private readonly IJwtService _jwtService;

        public readonly IEmailService _emailService; 

        public AuthRegisterCommandHandler(IAuthenticationService authentificationService, IJwtService jwtService, IEmailService emailService)
        {
            _authentificationService = authentificationService;

            _jwtService = jwtService;

            _emailService = emailService;
            
        }

        public async Task<AuthRegisterDto> Handle(AuthRegisterCommand request, CancellationToken cancellationToken)
        {
           var createUserDto = new CreateUserDto(request.FirstName, request.LastName, request.Email, request.Password);

            var userId = await _authentificationService.CreateUserAsync(createUserDto, cancellationToken);

            var emailToken = await _authentificationService.GenerateEmailActivationTokenAsync(userId, cancellationToken);


            var fullName = $"{request.FirstName} {request.LastName}";

            var jwtDto = _jwtService.Generate(userId, request.FirstName, request.LastName, fullName);

            
            
          /*  _emailService.SendEmailConfirmation(new SendEmailConfirmationDto()

            {
                Email = request.Email,
                Name = request.FirstName,
                Token= emailToken
            });*/

            return new AuthRegisterDto(request.Email, fullName, jwtDto.AccessToken);
        }
    }
}
