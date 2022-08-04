namespace GrampsView.Events
{
    using CommunityToolkit.Mvvm.Messaging.Messages;

    public class DataLoadStartEvent : ValueChangedMessage<bool>
    {
        public DataLoadStartEvent(bool value) : base(value)
        {
        }
    }
}