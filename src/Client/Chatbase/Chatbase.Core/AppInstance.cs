using Chatbase.Models;

namespace Chatbase.Core
{
    public static class AppInstance
    {
        public static string Username { get; set; }
        public static string Channel { get; set; }
        public static Session Session { get; set; }

        public static void Reset()
        {
            Username = null;
            Channel = null;
            Session = null;
        }
    }
}
