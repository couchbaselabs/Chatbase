using System;
using System.Collections.Generic;
using Chatbase.Models;

namespace Chatbase.Core.Repositories
{
    public interface IChatRepository : IRepository
    {
        List<Message> GetMessages(Action<List<Message>> messagesUpdated);
        void SaveItem(Message message);
        void DeleteItem(Message message);
    }
}
