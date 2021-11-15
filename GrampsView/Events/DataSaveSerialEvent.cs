namespace GrampsView.Events
{
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class DataSaveSerialEvent : ValueChangedMessage<bool>
    {
        public DataSaveSerialEvent(bool value) : base(value)
        {
        }
    }
}