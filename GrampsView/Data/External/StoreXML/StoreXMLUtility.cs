namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using SkiaSharp;

    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// Various utility and loading routines for XML data.
    /// </summary>
    /// <seealso cref="GrampsView.Data.ExternalStorage.IStoreXML"/>
    /// <seealso cref="IStoreXML"/>
    public partial class StoreXML : IStoreXML
    {
        public static async Task<HLinkMediaModel> CreateClippedMediaModel(HLinkLoadImageModel argHLinkLoadImageModel)
        {
            if (argHLinkLoadImageModel is null)
            {
                throw new ArgumentNullException(nameof(argHLinkLoadImageModel));
            }

            if (!argHLinkLoadImageModel.DeRef.Valid)
            {
                throw new ArgumentException("CreateClippedMediaModel argument is invalid", nameof(argHLinkLoadImageModel));
            }

            IMediaModel returnMediaModel = await MainThread.InvokeOnMainThreadAsync(() =>
         {
             // TODO cleanup code. Multiple copies of things in use

             IMediaModel theMediaModel = argHLinkLoadImageModel.DeRef;

             SKBitmap resourceBitmap = new SKBitmap();

             IMediaModel newMediaModel = new MediaModel();

             HLinkKey newHLinkKey = new HLinkKey(argHLinkLoadImageModel.HLinkKey + "-" + argHLinkLoadImageModel.GCorner1X + argHLinkLoadImageModel.GCorner1Y + argHLinkLoadImageModel.GCorner2X + argHLinkLoadImageModel.GCorner2Y);
             string outFileName = Path.Combine("Cropped", newHLinkKey.Value + ".png");

             string outFilePath = Path.Combine(DataStore.Instance.AD.CurrentDataFolder.FullName, outFileName);

             Debug.WriteLine(argHLinkLoadImageModel.DeRef.MediaStorageFilePath);

             // Check if already exists
             IMediaModel fileExists = DV.MediaDV.GetModelFromHLinkKey(newHLinkKey);

             if ((!fileExists.Valid) && (theMediaModel.IsMediaStorageFileValid))
             {
                 // Needs clipping
                 using (StreamReader stream = new StreamReader(theMediaModel.MediaStorageFilePath))
                 {
                     resourceBitmap = SKBitmap.Decode(stream.BaseStream);
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

                 SKRect cropRect = new SKRect(crleft, crtop, crright, crbottom);

                 SKBitmap croppedBitmap = new SKBitmap(
                                                     (int)cropRect.Width,
                                                     (int)cropRect.Height
                                                     );

                 SKRect dest = new SKRect(
                                         0,
                                         0,
                                         cropRect.Width,
                                         cropRect.Height
                                         );

                 SKRect source = new SKRect(
                                         cropRect.Left,
                                         cropRect.Top,
                                         cropRect.Right,
                                         cropRect.Bottom);

                 using (SKCanvas canvas = new SKCanvas(croppedBitmap))
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
                 newMediaModel = theMediaModel.Copy();
                 newMediaModel.HLinkKey = newHLinkKey;

                 newMediaModel.OriginalFilePath = outFileName;
                 newMediaModel.MediaStorageFile = StoreFolder.FolderGetFile(DataStore.Instance.AD.CurrentDataFolder, outFileName);

                 newMediaModel.IsInternalMediaFile = true;
                 newMediaModel.InternalMediaFileOriginalHLink = theMediaModel.HLinkKey;

                 newMediaModel.MetaDataHeight = cropRect.Height;
                 newMediaModel.MetaDataWidth = cropRect.Width;

                 // newMediaModel = SetHomeImage(newMediaModel);

                 DataStore.Instance.DS.MediaData.Add((MediaModel)newMediaModel);
             }
             else
             {
                 ErrorInfo t = new ErrorInfo("File not found when Region specified in ClipMedia")

                 {
                     { "Original ID", theMediaModel.Id },
                     { "Original File", theMediaModel.MediaStorageFilePath },
                     { "Clipped Id", argHLinkLoadImageModel.DeRef.Id }
                 };

                 DataStore.Instance.CN.NotifyError(t);
             }

             resourceBitmap.Dispose();

             return newMediaModel;
         }).ConfigureAwait(false);

            return returnMediaModel.HLink;
        }

        /// <summary>
        /// Sets the private object.
        /// </summary>
        /// <param name="thePriv">
        /// The priv.
        /// </param>
        /// <returns>
        /// True or False depending on if the object is private.
        /// </returns>
        public static Priv SetPrivateObject(string thePriv)
        {
            switch (thePriv)
            {
                case "1":
                    {
                        return new Priv(true);
                    }

                case "0":
                default:
                    {
                        return new Priv(false);
                    }
            }
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="a">
        /// a.
        /// </param>
        /// <returns>
        /// Text of the XML Attribute.
        /// </returns>
        private static string GetAttribute(XAttribute a)
        {
            if (a == null)
            {
                return string.Empty;
            }
            else
            {
                return ((string)a).Trim();
            }
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
            if (a == null)
            {
                return string.Empty;
            }
            else
            {
                return ((string)a).Trim();
            }
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
        /// Text of the XML element int he provided namespace.
        /// </returns>
        private static string GetElement(XElement a, string b)
        {
            if (a == null)
            {
                return string.Empty;
            }
            else
            {
                return GetElement(a.Element(ns + b));
            }
        }

        private static HLinkKey GetHLinkKey(XAttribute a)
        {
            if (a == null)
            {
                return new HLinkKey();
            }
            else
            {
                return new HLinkKey(((string)a).Trim());
            }
        }

        /// <summary>
        /// Converts a string into a uri (if it can).
        /// </summary>
        /// <param name="xmlData">
        /// string from XML.
        /// </param>
        private static Uri GetUri(string xmlData)
        {
            try
            {
                xmlData = xmlData.Trim();

                Uri uri = new UriBuilder(xmlData).Uri;

                return uri;
            }
            catch (UriFormatException ex)
            {
                ErrorInfo t = new ErrorInfo("The URI in the Internet address is not well formed")
                    {
                        { "Exception Message ", ex.Message },
                        { "xmlData", xmlData }
                    };

                DataStore.Instance.CN.NotifyError(t);

                return null;
            }
            catch (FormatException ex)
            {
                ErrorInfo t = new ErrorInfo("The URI in the Internet address is not in the correct format")
                    {
                        { "Exception Message ", ex.Message },
                        { "xmlData", xmlData }
                    };

                DataStore.Instance.CN.NotifyError(t);

                return null;
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("Exception in GetUri", ex);

                throw;
            }
        }

        private ModelBase GetBasics(XElement argElement)
        {
            ModelBase returnVal = new ModelBase
            {
                Id = GetAttribute(argElement.Attribute("id")),
                Change = GetDateTime(argElement, "change"),
                Priv = SetPrivateObject(GetAttribute(argElement.Attribute("priv"))),
                Handle = GetAttribute(argElement, "handle")
            };

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
            try
            {
                const string ColorNotSet = "#000000000000";

                var regexColorCode = new Regex("^#[a-fA-F0-9]{6}$");

                string hexColour = GetAttribute(a.Attribute(b));

                // Validate
                if ((!regexColorCode.IsMatch(hexColour.Trim()) && hexColour != ColorNotSet))
                {
                    ErrorInfo argErrorDetail = new ErrorInfo("Bad colour in GetColour")
                    {
                        { "Color element is", a.ToString() },
                        { "Attribute is", b }
                    };

                    DataStore.Instance.CN.NotifyError(argErrorDetail);

                    hexColour = "#000000";
                }

                if (hexColour == ColorNotSet)
                {
                    hexColour = "#000000";
                }

                ColorTypeConverter colConverter = new ColorTypeConverter();

                return (Color)(colConverter.ConvertFromInvariantString(hexColour));
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("Error in XML Utils GetColour", ex);
                throw;
            }
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private DateObjectModel GetDate(XElement xmlData)
        {
            return SetDate(xmlData);
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
                DataStore.Instance.CN.NotifyError(new ErrorInfo("The value passed to GetDateTime was not a valid number of Unix seconds"));
            };

            DateTimeOffset t = DateTimeOffset.FromUnixTimeSeconds(ls);

            // TODO This is in UTC and needs to be converted to local time

            return t.DateTime;
        }

        private TextStyle GetTextStyle(XElement a)
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

        /// <summary>
        /// Gets the h link.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private HLinkBase HLink(XElement xmlData)
        {
            HLinkBase t = new HLinkBase();

            if (xmlData != null)
            {
                t.HLinkKey = GetHLinkKey(xmlData.Attribute("hlink"));
            }

            return t;
        }
    }
}