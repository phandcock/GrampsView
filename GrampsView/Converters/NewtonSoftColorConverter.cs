namespace GrampsView.Converters
{
    using Newtonsoft.Json;

    using System;

    /// <summary>Convert Xamarin.Forms.Color to Hex string and back.  The normal converter seems to reset eveythign to the 0 values.<note type="note">TODO Remove when serial convertor fixed.</note></summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    internal class NewtonSoftColorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Xamarin.Forms.Color));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Xamarin.Forms.Color.FromHex(Convert.ToString(reader.Value, System.Globalization.CultureInfo.CurrentCulture));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Xamarin.Forms.Color)value).ToHex());
        }
    }
}