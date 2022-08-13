using MediatR;

namespace Application.Command.User
{
    public class PasswordCommand : IRequest<Unit>
    {
        public string Login { get; set; }

        public string PasswordOld { get; set; }

        public string PasswordNew { get; set; }
    }
}