namespace GrampsView.Events
{
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class AppStartEvent : ValueChangedMessage<bool>
    {
        public AppStartEvent(bool value) : base(value)
        {
        }
    }
}