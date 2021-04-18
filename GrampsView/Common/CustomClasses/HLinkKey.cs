namespace GrampsView.Common.CustomClasses
{
    using System.Runtime.Serialization;

    [DataContract]
    public class HLinkKey : CommonBindableBase
    {
        public HLinkKey(string argHlinkKeyValue)
        {
            Value = argHlinkKeyValue;
        }

        public HLinkKey()
        {
        }

        public bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(Value);
            }
        }

        [DataMember]
        public string Value
        {
            get; set;
        } = string.Empty;
    }
}