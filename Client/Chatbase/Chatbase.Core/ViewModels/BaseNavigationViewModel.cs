using Robo.Mvvm.Services;
using Robo.Mvvm.ViewModels;

namespace Chatbase.Core.ViewModels
{
    public abstract class BaseNavigationViewModel : BaseViewModel
    {
        protected INavigationService Navigation { get; set; }

        protected BaseNavigationViewModel(INavigationService navigationService)
        {
            Navigation = navigationService;
        }
    }
}
