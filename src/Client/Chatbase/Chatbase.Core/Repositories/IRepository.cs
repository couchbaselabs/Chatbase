using System;
using System.Threading.Tasks;

namespace Chatbase.Core.Repositories
{
    public interface IRepository : IDisposable
    {
        Task StartReplication();
    }
}
