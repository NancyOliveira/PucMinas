using Application.Command.User;
using Domain.DTO.User;
using Domain.Exceptions;
using Infra.ACL.Jwt;
using Infra.Data.User;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Handler.User
{
    public class AuthenticationCommandHandler : IRequestHandler<LoginCommand, TokenDTO>
    {
        private readonly IJwt _jwt;
        private readonly IUserReader _userReader;
        private readonly string seconds;

        public AuthenticationCommandHandler(IJwt jwt, IUserReader userReader, IConfiguration configuration)
        {
            this._jwt = jwt;
            this._userReader = userReader;
            this.seconds = configuration["Jwt:Seconds"];
        }

        public async Task<TokenDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (await this._userReader.ExistsAsync(request.Login, request.Password))
            {
                TokenDTO tokenDTO = new TokenDTO()
                {
                    ExpirationDate = DateTime.UtcNow.AddSeconds(Convert.ToInt32(this.seconds))
                };

                tokenDTO.Token = this._jwt.CreateToken(request.Login, tokenDTO.ExpirationDate);

                return tokenDTO;
            }
            else
            {
                throw new UnauthorizedException();
            }
        }
    }
}
