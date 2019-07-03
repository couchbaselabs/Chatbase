using System.Threading.Tasks;
using Chatbase.Models;

namespace Chatbase.Core.Services
{
    public interface IAuthenticationService
    {
        Task<Session> Authenticate(User user);
    }
}
