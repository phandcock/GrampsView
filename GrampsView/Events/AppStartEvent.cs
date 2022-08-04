namespace GrampsView.Events
{
    using CommunityToolkit.Mvvm.Messaging.Messages;

    public class AppStartEvent : ValueChangedMessage<bool>
    {
        public AppStartEvent(bool value) : base(value)
        {
        }
    }
}