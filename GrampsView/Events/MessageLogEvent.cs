namespace GrampsView.Events
{
    using GrampsView.Common.CustomClasses;

    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class MessageLogEvent : ValueChangedMessage<MessageLogEventPayload>
    {
        public MessageLogEvent(MessageLogEventPayload value) : base(value)
        {
        }
    }
}