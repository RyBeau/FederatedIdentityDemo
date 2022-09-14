using AuthAPI.Responses;
using AuthAPI.Services.DB;
using MediatR;

namespace AuthAPI.Handlers.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserSummary>
    {
        private readonly Repository _repository;

        public LoginQueryHandler(Repository repository)
        {
              _repository = repository;
        }

        public Task<UserSummary> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetUser(request.Username, request.Password);
        }
    }
}
