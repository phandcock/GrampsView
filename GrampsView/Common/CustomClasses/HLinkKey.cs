namespace GrampsView.Common.CustomClasses
{
    using System;
    using System.Runtime.Serialization;

    using Xamarin.CommunityToolkit.ObjectModel;

    [DataContract]
    public class HLinkKey : ObservableObject, IComparable<HLinkKey>, IComparable
    {
        public HLinkKey(string argHLinkKeyValue)
        {
            Value = argHLinkKeyValue;
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

        public static HLinkKey NewAsGUID()
        {
            return new HLinkKey(Guid.NewGuid().ToString());
        }

        public int CompareTo(HLinkKey argOther)
        {
            if (!Valid)
            {
                return CommonConstants.CompareLessThan;
            }

            return Value.CompareTo(argOther.Value);
        }

        public int CompareTo(object obj)
        {
            if (obj is HLinkKey)
            {
                return CommonConstants.CompareLessThan;
            }

            return Value.CompareTo((obj as HLinkKey).Value);
        }
    }
}