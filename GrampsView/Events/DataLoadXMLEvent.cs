namespace GrampsView.Events
{
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class DataLoadXMLEvent : ValueChangedMessage<bool>
    {
        public DataLoadXMLEvent(bool value) : base(value)
        {
        }
    }
}