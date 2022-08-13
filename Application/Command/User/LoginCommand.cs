using Domain.DTO.User;
using MediatR;

namespace Application.Command.User
{
    public class LoginCommand : IRequest<TokenDTO>
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}