using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GrampsView.Converters
{
    /// <summary>
    /// Convert Xamarin.Forms.Color to Hex string and back. The normal converter seems to reset
    /// eveything to the 0 value. <note type="note"> TODO Remove when serial convertor fixed. </note>
    /// </summary>
    /// <seealso cref="System.Text.Json.JsonConverter"/>
    internal class JsonColorConverter : JsonConverter<Color>
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Color);
        }

        [Obsolete]
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Color.FromHex(Convert.ToString(reader.GetString(), System.Globalization.CultureInfo.CurrentCulture));
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToHex());
        }
    }
}