namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Xamarin.Forms;

    /// <summary>
    /// Private Storage Routines.
    /// </summary>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        public static IMediaModel SetHomeImage(IMediaModel argModel)
        {
            Contract.Requires(argModel != null);

            // Setup HomeImage
            argModel.HomeImageHLink.HLinkKey = argModel.HLinkKey;

            switch (argModel.FileMimeType)
            {
                case "application":
                    {
                        argModel.HomeImageHLink.HomeImageType = CommonEnums.HomeImageType.Symbol;

                        switch (argModel.FileMimeSubType)
                        {
                            case "pdf":
                                {
                                    argModel.HomeImageHLink.HomeSymbol = CommonFontNamesFAS.FilePdf;
                                    break;
                                }

                            case "x-zip-compressed":
                                {
                                    argModel.HomeImageHLink.HomeSymbol = CommonFontNamesFAS.FileArchive;
                                    break;
                                }

                            case "zip":
                                {
                                    argModel.HomeImageHLink.HomeSymbol = CommonFontNamesFAS.FileArchive;
                                    break;
                                }
                        }

                        break;
                    }

                case "audio":
                    {
                        argModel.HomeImageHLink.HomeImageType = CommonEnums.HomeImageType.Symbol;
                        argModel.HomeImageHLink.HomeSymbol = CommonFontNamesFAS.FileAudio;
                        break;
                    }

                case "image":
                    {
                        argModel.HomeImageHLink.HomeImageType = CommonEnums.HomeImageType.ThumbNail;
                        argModel.HomeImageHLink.HomeSymbol = CommonFontNamesFAS.FileImage;
                        break;
                    }

                case "video":
                    {
                        argModel.HomeImageHLink.HomeImageType = CommonEnums.HomeImageType.Symbol;
                        argModel.HomeImageHLink.HomeSymbol = CommonFontNamesFAS.FileVideo;
                        break;
                    }
            }

            return argModel;
        }

        /// <summary>
        /// load media objects from external storage.
        /// </summary>
        /// <param name="mediaRepository">
        /// The media repository.
        /// </param>
        /// <returns>
        /// Flag showing of loaded successfully.
        /// </returns>
        public async Task<bool> LoadMediaObjectsAsync()
        {
            localGrampsCommonLogging.LogRoutineEntry("loadMediaObjects");

            await DataStore.CN.DataLogEntryAdd("Loading Media Objects").ConfigureAwait(false);
            {
                // start file load
                await DataStore.CN.DataLogEntryAdd("Loading Media File").ConfigureAwait(false);

                //// Get colour
                //Application.Current.Resources.TryGetValue("CardBackGroundMedia", out var varCardColour);
                //Color cardColour = (Color)varCardColour;

                // Load notes Run query
                var de =
                    from el in localGrampsXMLdoc.Descendants(ns + "object")
                    select el;

                try
                {
                    foreach (XElement pname in de)
                    {
                        // <code> < define name = "object-content" > <ref name=
                        // "SecondaryColor-object" /> < element name = "file" > < attribute name =
                        // "src" > < text /> </ attribute > < attribute name = "mime" >

                        // < text />

                        // </ attribute >

                        // < optional >

                        // < attribute name = "checksum" >

                        // < text />

                        // </ attribute >

                        // </ optional > </code>

                        // < optional >

                        // < attribute name = "description" >

                        // < text />

                        // </ attribute >

                        // </ optional >

                        // </ element >

                        // < zeroOrMore >

                        // < element name = "attribute" >

                        // <ref name="attribute-content"/>

                        // </ element >

                        // </ zeroOrMore >

                        // < zeroOrMore >

                        // < element name = "noteref" >

                        // <ref name="noteref-content"/>

                        // </ element >

                        // </ zeroOrMore >

                        // < optional >

                        // <ref name="date-content"/>

                        // </ optional >

                        // < zeroOrMore >

                        // < element name = "citationref" >

                        // <ref name="citationref-content"/>

                        // </ element >

                        // </ zeroOrMore >

                        // </ element >

                        // </ zeroOrMore >

                        // </ define >
                        IMediaModel loadObject = new MediaModel();
                        loadObject.LoadBasics(GetBasics(pname));

                        //IMediaModel loadObject = new MediaModel
                        //{
                        //    // object details
                        //    Id = (string)pname.Attribute("id"),
                        //    Handle = (string)pname.Attribute("handle"),
                        //    Priv = SetPrivateObject((string)pname.Attribute("priv")),
                        //    Change = GetDateTime(GetAttribute(pname, "change")),
                        //};

                        if (loadObject.Id == "O0531")
                        {
                        }

                        // file details
                        XElement filedetails = pname.Element(ns + "file");
                        if (filedetails != null)
                        {
                            loadObject.FileContentType = (string)filedetails.Attribute("mime");

                            string mediaFileName = (string)filedetails.Attribute("src");

                            if (mediaFileName.Length == 0)
                            {
                                DataStore.CN.NotifyError("Error trying to load a media file for object (" + loadObject.Id + ") listed in the GRAMPS file.  FileName is null");
                                loadObject.MediaStorageFile = null;
                            }
                            else
                            {
                                try
                                {
                                    string temp = StoreFileUtility.CleanFilePath(mediaFileName);

                                    await DataStore.CN.DataLogEntryReplace("Loading media file: " + temp).ConfigureAwait(false);

                                    loadObject.OriginalFilePath = temp;

                                    // Load FileInfoEx and metadata
                                    loadObject.MediaStorageFile = await StoreFile.GetStorageFileAsync(loadObject.OriginalFilePath).ConfigureAwait(false);

                                    var imageSize = DependencyService.Get<IImageResource>().GetSize(loadObject.MediaStorageFilePath);

                                    loadObject.MetaDataHeight = imageSize.Height;
                                    loadObject.MetaDataWidth = imageSize.Width;
                                }
                                catch (Exception ex)
                                {
                                    DataStore.CN.NotifyException("Error trying to load a media file (" + loadObject.OriginalFilePath + ") listed in the GRAMPS file", ex);
                                    throw;
                                }
                            }
                        }
                        // Get description
                        loadObject.GDescription = (string)filedetails.Attribute("description");

                        // date details
                        XElement dateval = pname.Element(ns + "dateval");
                        if (dateval != null)
                        {
                            loadObject.GDateValue = SetDate(pname);
                        }

                        // Load NoteRefs

                        // citationref details TODO Event References
                        loadObject.GCitationRefCollection.Clear();
                        loadObject.GCitationRefCollection.AddRange(GetCitationCollection(pname));

                        foreach (HLinkTagModel item in GetTagCollection(pname))
                        {
                            loadObject.GTagRefCollection.Add(item);
                        }

                        loadObject = SetHomeImage(loadObject);

                        // save the object
                        DataStore.DS.MediaData.Add((MediaModel)loadObject);

                        localGrampsCommonLogging.LogVariable("LoadMedia", loadObject.GDescription);
                    }

                    await DataStore.CN.DataLogEntryDelete().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    // TODO handle this
                    DataStore.CN.NotifyException("Loading Media Objects", e);

                    throw;
                }
            }

            localGrampsCommonLogging.LogRoutineExit(nameof(LoadMediaObjectsAsync));
            return true;
        }
    }
}