using System;
using System.Linq;
using System.Threading.Tasks;
using Chatbase.API.Services.Models;
using Chatbase.Models;

namespace Chatbase.API.Services
{
    public class SyncService : BaseHttpService
    {
        string _database;

        public SyncService(string database) : base($"http://52.53.240.207:4985/")
        {
            _database = database;
        }

        public async Task<Session> GetSession(string username, string channel)
        {
            var user = await GetUser(username).ConfigureAwait(false);

            if (user == null)
            {
                user = new Models.User
                {
                    name = username,
                    password = username,
                    admin_channels = new string[] { channel }
                };

                await CreateUser(user).ConfigureAwait(false);
            }
            else if (user != null && !user.admin_channels.Contains(channel))
            {
                var admin_channels = user.admin_channels.ToList();
                admin_channels.Add(channel);

                await UpdateUser(user).ConfigureAwait(false);
            }

            return await CreateSession(username);
        }

        public Task<Models.User> GetUser(string name) => GetAsync<Models.User>($"{_database}/_user/{name}");

        public Task CreateUser(Models.User user) => PostAsync($"{_database}/_user/", user);

        public Task UpdateUser(Models.User user) => PutAsync($"{_database}/_user/{user.name}", user);

        public Task<Session> CreateSession(string name)
            => PostAsync<Session, SessionRequest>($"{_database}/_session", new SessionRequest { name = name });
    }
}
