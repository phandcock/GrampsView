namespace GrampsView.Events
{
    using CommunityToolkit.Mvvm.Messaging.Messages;

    public class DataLoadCompleteEvent : ValueChangedMessage<bool>
    {
        public DataLoadCompleteEvent(bool value) : base(value)
        {
        }
    }
}