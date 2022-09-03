namespace GrampsView.Common.CustomClasses
{
    using GrampsView.Data.Model;

    using System;

    using Xamarin.CommunityToolkit.ObjectModel;

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
                return Constants.CompareLessThan;
            }

            return Value.CompareTo(argOther.Value);
        }

        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                return Constants.CompareLessThan;
            }

            return this.CompareTo((obj as HLinkBase).HLinkKey);
        }
    }
}