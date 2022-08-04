namespace GrampsView.Events
{
    using CommunityToolkit.Mvvm.Messaging.Messages;

    public class DataLoadXMLEvent : ValueChangedMessage<bool>
    {
        public DataLoadXMLEvent(bool value) : base(value)
        {
        }
    }
}