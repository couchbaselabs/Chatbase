using System.Threading.Tasks;
using Chatbase.Core.Services;
using Chatbase.Models;

namespace Chatbase.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        public AuthenticationService() : base($"http://localhost:5000/api/")
        //public AuthenticationService() : base($"http://10.0.2.2:5000/api/")
        { }

        public Task<Session> Authenticate(User user) => PostAsync<Session, User>("authenticate", user);
    }
}
