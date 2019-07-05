using System;
using System.Linq;
using Acr.UserDialogs;
using Chatbase.Core.ViewModels;
using Robo.Mvvm.Forms.Pages;
using Xamarin.Forms;

namespace Chatbase.Pages
{
    public partial class ChannelPage : BaseContentPage<ChannelViewModel>
    {
        public ChannelPage()
        {
            InitializeComponent();

            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnMessagesUpdated += ScrollToEnd;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.OnMessagesUpdated -= ScrollToEnd;
        }

        void ScrollToEnd(object sender, EventArgs e)
        {
            var messages = (BindingContext as ChannelViewModel)?.Messages;

            if (messages?.Count > 0)
            {
                Device.BeginInvokeOnMainThread(() =>
                  messageList.ScrollTo((BindingContext as ChannelViewModel).Messages.Last(), ScrollToPosition.End, false)
                );
            }
        }

        public async void Handle_Channel_Clicked(object sender, EventArgs e)
        {
            var result = await UserDialogs.Instance.PromptAsync(new PromptConfig
            {
                InputType = InputType.Name,
                OkText = "Join",
                Title = "Switch Channels",
            });

            if (result.Ok && !string.IsNullOrWhiteSpace(result.Text))
            {
                await ViewModel.SwitchChannelAsync(result.Text);
            }
        }
    }
}
