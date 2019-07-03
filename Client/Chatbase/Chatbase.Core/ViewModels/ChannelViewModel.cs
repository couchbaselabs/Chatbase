using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Chatbase.Core.Repositories;
using Chatbase.Core.Services;
using Chatbase.Models;
using Robo.Mvvm.Input;
using Robo.Mvvm.Services;

namespace Chatbase.Core.ViewModels
{
    public class ChannelViewModel : BaseNavigationViewModel
    {
        public event EventHandler OnMessagesUpdated;

        string _channel;
        public string Channel
        {
            get => _channel;
            set => SetPropertyChanged(ref _channel, value);
        }

        string _message;
        public string Message
        {
            get => _message;
            set => SetPropertyChanged(ref _message, value);
        }

        // In case we want to add (observed) individual additions later
        ObservableCollection<Message> _messages = new ObservableCollection<Message>();
        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set => SetPropertyChanged(ref _messages, value);
        }

        ICommand _sendMessageCommand;
        public ICommand SendMessageCommand
        {
            get
            {
                if (_sendMessageCommand == null)
                {
                    _sendMessageCommand = new Command(SendMessage);
                }

                return _sendMessageCommand;
            }
        }

        ICommand _logoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                if (_logoutCommand == null)
                {
                    _logoutCommand = new Command(Logout);
                }

                return _logoutCommand;
            }
        }

        public IAuthenticationService AuthenticationService { get; set; }
        public IChatRepository ChatRepository { get; set; }

        public ChannelViewModel(INavigationService navigationService,
                                IAuthenticationService authenticationService,
                                IChatRepository chatRepository) : base(navigationService)
        {
            AuthenticationService = authenticationService;
            ChatRepository = chatRepository;
        }

        public override async Task InitAsync()
        {
            IsBusy = true;

            Channel = AppInstance.Channel;

            await ChatRepository.StartReplication().ConfigureAwait(false);

            var items = await Task.Run(() => ChatRepository?.GetMessages(UpdateItems));

            if (items?.Count > 0)
            {
                UpdateItems(items);
            }

            IsBusy = false;
        }

        void UpdateItems(List<Message> messages)
        {
            Messages = new ObservableCollection<Message>(messages);
            OnMessagesUpdated?.Invoke(this, null);
        }

        void SendMessage()
        {
            if (!string.IsNullOrEmpty(Message))
            {
                var message = new Message
                {
                    MessageId = Guid.NewGuid().ToString(),
                    Author = AppInstance.Username,
                    Text = Message,
                    CreatedDateTime = DateTime.Now,
                    Channel = AppInstance.Channel
                };

                ChatRepository.SaveItem(message);

                Message = null;
            }
        }

        public async Task SwitchChannelAsync(string channel)
        {
            var session = await AuthenticationService.Authenticate(new User
            {
                Username = AppInstance.Username,
                Channel = channel
            });

            if (session != null)
            {
                Messages = null;

                AppInstance.Session = session;
                AppInstance.Channel = channel;

                await ChatRepository.StartReplication();

                await InitAsync();
            }
        }

        void Logout()
        {
            AppInstance.Reset();
            ChatRepository.Dispose();
            Navigation.SetRoot<LoginViewModel>(false);
        }
    }
}
