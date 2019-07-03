using System;

namespace Chatbase.Models
{
    public class Message
    {
        public string Type => "message";
        public string MessageId { get; set; }
        public string Channel { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string[] Channels => new string[] { Channel };
    }
}
