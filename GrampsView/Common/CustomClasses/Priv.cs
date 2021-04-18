namespace GrampsView.Common.CustomClasses
{
    using System.Globalization;
    using System.Runtime.Serialization;

    [DataContract]
    public class Priv : CommonBindableBase
    {
        public Priv(bool argValue)
        {
            Value = argValue;
        }

        [DataMember]
        public bool Value
        {
            get; set;
        } = false;

        public override string ToString()

        {
            return Value.ToString(CultureInfo.CurrentCulture);
        }
    }
}