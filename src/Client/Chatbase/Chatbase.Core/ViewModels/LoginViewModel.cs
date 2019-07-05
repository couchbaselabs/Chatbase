using System.Threading.Tasks;
using System.Windows.Input;
using Chatbase.Core.Services;
using Chatbase.Models;
using Robo.Mvvm.Input;
using Robo.Mvvm.Services;

namespace Chatbase.Core.ViewModels
{
    public class LoginViewModel : BaseNavigationViewModel
    {
        string _username;
        public string Username
        {
            get => _username;
            set => SetPropertyChanged(ref _username, value);
        }

        string _channel;
        public string Channel
        {
            get => _channel;
            set => SetPropertyChanged(ref _channel, value);
        }

        ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new Command(async () => await LoginAsync());
                }

                return _loginCommand;
            }
        }

        IAuthenticationService AuthenticationService { get; set; }

        public LoginViewModel(INavigationService navigationService, IAuthenticationService authenticationService) : base(navigationService)
        {
            AuthenticationService = authenticationService;
        }

        async Task LoginAsync()
        {
            var user = new User
            {
                Username = Username,
                Channel = Channel
            };

            var session = await AuthenticationService.Authenticate(user);

            if (!string.IsNullOrEmpty(session?.session_id))
            {
                AppInstance.Username = Username;
                AppInstance.Channel = Channel;
                AppInstance.Session = session;

                Navigation.SetRoot<ChannelViewModel>(true);
            }
        }
    }
}
