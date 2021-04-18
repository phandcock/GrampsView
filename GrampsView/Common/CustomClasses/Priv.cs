using System.Globalization;

namespace GrampsView.Common.CustomClasses
{
    public class Priv : CommonBindableBase
    {
        public Priv(bool argValue)
        {
            Value = argValue;
        }

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