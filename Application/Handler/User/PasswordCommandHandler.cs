using Application.Command.User;
using Domain.Exceptions;
using Infra.Data.User;
using MediatR;

namespace Application.Handler.User
{
    public class PasswordCommandHandler : IRequestHandler<PasswordCommand>
    {
        private readonly IUserWriter _userWriter;
        private readonly IUserReader _userReader;

        public PasswordCommandHandler(IUserWriter userWriter, IUserReader userReader)
        {
            this._userWriter = userWriter;
            this._userReader = userReader;
        }

        public async Task<Unit> Handle(PasswordCommand request, CancellationToken cancellationToken)
        {
            if(! await this._userReader.ExistsAsync(request.Login, request.PasswordOld))
            {
                throw new UnauthorizedException();
            }

            if (request.PasswordOld == request.PasswordNew)
            {
                throw new InvalidPasswordException();
            }

           await this._userWriter.UpdatePassword(request.Login, request.PasswordOld, request.PasswordNew);

            return Unit.Value;
        }
    }
}