namespace Chatbase.API.Services.Models
{
    public class User
    {
        public string name { get; set; }
        public string password { get; set; }
        public string[] admin_channels { get; set; }
        public string[] admin_roles { get; set; }
        public string[] all_channels { get; set; }
        public string email { get; set; }
        public bool disabled { get; set; }
        public string[] roles { get; set; }
    }
}
