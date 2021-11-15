namespace GrampsView.Events
{
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class DataLoadStartEvent : ValueChangedMessage<bool>
    {
        public DataLoadStartEvent(bool value) : base(value)
        {
        }
    }
}