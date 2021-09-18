﻿namespace GrampsView.Converters
{
    using GrampsView.Data.Model;

    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using Xamarin.Forms;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// Convert DtaeObjectModel to the underlying DateObjectModel derived class. <note type="note">
    /// TODO Remove when serial convertor polymorphism fixed. </note>
    /// </summary>
    /// <seealso cref="System.Text.Json.JsonConverter"/>
    internal class JsonDateObjectModelConverter : JsonConverter<DateObjectModel>
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateObjectModel);
        }

        public override DateObjectModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyName = reader.GetString();
            if (propertyName != "TypeDiscriminator")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            DateObjectModelDerivedTypeEnum typeDiscriminator = (DateObjectModelDerivedTypeEnum)reader.GetInt32();

            switch (typeDiscriminator)

            {
                case DateObjectModelDerivedTypeEnum.DateObjectModelRange:
                    {
                        return readRange(ref reader);
                    }

                case DateObjectModelDerivedTypeEnum.DateObjectModelSpan:
                    {
                        return readSpan(ref reader);
                    }
                case DateObjectModelDerivedTypeEnum.DateObjectModelStr:
                    {
                        return readStr(ref reader);
                    }
                case DateObjectModelDerivedTypeEnum.DateObjectModelVal:
                    {
                        return readVal(ref reader);
                    }
                default:
                    {
                        throw new JsonException();
                    }
            };

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, DateObjectModel value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Write specific fields
            if (value is DateObjectModelRange range)
            {
                writer.WriteNumber("TypeDiscriminator", (int)DateObjectModelDerivedTypeEnum.DateObjectModelRange);

                writeRange(ref writer, range);
            }
            else if (value is DateObjectModelSpan span)
            {
                writer.WriteNumber("TypeDiscriminator", (int)DateObjectModelDerivedTypeEnum.DateObjectModelSpan);

                writeSpan(ref writer, span);
            }
            else if (value is DateObjectModelStr str)
            {
                writer.WriteNumber("TypeDiscriminator", (int)DateObjectModelDerivedTypeEnum.DateObjectModelStr);

                writeStr(ref writer, str);
            }
            else if (value is DateObjectModelVal val)
            {
                writer.WriteNumber("TypeDiscriminator", (int)DateObjectModelDerivedTypeEnum.DateObjectModelVal);

                writeVal(ref writer, val);
            }

            writer.WriteEndObject();
        }

        private DateObjectModelRange readRange(ref Utf8JsonReader argReader)
        {
            DateObjectModelRange returnDate = new DateObjectModelRange();

            while (argReader.Read())
            {
                if (argReader.TokenType == JsonTokenType.EndObject)
                {
                    return returnDate;
                }

                if (argReader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = argReader.GetString();
                    argReader.Read();
                    switch (propertyName)
                    {
                        case "GDualdated":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.GDualdated = argValue;
                                break;
                            }
                        case "GQuality":
                            {
                                returnDate.GQuality = (DateQuality)argReader.GetInt32();
                                break;
                            }
                        case "GStart":
                            {
                                //argReader.Read(); // Start token
                                returnDate.GStart = readVal(ref argReader);
                                //argReader.Read();  // End Token
                                break;
                            }
                        case "GStop":
                            {
                                //argReader.Read(); // Start token
                                returnDate.GStop = readVal(ref argReader);
                                //argReader.Read();  // End Token
                                break;
                            }
                        case "GCformat":
                            {
                                returnDate.GCformat = argReader.GetString();

                                break;
                            }
                        case "GNewYear":
                            {
                                returnDate.GNewYear = argReader.GetString();
                                break;
                            }

                        case "HLinkKey":
                            {
                                returnDate.HLinkKey.Value = argReader.GetString();
                                break;
                            }
                        case "ModelItemGlyph.Symbol":
                            {
                                returnDate.ModelItemGlyph.Symbol = argReader.GetString();
                                break;
                            }
                        case "ModelItemGlyph.SymbolColour":
                            {
                                returnDate.ModelItemGlyph.SymbolColour = Color.FromHex(Convert.ToString(argReader.GetString(), System.Globalization.CultureInfo.CurrentCulture));
                                break;
                            }
                        case "NotionalDate":
                            {
                                returnDate.NotionalDate = argReader.GetDateTime();
                                break;
                            }
                        case "Valid":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.Valid = argValue;
                                break;
                            }
                        case "ValidDay":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.ValidDay = argValue;
                                break;
                            }
                        case "ValidMonth":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.ValidMonth = argValue;
                                break;
                            }
                        case "ValidYear":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.ValidYear = argValue;
                                break;
                            }
                        default:
                            {
                                throw new JsonException();
                            }
                    }
                }
            }

            return returnDate;
        }

        private DateObjectModelSpan readSpan(ref Utf8JsonReader argReader)
        {
            DateObjectModelSpan returnDate = new DateObjectModelSpan();

            while (argReader.Read())
            {
                if (argReader.TokenType == JsonTokenType.EndObject)
                {
                    return returnDate;
                }

                if (argReader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = argReader.GetString();
                    argReader.Read();
                    switch (propertyName)
                    {
                        case "GDualdated":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.GDualdated = argValue;
                                break;
                            }
                        case "GQuality":
                            {
                                returnDate.GQuality = (DateQuality)argReader.GetInt32();
                                break;
                            }
                        case "GStart":
                            {
                                returnDate.GStart = readVal(ref argReader);

                                break;
                            }
                        case "GStop":
                            {
                                returnDate.GStop = readVal(ref argReader);

                                break;
                            }
                        case "GCformat":
                            {
                                returnDate.GCformat = argReader.GetString();

                                break;
                            }
                        case "GNewYear":
                            {
                                returnDate.GNewYear = argReader.GetString();
                                break;
                            }

                        case "HLinkKey":
                            {
                                returnDate.HLinkKey.Value = argReader.GetString();
                                break;
                            }
                        case "ModelItemGlyph.Symbol":
                            {
                                returnDate.ModelItemGlyph.Symbol = argReader.GetString();
                                break;
                            }
                        case "ModelItemGlyph.SymbolColour":
                            {
                                returnDate.ModelItemGlyph.SymbolColour = Color.FromHex(Convert.ToString(argReader.GetString(), System.Globalization.CultureInfo.CurrentCulture));
                                break;
                            }
                        case "NotionalDate":
                            {
                                returnDate.NotionalDate = argReader.GetDateTime();
                                break;
                            }
                        case "Valid":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.Valid = argValue;
                                break;
                            }
                        case "ValidDay":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.ValidDay = argValue;
                                break;
                            }
                        case "ValidMonth":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.ValidMonth = argValue;
                                break;
                            }
                        case "ValidYear":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.ValidYear = argValue;
                                break;
                            }
                        default:
                            {
                                throw new JsonException();
                            }
                    }
                }
            }

            return returnDate;
        }

        private DateObjectModelStr readStr(ref Utf8JsonReader argReader)
        {
            DateObjectModelStr returnDate = new DateObjectModelStr();

            while (argReader.Read())
            {
                if (argReader.TokenType == JsonTokenType.EndObject)
                {
                    return returnDate;
                }

                if (argReader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = argReader.GetString();
                    argReader.Read();
                    switch (propertyName)
                    {
                        case "GVal":
                            {
                                returnDate.GVal = argReader.GetString();

                                break;
                            }
                        case "HLinkKey":
                            {
                                returnDate.HLinkKey.Value = argReader.GetString();
                                break;
                            }
                        case "ModelItemGlyph.Symbol":
                            {
                                returnDate.ModelItemGlyph.Symbol = argReader.GetString();
                                break;
                            }
                        case "ModelItemGlyph.SymbolColour":
                            {
                                returnDate.ModelItemGlyph.SymbolColour = Color.FromHex(Convert.ToString(argReader.GetString(), System.Globalization.CultureInfo.CurrentCulture));
                                break;
                            }
                        case "NotionalDate":
                            {
                                returnDate.NotionalDate = argReader.GetDateTime();
                                break;
                            }
                        case "Valid":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.Valid = argValue;
                                break;
                            }
                        case "ValidDay":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.ValidDay = argValue;
                                break;
                            }
                        case "ValidMonth":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.ValidMonth = argValue;
                                break;
                            }
                        case "ValidYear":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.ValidYear = argValue;
                                break;
                            }
                        default:
                            {
                                throw new JsonException();
                            }
                    }
                }
            }

            return returnDate;
        }

        private DateObjectModelVal readVal(ref Utf8JsonReader argReader)
        {
            DateObjectModelVal returnDate = new DateObjectModelVal();

            while (argReader.Read())
            {
                if (argReader.TokenType == JsonTokenType.EndObject)
                {
                    return returnDate;
                }

                if (argReader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = argReader.GetString();
                    argReader.Read();
                    switch (propertyName)
                    {
                        case "GDualdated":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.GDualdated = argValue;
                                break;
                            }
                        case "GQuality":
                            {
                                returnDate.GQuality = (DateQuality)argReader.GetInt32();
                                break;
                            }
                        case "GValType":
                            {
                                returnDate.GValType = (DateValType)argReader.GetInt32();

                                break;
                            }

                        case "GCformat":
                            {
                                returnDate.GCformat = argReader.GetString();

                                break;
                            }
                        case "GNewYear":
                            {
                                returnDate.GNewYear = argReader.GetString();
                                break;
                            }
                        case "GVal":
                            {
                                returnDate.GVal = argReader.GetString();

                                break;
                            }
                        case "HLinkKey":
                            {
                                returnDate.HLinkKey.Value = argReader.GetString();
                                break;
                            }
                        case "ModelItemGlyph.Symbol":
                            {
                                returnDate.ModelItemGlyph.Symbol = argReader.GetString();
                                break;
                            }
                        case "ModelItemGlyph.SymbolColour":
                            {
                                returnDate.ModelItemGlyph.SymbolColour = Color.FromHex(Convert.ToString(argReader.GetString(), System.Globalization.CultureInfo.CurrentCulture));
                                break;
                            }
                        case "NotionalDate":
                            {
                                returnDate.NotionalDate = argReader.GetDateTime();
                                break;
                            }
                        case "Valid":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.Valid = argValue;
                                break;
                            }
                        case "ValidDay":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.ValidDay = argValue;
                                break;
                            }
                        case "ValidMonth":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.ValidMonth = argValue;
                                break;
                            }
                        case "ValidYear":
                            {
                                Boolean argValue = argReader.GetBoolean();
                                returnDate.ValidYear = argValue;
                                break;
                            }
                        default:
                            {
                                throw new JsonException();
                            }
                    }
                }
            }

            return returnDate;
        }

        private void writeBasic(ref Utf8JsonWriter argWriter, DateObjectModel argBasic)
        {
            argWriter.WriteString("ModelItemGlyph.Symbol", argBasic.ModelItemGlyph.Symbol);

            argWriter.WriteString("ModelItemGlyph.SymbolColour", argBasic.ModelItemGlyph.SymbolColour.ToHex());

            argWriter.WriteString("HLinkKey", argBasic.HLinkKey.Value);

            argWriter.WriteString("NotionalDate", argBasic.NotionalDate);

            argWriter.WriteBoolean("Valid", argBasic.Valid);
            argWriter.WriteBoolean("ValidDay", argBasic.ValidDay);
            argWriter.WriteBoolean("ValidMonth", argBasic.ValidMonth);
            argWriter.WriteBoolean("ValidYear", argBasic.ValidYear);
        }

        private void writeRange(ref Utf8JsonWriter argWriter, DateObjectModelRange argRange)
        {
            writeBasic(ref argWriter, argRange);

            argWriter.WriteString("GCformat", argRange.GCformat);

            argWriter.WriteBoolean("GDualdated", argRange.GDualdated);

            argWriter.WriteString("GNewYear", argRange.GNewYear);

            argWriter.WriteNumber("GQuality", (int)argRange.GQuality);

            argWriter.WriteStartObject("GStart");
            writeVal(ref argWriter, argRange.GStart);
            argWriter.WriteEndObject();

            argWriter.WriteStartObject("GStop");
            writeVal(ref argWriter, argRange.GStop);
            argWriter.WriteEndObject();
        }

        private void writeSpan(ref Utf8JsonWriter argWriter, DateObjectModelSpan argSpan)
        {
            writeBasic(ref argWriter, argSpan);

            argWriter.WriteString("GCformat", argSpan.GCformat);

            argWriter.WriteBoolean("GDualdated", argSpan.GDualdated);

            argWriter.WriteString("GNewYear", argSpan.GNewYear);

            argWriter.WriteNumber("GQuality", (int)argSpan.GQuality);

            argWriter.WriteStartObject("GStart");
            writeVal(ref argWriter, argSpan.GStart);
            argWriter.WriteEndObject();

            argWriter.WriteStartObject("GStop");
            writeVal(ref argWriter, argSpan.GStop);
            argWriter.WriteEndObject();
        }

        private void writeStr(ref Utf8JsonWriter argWriter, DateObjectModelStr argStr)
        {
            writeBasic(ref argWriter, argStr);

            argWriter.WriteString("GVal", argStr.GVal);
        }

        private void writeVal(ref Utf8JsonWriter argWriter, DateObjectModelVal argVal)
        {
            writeBasic(ref argWriter, argVal);

            argWriter.WriteString("GCformat", argVal.GCformat);

            argWriter.WriteBoolean("GDualdated", argVal.GDualdated);

            argWriter.WriteString("GNewYear", argVal.GNewYear);

            argWriter.WriteNumber("GQuality", (int)argVal.GQuality);

            argWriter.WriteString("GVal", argVal.GVal);

            argWriter.WriteNumber("GValType", (int)argVal.GValType);
        }
    }
}