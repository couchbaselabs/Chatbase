using System;
using System.Threading.Tasks;
using Couchbase.Lite;
using Chatbase.Core;
using Chatbase.Core.Repositories;

namespace Chatbase.Repositories
{
    public abstract class BaseRepository : IRepository
    {
        readonly string _databaseName;

        protected DatabaseManager _databaseManager;
        protected DatabaseManager DatabaseManager
        {
            get
            {
                if (_databaseManager == null)
                {
                    _databaseManager = new DatabaseManager(_databaseName);
                }

                return _databaseManager;
            }
        }

        protected BaseRepository(string databaseName)
        {
            _databaseName = databaseName;

            try
            {
                Database.Log.Console.Level = Couchbase.Lite.Logging.LogLevel.Verbose;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task StartReplication()
        {
            return Task.Run(() =>
            {
                DatabaseManager.StopReplication();
                DatabaseManager.StartReplication(AppInstance.Session.session_id, new string[] { AppInstance.Channel });
            });
        }

        public Task RestartReplication()
        {
            return Task.Run(() =>
            {
                DatabaseManager.StopReplication();
                return StartReplication();
            });
        }

        public virtual void Dispose() => DatabaseManager?.Dispose();
    }
}