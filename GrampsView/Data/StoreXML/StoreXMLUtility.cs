// Copyright (c) phandcock.  All rights reserved.

using CommunityToolkit.Diagnostics;

using GrampsView.Common.CustomClasses;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.Data.Repository;
using GrampsView.Data.StoreXML;
using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Date;
using GrampsView.Models.DataModels.Interfaces;

using SharedSharp.Errors;

using SkiaSharp;

using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using static GrampsView.Common.CommonEnums;

namespace GrampsView.Data.ExternalStorage
{
    /// <summary>
    /// Various utility and loading routines for XML data.
    /// </summary>
    /// <seealso cref="Data.StoreXML.IStoreXML"/>
    /// <seealso cref="IStoreXML"/>
    public partial class StoreXML : IStoreXML
    {
        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="a">
        /// a.
        /// </param>
        /// <returns>
        /// Text of the XML Attribute.
        /// </returns>
        private static string GetAttribute(XAttribute? a)
        {
            return a == null ? string.Empty : ((string)a).Trim();
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="a">
        /// a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// </returns>
        private static string GetAttribute(XElement a, string b)
        {
            return GetAttribute(a.Attribute(b));
        }

        /// <summary>
        /// Gets the bool.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <param name="argName">
        /// Name of the argument.
        /// </param>
        /// <returns>
        /// </returns>
        private static bool GetBool(XElement xmlData, string argName)
        {
            string boolString = GetAttribute(xmlData.Attribute(argName));

            if (boolString == null)
            {
                return false;
            }

            switch (boolString)
            {
                case "0":
                    {
                        return true;
                    }

                case "1":
                    {
                        return false;
                    }

                default:
                    {
                        return false;
                    }
            }
        }

        private static DataConfidence GetDataConfidence(XElement a)
        {
            string t = ((string)a.Element(ns + "confidence")).Trim();

            switch (t)
            {
                case "4":
                    {
                        return DataConfidence.VeryHigh;
                    }
                case "3":
                    {
                        return DataConfidence.High;
                    }
                case "2":
                    {
                        return DataConfidence.Normal;
                    }
                case "1":
                    {
                        return DataConfidence.Low;
                    }
                case "0":
                    {
                        return DataConfidence.VeryLow;
                    }

                default:
                    {
                        break;
                    }
            }

            return DataConfidence.Normal;
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private static DateObjectModelBase GetDate(XElement xmlData)
        {
            return SetDate(xmlData);
        }

        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <param name="a">
        /// a.
        /// </param>
        /// <returns>
        /// Text of the XML Element.
        /// </returns>
        private static string GetElement(XElement a)
        {
            return a == null ? string.Empty : ((string)a).Trim();
        }

        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <param name="a">
        /// a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// Text of the XML element in the provided namespace.
        /// </returns>
        private static string GetElement(XElement a, string b)
        {
            return a == null ? string.Empty : GetElement(a.Element(ns + b));
        }

        private static HLinkKey GetHLinkKey(XElement argElement)
        {
            return argElement == null ? new HLinkKey() : new HLinkKey(GetAttribute(argElement, "hlink"));
        }

        /// <summary>Sets the private object.</summary>
        /// <param name="xmlData"></param>
        /// <returns>True or False depending on if the object is private.</returns>
        private static bool GetPrivateObject(XElement xmlData)
        {
            string t = GetAttribute(xmlData.Attribute("priv"));

            return t == "1";
        }

        private static TextStyle GetTextStyle(XElement a)
        {
            XAttribute t = a.Attribute("name");

            switch (t.Value)
            {
                case "bold":
                    {
                        return TextStyle.bold;
                    }
                case "italic":
                    {
                        return TextStyle.italic;
                    }
                case "underline":
                    {
                        return TextStyle.underline;
                    }
                case "fontface":
                    {
                        return TextStyle.fontface;
                    }
                case "fontsize":
                    {
                        return TextStyle.fontsize;
                    }
                case "fontcolor":
                    {
                        return TextStyle.fontcolor;
                    }
                case "highlight":
                    {
                        return TextStyle.highlight;
                    }
                case "superscript":
                    {
                        return TextStyle.superscript;
                    }
                case "link":
                    {
                        return TextStyle.link;
                    }

                default:
                    {
                        break;
                    }
            }

            return TextStyle.unknown;
        }

        private async Task<HLinkMediaModel> CreateClippedMediaModel(HLinkMediaModel argHLinkLoadImageModel)
        {
            try
            {
                if (argHLinkLoadImageModel is null)
                {
                    throw new ArgumentNullException(nameof(argHLinkLoadImageModel));
                }

                if (!argHLinkLoadImageModel.DeRef.Valid)
                {
                    throw new ArgumentException("CreateClippedMediaModel argument is invalid", nameof(argHLinkLoadImageModel));
                }

                HLinkKey temp = argHLinkLoadImageModel.HLinkKey;

                IMediaModel returnMediaModel = await MainThread.InvokeOnMainThreadAsync(() =>
             {
                 // TODO cleanup code. Multiple copies of things in use

                 IMediaModel theMediaModel = argHLinkLoadImageModel.DeRef;

                 SKBitmap resourceBitmap = new();

                 IMediaModel newMediaModel = new MediaModel();

                 HLinkKey newHLinkKey = new($"{argHLinkLoadImageModel.HLinkKey.Value}-{argHLinkLoadImageModel.GCorner1X}{argHLinkLoadImageModel.GCorner1Y}{argHLinkLoadImageModel.GCorner2X}{argHLinkLoadImageModel.GCorner2Y}");
                 string outFileName = $"{newHLinkKey.Value}{"~crop"}.png";

                 if (newHLinkKey.Value == "_c5132553a185c9c51d8-35415065")
                 {
                 }

                 Debug.WriteLine(newHLinkKey.Value);

                 string outFilePath = Path.Combine(DataStore.Instance.AD.CurrentImageAssetsFolder.FolderAsString, outFileName);

                 Debug.WriteLine(argHLinkLoadImageModel.DeRef.CurrentStorageFile);

                 // Check if already exists
                 IMediaModel fileExists = DV.MediaDV.GetModelFromHLinkKey(newHLinkKey);

                 if ((!fileExists.Valid) && theMediaModel.CurrentStorageFile.Valid)
                 {
                     // Needs clipping
                     using (StreamReader stream = new(theMediaModel.CurrentStorageFile.GetAbsoluteFilePath))
                     {
                         //resourceBitmap = SKBitmap.Decode(stream.BaseStream);
                         // TODO See https://github.com/mono/SkiaSharp/issues/1621

                         SKImage img = SKImage.FromEncodedData(stream.BaseStream);
                         resourceBitmap = SKBitmap.FromImage(img);
                     }

                     // Check for too large a bitmap
                     //Debug.WriteLine("Image ResourceBitmap size: " + resourceBitmap.ByteCount);
                     if (resourceBitmap.ByteCount > int.MaxValue - 1000)
                     {
                         // TODO Handle this better. Perhaps resize? Delete for now
                         resourceBitmap = new SKBitmap();
                     }

                     float crleft = (float)(argHLinkLoadImageModel.GCorner1X / 100d * theMediaModel.MetaDataWidth);
                     float crright = (float)(argHLinkLoadImageModel.GCorner2X / 100d * theMediaModel.MetaDataWidth);
                     float crtop = (float)(argHLinkLoadImageModel.GCorner1Y / 100d * theMediaModel.MetaDataHeight);
                     float crbottom = (float)(argHLinkLoadImageModel.GCorner2Y / 100d * theMediaModel.MetaDataHeight);

                     SKRect cropRect = new(crleft, crtop, crright, crbottom);

                     SKBitmap croppedBitmap = new(
                                                         (int)cropRect.Width,
                                                         (int)cropRect.Height
                                                         );

                     SKRect dest = new(
                                             0,
                                             0,
                                             cropRect.Width,
                                             cropRect.Height
                                             );

                     SKRect source = new(
                                             cropRect.Left,
                                             cropRect.Top,
                                             cropRect.Right,
                                             cropRect.Bottom);

                     using (SKCanvas canvas = new(croppedBitmap))
                     {
                         canvas.DrawBitmap(resourceBitmap, source, dest);
                     }

                     // create an image COPY
                     SKImage image = SKImage.FromBitmap(croppedBitmap);

                     // encode the image (defaults to PNG)
                     SKData encoded = image.Encode();

                     // get a stream over the encoded data

                     using (Stream stream = File.Open(outFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                     {
                         encoded.SaveTo(stream);
                     }

                     croppedBitmap.Dispose();

                     // ------------ Save new MediaObject
                     newMediaModel = SharedSharpGeneral.CopyObject<MediaModel>(theMediaModel);
                     newMediaModel.HLinkKey = newHLinkKey;

                     newMediaModel.OriginalFilePath = outFilePath;
                     newMediaModel.CurrentStorageFile = new FileInfoEx(argFileName: outFilePath);

                     newMediaModel.IsInternalMediaFile = true;
                     newMediaModel.InternalMediaFileOriginalHLink = theMediaModel.HLinkKey;

                     newMediaModel.MetaDataHeight = cropRect.Height;
                     newMediaModel.MetaDataWidth = cropRect.Width;

                     // newMediaModel = SetHomeImage(newMediaModel);

                     DataStore.Instance.DS.MediaData.Add((MediaModel)newMediaModel);
                 }
                 else
                 {
                     ErrorInfo t = new("File not found when Region specified in ClipMedia")

                     {
                     { "Original ID", theMediaModel.Id },
                     { "Original File", theMediaModel.OriginalFilePath },
                     { "Clipped Id", argHLinkLoadImageModel.DeRef.Id }
                     };

                     MyNotifications.NotifyError(t);
                 }

                 resourceBitmap.Dispose();

                 return newMediaModel;
             }).ConfigureAwait(false);

                HLinkMediaModel tt = returnMediaModel.HLink;
                tt.OriginalMediaHLink = temp;

                return tt;
            }
            catch (Exception ex)
            {
                MyNotifications.NotifyException("Load Clipped Media", ex);
            }

            return new HLinkMediaModel();
        }

        private ModelBase GetBasics(XElement argElement)
        {
            ModelBase returnVal = new()
            {
                Id = GetAttribute(argElement.Attribute("id")),
                Change = GetDateTime(argElement, "change"),
                Priv = GetPrivateObject(argElement),
            };

            returnVal.HLinkKey.Value = GetAttribute(argElement, "handle");

            return returnVal;
        }

        /// <summary>
        /// Gets the colour.
        /// </summary>
        /// <param name="a">
        /// a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// </returns>

        private Color GetColour(XElement a, string b)
        {
            Guard.IsNotNull(a, nameof(a));

            try
            {
                const string ColorNotSet = "#000000000000";

                Regex regexColorCode = new("^#[a-fA-F0-9]{6}$");

                string hexColour = GetAttribute(a.Attribute(b));

                // Validate
                if (!regexColorCode.IsMatch(hexColour.Trim()) && hexColour != ColorNotSet)
                {
                    ErrorInfo argErrorDetail = new("Bad colour in GetColour")
                    {
                        { "Color element is", a.ToString() },
                        { "Attribute is", b }
                    };

                    MyNotifications.NotifyError(argErrorDetail);

                    hexColour = "#000000";
                }

                if (hexColour == ColorNotSet)
                {
                    hexColour = "#000000";
                }

                return Color.FromArgb(hexColour);
            }
            catch (Exception ex)
            {
                MyNotifications.NotifyException("Error in XML Utils GetColour", ex);
                return new Color();
            }
        }

        private DateTime GetDateTime(XElement a, string b)
        {
            string argUnixSecs = GetAttribute(a.Attribute(b));

            return GetDateTime(argUnixSecs);
        }

        private DateTime GetDateTime(string argUnixSecs)
        {
            if (!long.TryParse(argUnixSecs, out long ls))
            {
                MyNotifications.NotifyError(new ErrorInfo("The value passed to GetDateTime was not a valid number of Unix seconds"));
            };

            DateTimeOffset t = DateTimeOffset.FromUnixTimeSeconds(ls);

            // TODO This is in UTC and needs to be converted to local time

            return t.DateTime;
        }

        /// <summary>
        /// Converts a string into a uri (if it can).
        /// </summary>
        /// <param name="xmlData">
        /// string from XML.
        /// </param>
        private Uri? GetUri(string xmlData)
        {
            try
            {
                xmlData = xmlData.Trim();

                Uri uri = new UriBuilder(xmlData).Uri;

                return uri;
            }
            catch (UriFormatException ex)
            {
                ErrorInfo t = new("The URI in the Internet address is not well formed")
                    {
                        { "Exception Message ", ex.Message },
                        { "URI", xmlData }
                    };

                MyNotifications.NotifyError(t);

                return null;
            }
            catch (FormatException ex)
            {
                ErrorInfo t = new("The URI in the Internet address is not in the correct format")
                    {
                        { "Exception Message ", ex.Message },
                        { "URI", xmlData }
                    };

                MyNotifications.NotifyError(t);

                return null;
            }
            catch (Exception ex)
            {
                MyNotifications.NotifyException("Exception in GetUri", ex);

                return null;
            }
        }

        ///// <summary>
        ///// Gets the h link.
        ///// </summary>
        ///// <param name="xmlData">
        ///// The XML data.
        ///// </param>
        ///// <returns>
        ///// </returns>
        //private static HLinkBase HLink(XElement xmlData)
        //{
        //    HLinkBase t = new HLinkBase();

        // if (xmlData != null) { t.HLinkKey = GetHLinkKey(xmlData.Attribute("hlink")); }

        //    return t;
        //}
    }
}