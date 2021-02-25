namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Xamarin.Forms;

    /// <summary>
    /// Private Storage Routines.
    /// </summary>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
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
            localGrampsCommonLogging.RoutineEntry("loadMediaObjects");

            await DataStore.Instance.CN.DataLogEntryAdd("Loading Media Objects").ConfigureAwait(false);
            {
                // start file load
                await DataStore.Instance.CN.DataLogEntryAdd("Loading Media File").ConfigureAwait(false);

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

                        IMediaModel loadObject = new MediaModel();
                        loadObject.LoadBasics(GetBasics(pname));

                        if (loadObject.Id == "O0521")
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
                                DataStore.Instance.CN.NotifyError(new ErrorInfo("Error trying to load a media file for object listed in the GRAMPS file.  FileName is null") { { "Id", loadObject.Id }, });

                                loadObject.MediaStorageFile = null;
                            }
                            else
                            {
                                try
                                {
                                    string temp = StoreFileUtility.CleanFilePath(mediaFileName);

                                    await DataStore.Instance.CN.MinorMessageAdd(string.Format("Loading media file: {0}", temp));

                                    loadObject.OriginalFilePath = temp;

                                    // Load FileInfoEx and metadata
                                    loadObject.MediaStorageFile = await StoreFile.GetStorageFileAsync(loadObject.OriginalFilePath).ConfigureAwait(false);

                                    var imageSize = DependencyService.Get<IImageResource>().GetSize(loadObject.MediaStorageFilePath);

                                    loadObject.MetaDataHeight = imageSize.Height;
                                    loadObject.MetaDataWidth = imageSize.Width;
                                }
                                catch (Exception ex)
                                {
                                    DataStore.Instance.CN.NotifyException("Error trying to load a media file (" + loadObject.OriginalFilePath + ") listed in the GRAMPS file", ex);
                                    throw;
                                }
                            }
                        }

                        // Get attributes

                        // Get description
                        loadObject.GDescription = (string)filedetails.Attribute("description");

                        // date details
                        XElement dateval = pname.Element(ns + "dateval");
                        if (dateval != null)
                        {
                            loadObject.GDateValue = SetDate(pname);
                        }

                        // Load NoteRefs
                        loadObject.GNoteRefCollection.Clear();
                        loadObject.GNoteRefCollection.AddRange(GetNoteCollection(pname));

                        // citationref details TODO Event References
                        loadObject.GCitationRefCollection.Clear();
                        loadObject.GCitationRefCollection.AddRange(GetCitationCollection(pname));

                        foreach (HLinkTagModel item in GetTagCollection(pname))
                        {
                            loadObject.GTagRefCollection.Add(item);
                        }

                        // loadObject = SetHomeImage(loadObject);

                        // save the object
                        DataStore.Instance.DS.MediaData.Add((MediaModel)loadObject);

                        localGrampsCommonLogging.LogVariable("LoadMedia", loadObject.GDescription);
                    }

                    await DataStore.Instance.CN.DataLogEntryDelete().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    // TODO handle this
                    DataStore.Instance.CN.NotifyException("Loading Media Objects", e);

                    throw;
                }
            }

            localGrampsCommonLogging.RoutineExit(nameof(LoadMediaObjectsAsync));
            return true;
        }
    }
}