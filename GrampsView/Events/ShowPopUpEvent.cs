namespace GrampsView.Events
{
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class ShowPopUpEvent : ValueChangedMessage<bool>
    {
        public ShowPopUpEvent(bool value) : base(value)
        {
        }
    }
}