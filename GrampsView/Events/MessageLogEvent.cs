namespace GrampsView.Events
{
    using GrampsView.Common.CustomClasses;

    using CommunityToolkit.Mvvm.Messaging.Messages;

    public class MessageLogEvent : ValueChangedMessage<MessageLogEventPayload>
    {
        public MessageLogEvent(MessageLogEventPayload value) : base(value)
        {
        }
    }
}