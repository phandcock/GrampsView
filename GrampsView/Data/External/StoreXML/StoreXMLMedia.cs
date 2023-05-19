// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.External.StoreFile;
using GrampsView.Data.External.StoreXML;
using GrampsView.Data.Model;
using GrampsView.Data.Repository;
using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Interfaces;

using SharedSharp.Errors;

using SkiaSharp;

using System.Xml.Linq;

//using ImageExtensions = SharedSharp.Common.SharedSharpImageExtensions;

namespace GrampsView.Data.ExternalStorage
{
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
            MyLog.RoutineEntry("loadMediaObjects");

            MyLog.DataLogEntryAdd("Loading Media Objects");
            {
                //// start file load
                //await _iocCommonNotifications.DataLogEntryAdd("Loading Media File").ConfigureAwait(false);

                //   ImageExtensions PlatformImageHandler = new SharedSharp.Common.SharedSharpImageExtensions();

                // Load notes Run query
                IEnumerable<XElement> de =
                    from el in LocalGrampsXMLdoc.Descendants(ns + "object")
                    select el;

                try
                {
                    foreach (XElement pname in de)
                    {
                        MyLog.Progress($"Loading {pname.Value.Trim()}");

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

                        if (loadObject.Id == "O0631")
                        {
                        }

                        // file details
                        XElement filedetails = pname.Element(ns + "file");
                        if (filedetails != null)
                        {
                            // load filename
                            string mediaFileName = (string)filedetails.Attribute("src");

                            if (mediaFileName.Length == 0)
                            {
                                MyNotifications.NotifyError(new ErrorInfo("Error trying to load a media file for object listed in the GRAMPS file.  FileName is null") { { "Id", loadObject.Id }, });

                                loadObject.CurrentStorageFile = null;
                            }
                            else
                            {
                                try
                                {
                                    string temp = StoreFileUtility.CleanFilePath(mediaFileName);

                                    loadObject.OriginalFilePath = temp;

                                    // Load FileInfoEx and metadata
                                    loadObject.CurrentStorageFile = new FileInfoEx(loadObject.OriginalFilePath);

                                    if (loadObject.CurrentStorageFile.Valid)
                                    {
                                        // TODO add this back in
                                        //Size imageSize = Task.Run(async () => await PlatformImageHandler.GetSize(loadObject.CurrentStorageFile.GetAbsoluteFilePath)).Result;

                                        using Stream st = new FileStream(loadObject.CurrentStorageFile.GetAbsoluteFilePath, FileMode.Open);
                                        if (st is null)
                                        {
                                            loadObject.MetaDataHeight = 100;
                                            loadObject.MetaDataWidth = 100;
                                            continue;
                                        }

                                        SKBitmap b = SKBitmap.Decode(st);
                                        if (b is null)
                                        {
                                            loadObject.MetaDataHeight = 100;
                                            loadObject.MetaDataWidth = 100;
                                            continue;
                                        }

                                        loadObject.MetaDataHeight = b.Height;
                                        loadObject.MetaDataWidth = b.Width;

                                        // TODO check File Content Type if ( loadObject.MediaStorageFile.FInfo.)
                                    }
                                    else
                                    {
                                        MyNotifications.NotifyError(new ErrorInfo("Bad media file path") { { "Path", loadObject.OriginalFilePath } });
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MyNotifications.NotifyException("Error trying to load a media file (" + loadObject.OriginalFilePath + ") listed in the GRAMPS file", ex);
                                }
                            }

                            // Load mime types
                            loadObject.FileContentType = (string)filedetails.Attribute("mime");

                            if (loadObject.FileMimeType == "unknown")
                            {
                                loadObject.FileContentType = CommonRoutines.MimeFileContentTypeGet(Path.GetExtension(loadObject.OriginalFilePath));
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

                        MyLog.Progress("Media Loaded", loadObject.GDescription);
                    }
                }
                catch (Exception ex)
                {

                    MyNotifications.NotifyException("Loading Media Objects", ex);
                }
            }

            MyLog.DataLogEntryReplace("Media load complete");

            MyLog.RoutineExit(nameof(LoadMediaObjectsAsync));

            return true;
        }
    }
}