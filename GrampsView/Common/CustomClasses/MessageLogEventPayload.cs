namespace GrampsView.Common.CustomClasses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using static GrampsView.Common.CommonEnums;

    public class MessageLogEventPayload : EventArgs
    {
        public MessageLogEventType Type
        {
            get; set;
        }

        public string Text
        {
            get; set;
        }

        public MessageLogEventPayload(MessageLogEventType argType, string argText)
        {
            Type = argType;
            Text = argText;
        }
    }
}
