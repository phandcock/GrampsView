// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.HLinks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrampsView.Common.CustomClasses
{
    [ComplexType]
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

        [Key]
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
                return SharedConstants.CompareLessThan;
            }

            return Value.CompareTo(argOther.Value);
        }

        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                return SharedConstants.CompareLessThan;
            }

            return this.CompareTo((obj as HLinkBase).HLinkKey);
        }
    }
}