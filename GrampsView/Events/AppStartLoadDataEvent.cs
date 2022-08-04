namespace GrampsView.Events
{
    using CommunityToolkit.Mvvm.Messaging.Messages;

    public class AppStartLoadDataEvent : ValueChangedMessage<bool>
    {
        public AppStartLoadDataEvent(bool value) : base(value)
        {
        }
    }
}