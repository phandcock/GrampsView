namespace GrampsView.Events
{
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class AppStartLoadDataEvent : ValueChangedMessage<bool>
    {
        public AppStartLoadDataEvent(bool value) : base(value)
        {
        }
    }
}