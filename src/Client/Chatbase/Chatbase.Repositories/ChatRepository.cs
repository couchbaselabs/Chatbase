using System;
using System.Collections.Generic;
using Couchbase.Lite;
using Couchbase.Lite.Query;
using Chatbase.Core;
using Chatbase.Core.Repositories;
using Chatbase.Models;

namespace Chatbase.Repositories
{
    public sealed class ChatRepository : BaseRepository, IChatRepository
    {
        IQuery _messagesQuery;
        ListenerToken _messagesQueryToken;

        public ChatRepository() : base("chatbase")
        { }

        public List<Message> GetMessages(Action<List<Message>> messagesUpdated)
        {
            List<Message> messages = new List<Message>();

            try
            {
                var database = DatabaseManager.Database;

                if (database != null)
                {
                    _messagesQuery = QueryBuilder
                                    .Select(SelectResult.All())
                                    .From(DataSource.Database(database))
                                    .Where((Expression.Property("type").EqualTo(Expression.String("message")))
                                    .And((Expression.Property("channel").EqualTo(Expression.String(AppInstance.Channel)))));

                    if (messagesUpdated != null)
                    {
                        _messagesQueryToken = _messagesQuery.AddChangeListener((object sender, QueryChangedEventArgs e) =>
                        {
                            if (e?.Results != null && e.Error == null)
                            {
                                messages = e.Results.AllResults()?.ToObjects<Message>("chatbase") as List<Message>;

                                if (messages != null)
                                {
                                    messagesUpdated.Invoke(messages);
                                }
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ChatRepository Exception: {ex.Message}");
            }

            return messages;
        }

        public void SaveItem(Message message) => DatabaseManager.Database.Save(message.ToMutableDocument($"message::{message.MessageId}"));

        public void DeleteItem(Message message)
        {
            var document = DatabaseManager.Database.GetDocument($"item::{message.MessageId}");

            if (document != null)
            {
                DatabaseManager.Database.Delete(document);
            }
        }

        public override void Dispose()
        {
            // Remove the live query change listener
            _messagesQuery.RemoveChangeListener(_messagesQueryToken);

            base.Dispose();
        }
    }
}

