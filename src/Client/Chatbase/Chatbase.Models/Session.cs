using System;

namespace Chatbase.Models
{
    public class Session
    {
        public string cookie_name { get; set; }
        public string expires { get; set; }
        public string session_id { get; set; }
    }
}
