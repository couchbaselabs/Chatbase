using System;
using System.Threading.Tasks;
using Chatbase.API.Services;
using Chatbase.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chatbase.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        const string BucketName = "chatbase";

        SyncService syncService;
        SyncService SyncService
        {
            get
            {
                if (syncService == null)
                {
                    syncService = new SyncService(BucketName);
                }

                return syncService;
            }
        }

        [HttpPost]
        public Task<Session> Post([FromBody]User user)
        {
            if (!string.IsNullOrEmpty(user?.Username) &&
                !string.IsNullOrEmpty(user?.Channel))
            {
                return SyncService.GetSession(user.Username, user.Channel);
            }

            throw new InvalidOperationException("Invalid user!");
        }
    }
}
