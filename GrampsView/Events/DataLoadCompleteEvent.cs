namespace GrampsView.Events
{
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class DataLoadCompleteEvent : ValueChangedMessage<bool>
    {
        public DataLoadCompleteEvent(bool value) : base(value)
        {
        }
    }
}