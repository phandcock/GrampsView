using GrampsView.Models.DataModels.Date;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System.Text.Json;
using System.Text.Json.Serialization;

using static GrampsView.Common.CommonEnums;

namespace GrampsView.Converters
{
    /// <summary>
    /// Convert DtaeObjectModel to the underlying DateObjectModel derived class. <note type="note">
    /// TODO Remove when serial convertor polymorphism fixed. </note>
    /// </summary>
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
                ErrorInfo tt = new("JsonDateObjectModelConverter", "Unexpected Reader TokenType.")
                {
                    { "Expected Token", JsonTokenType.StartObject.ToString() },
                    { "Found Token", reader.TokenType.ToString() }
                };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(new JsonException(), tt);
            }

            _ = reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                ErrorInfo tt = new("JsonDateObjectModelConverter", "Unexpected Reader TokenType.")
                {
                    { "Expected Token", JsonTokenType.PropertyName.ToString() },
                    { "Found Token", reader.TokenType.ToString() }
                };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(new JsonException(), tt);
            }

            string propertyName = reader.GetString();
            if (propertyName != "TypeDiscriminator")
            {
                ErrorInfo tt = new("JsonDateObjectModelConverter", "Unexpected Reader TokenType.")
                {
                    { "Expected Property Name", "TypeDiscriminator" },
                    { "Found Property Name", propertyName }
                };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(new JsonException(), tt);
            }

            _ = reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                ErrorInfo tt = new("JsonDateObjectModelConverter", "Unexpected Reader TokenType.")
                {
                    { "Expected Token", JsonTokenType.Number.ToString() },
                    { "Found Token", reader.TokenType.ToString() }
                };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(new JsonException(), tt);
            }

            DateObjectModelDerivedTypeEnum typeDiscriminator = (DateObjectModelDerivedTypeEnum)reader.GetInt32();

            switch (typeDiscriminator)

            {
                case DateObjectModelDerivedTypeEnum.DateObjectInvalid:
                    {
                        _ = reader.Read();

                        return new DateObjectModel();
                    }
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
                case DateObjectModelDerivedTypeEnum.DateObjectModelUnknown:
                    {
                        return readDOM(ref reader);
                    }
                default:
                    {
                        ErrorInfo tt = new("JsonDateObjectModelConverter", "Unexpected TypeDiscriminator.")
                        {
                            { "Found TypeDiscriminator", typeDiscriminator.ToString() }
                        };

                        Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(new JsonException(), tt);
                        break;
                    }
            };

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, DateObjectModel value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Handle Invalid
            if (!value.Valid)
            {
                writer.WriteNumber("TypeDiscriminator", (int)DateObjectModelDerivedTypeEnum.DateObjectInvalid);
            }

            // Write specific fields
            else if (value is DateObjectModel dom)
            {
                writer.WriteNumber("TypeDiscriminator", (int)DateObjectModelDerivedTypeEnum.DateObjectModelUnknown);

                writeDOM(ref writer, dom);
            }
            else if (value is DateObjectModelRange range)
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
            else
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(new JsonException(),
                                                                                            new ErrorInfo("Exception in JsonDateObjectModelConverter - Write")
                                                                                            {
                                                                                              new CardListLine("Value",value.ToString()),
                                                                                            });
            }

            writer.WriteEndObject();
        }

        private DateObjectModel readDOM(ref Utf8JsonReader argReader)
        {
            DateObjectModel returnDate = new DateObjectModelStr();

            while (argReader.Read())
            {
                if (argReader.TokenType == JsonTokenType.EndObject)
                {
                    return returnDate;
                }

                if (argReader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = argReader.GetString();
                    _ = argReader.Read();
                    switch (propertyName)
                    {
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
                                returnDate.ModelItemGlyph.SymbolColour = Color.FromArgb(Convert.ToString(argReader.GetString(), System.Globalization.CultureInfo.CurrentCulture));
                                break;
                            }
                        case "NotionalDate":
                            {
                                returnDate.NotionalDate = argReader.GetDateTime();
                                break;
                            }
                        case "Valid":
                            {
                                bool argValue = argReader.GetBoolean();
                                returnDate.Valid = argValue;
                                break;
                            }
                        case "ValidDay":
                            {
                                bool argValue = argReader.GetBoolean();
                                returnDate.ValidDay = argValue;
                                break;
                            }
                        case "ValidMonth":
                            {
                                bool argValue = argReader.GetBoolean();
                                returnDate.ValidMonth = argValue;
                                break;
                            }
                        case "ValidYear":
                            {
                                bool argValue = argReader.GetBoolean();
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

        private DateObjectModelRange readRange(ref Utf8JsonReader argReader)
        {
            DateObjectModelRange returnDate = new();

            while (argReader.Read())
            {
                if (argReader.TokenType == JsonTokenType.EndObject)
                {
                    return returnDate;
                }

                if (argReader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = argReader.GetString();
                    _ = argReader.Read();
                    switch (propertyName)
                    {
                        case "GDualdated":
                            {
                                bool argValue = argReader.GetBoolean();
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
                                returnDate.ModelItemGlyph.SymbolColour = Color.FromArgb(Convert.ToString(argReader.GetString(), System.Globalization.CultureInfo.CurrentCulture));
                                break;
                            }
                        case "NotionalDate":
                            {
                                returnDate.NotionalDate = argReader.GetDateTime();
                                break;
                            }
                        case "Valid":
                            {
                                bool argValue = argReader.GetBoolean();
                                returnDate.Valid = argValue;
                                break;
                            }
                        case "ValidDay":
                            {
                                bool argValue = argReader.GetBoolean();
                                returnDate.ValidDay = argValue;
                                break;
                            }
                        case "ValidMonth":
                            {
                                bool argValue = argReader.GetBoolean();
                                returnDate.ValidMonth = argValue;
                                break;
                            }
                        case "ValidYear":
                            {
                                bool argValue = argReader.GetBoolean();
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
            DateObjectModelSpan returnDate = new();

            while (argReader.Read())
            {
                if (argReader.TokenType == JsonTokenType.EndObject)
                {
                    return returnDate;
                }

                if (argReader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = argReader.GetString();
                    _ = argReader.Read();
                    switch (propertyName)
                    {
                        case "GDualdated":
                            {
                                bool argValue = argReader.GetBoolean();
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
                                returnDate.ModelItemGlyph.SymbolColour = Color.FromArgb(Convert.ToString(argReader.GetString(), System.Globalization.CultureInfo.CurrentCulture));
                                break;
                            }
                        case "NotionalDate":
                            {
                                returnDate.NotionalDate = argReader.GetDateTime();
                                break;
                            }
                        case "Valid":
                            {
                                bool argValue = argReader.GetBoolean();
                                returnDate.Valid = argValue;
                                break;
                            }
                        case "ValidDay":
                            {
                                bool argValue = argReader.GetBoolean();
                                returnDate.ValidDay = argValue;
                                break;
                            }
                        case "ValidMonth":
                            {
                                bool argValue = argReader.GetBoolean();
                                returnDate.ValidMonth = argValue;
                                break;
                            }
                        case "ValidYear":
                            {
                                bool argValue = argReader.GetBoolean();
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
            DateObjectModelStr returnDate = new();

            while (argReader.Read())
            {
                if (argReader.TokenType == JsonTokenType.EndObject)
                {
                    return returnDate;
                }

                if (argReader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = argReader.GetString();
                    _ = argReader.Read();
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
                                returnDate.ModelItemGlyph.SymbolColour = Color.FromArgb(Convert.ToString(argReader.GetString(), System.Globalization.CultureInfo.CurrentCulture));
                                break;
                            }
                        case "NotionalDate":
                            {
                                returnDate.NotionalDate = argReader.GetDateTime();
                                break;
                            }
                        case "Valid":
                            {
                                bool argValue = argReader.GetBoolean();
                                returnDate.Valid = argValue;
                                break;
                            }
                        case "ValidDay":
                            {
                                bool argValue = argReader.GetBoolean();
                                returnDate.ValidDay = argValue;
                                break;
                            }
                        case "ValidMonth":
                            {
                                bool argValue = argReader.GetBoolean();
                                returnDate.ValidMonth = argValue;
                                break;
                            }
                        case "ValidYear":
                            {
                                bool argValue = argReader.GetBoolean();
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
            DateObjectModelVal returnDate = new();

            while (argReader.Read())
            {
                if (argReader.TokenType == JsonTokenType.EndObject)
                {
                    return returnDate;
                }

                if (argReader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = argReader.GetString();
                    _ = argReader.Read();
                    switch (propertyName)
                    {
                        case "GDualdated":
                            {
                                bool argValue = argReader.GetBoolean();
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
                                returnDate.ModelItemGlyph.SymbolColour = Color.FromArgb(Convert.ToString(argReader.GetString(), System.Globalization.CultureInfo.CurrentCulture));
                                break;
                            }
                        case "NotionalDate":
                            {
                                returnDate.NotionalDate = argReader.GetDateTime();
                                break;
                            }
                        case "Valid":
                            {
                                bool argValue = argReader.GetBoolean();
                                returnDate.Valid = argValue;
                                break;
                            }
                        case "ValidDay":
                            {
                                bool argValue = argReader.GetBoolean();
                                returnDate.ValidDay = argValue;
                                break;
                            }
                        case "ValidMonth":
                            {
                                bool argValue = argReader.GetBoolean();
                                returnDate.ValidMonth = argValue;
                                break;
                            }
                        case "ValidYear":
                            {
                                bool argValue = argReader.GetBoolean();
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

        private void writeDOM(ref Utf8JsonWriter argWriter, DateObjectModel argVal)
        {
            writeBasic(ref argWriter, argVal);
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