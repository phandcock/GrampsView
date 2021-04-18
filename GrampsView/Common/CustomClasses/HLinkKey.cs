namespace GrampsView.Common.CustomClasses
{
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

        public string Value
        {
            get; set;
        } = string.Empty;
    }
}