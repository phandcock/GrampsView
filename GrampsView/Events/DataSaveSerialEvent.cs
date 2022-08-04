namespace GrampsView.Events
{
    using CommunityToolkit.Mvvm.Messaging.Messages;

    public class DataSaveSerialEvent : ValueChangedMessage<bool>
    {
        public DataSaveSerialEvent(bool value) : base(value)
        {
        }
    }
}