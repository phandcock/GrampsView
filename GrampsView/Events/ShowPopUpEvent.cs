namespace GrampsView.Events
{
    using CommunityToolkit.Mvvm.Messaging.Messages;

    public class ShowPopUpEvent : ValueChangedMessage<bool>
    {
        public ShowPopUpEvent(bool value) : base(value)
        {
        }
    }
}