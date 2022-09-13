using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Handlers.Login
{
    public class LoginQuery : IRequest<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
