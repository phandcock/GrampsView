namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
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
    public partial class StoreXML : IStoreXML
    {
        /// <summary>
        /// Load media objects from external storage.
        /// </summary>
        /// <returns>
        /// Flag showing of loaded successfully.
        /// </returns>
        public async Task<bool> LoadMediaObjectsAsync()
        {
            localGrampsCommonLogging.RoutineEntry("loadMediaObjects");

            await DataStore.Instance.CN.DataLogEntryAdd("Loading Media Objects").ConfigureAwait(false);
            {
                //// start file load
                //await DataStore.Instance.CN.DataLogEntryAdd("Loading Media File").ConfigureAwait(false);

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

                        if (loadObject.Id == "O0326")
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

                                    loadObject.OriginalFilePath = temp;

                                    // Load FileInfoEx and metadata
                                    loadObject.MediaStorageFile = await StoreFile.GetStorageFileAsync(loadObject.OriginalFilePath).ConfigureAwait(false);

                                    if (loadObject.MediaStorageFile.Valid)
                                    {
                                        var imageSize = DependencyService.Get<IImageResource>().GetSize(loadObject.MediaStorageFilePath);

                                        loadObject.MetaDataHeight = imageSize.Height;
                                        loadObject.MetaDataWidth = imageSize.Width;

                                        // TODO check File Content Type if ( loadObject.MediaStorageFile.FInfo.)
                                    }
                                    else
                                    {
                                        DataStore.Instance.CN.NotifyError(new ErrorInfo("Bad media file path") { { "Path", loadObject.OriginalFilePath } });
                                    }
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

                        // save the object
                        DataStore.Instance.DS.MediaData.Add((MediaModel)loadObject);

                        localGrampsCommonLogging.LogVariable("LoadMedia", loadObject.GDescription);
                    }
                }
                catch (Exception e)
                {
                    // TODO handle this
                    DataStore.Instance.CN.NotifyException("Loading Media Objects", e);

                    throw;
                }
            }

            await DataStore.Instance.CN.DataLogEntryReplace("Media load complete");

            localGrampsCommonLogging.RoutineExit(nameof(LoadMediaObjectsAsync));

            return true;
        }
    }
}