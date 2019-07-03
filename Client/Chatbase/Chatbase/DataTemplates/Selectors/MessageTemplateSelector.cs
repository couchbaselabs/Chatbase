using Chatbase.Core;
using Chatbase.DataTemplates.ViewCells;
using Chatbase.Models;
using Xamarin.Forms;

namespace Chatbase.DataTemplates.Selectors
{
    class MessageTemplateSelector : DataTemplateSelector
    {
        readonly DataTemplate incomingDataTemplate;
        readonly DataTemplate outgoingDataTemplate;

        public MessageTemplateSelector()
        {
            incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var message = item as Message;

            if (message == null)
                return null;

            return (message.Author == AppInstance.Username) ? outgoingDataTemplate : incomingDataTemplate;
        }

    }
}