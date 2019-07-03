using Chatbase.Core.Repositories;
using Chatbase.Core.Services;
using Chatbase.Core.ViewModels;
using Chatbase.Repositories;
using Chatbase.Services;
using Robo.Mvvm;
using Xamarin.Forms;

namespace Chatbase
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ServiceContainer.Register<IChatRepository>(new ChatRepository());
            ServiceContainer.Register<IAuthenticationService>(new AuthenticationService());

            Robo.Mvvm.Forms.App.Init<LoginViewModel>(GetType().Assembly);
        }
    }
}
