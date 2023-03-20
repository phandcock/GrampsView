// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.External.StoreXML;
using GrampsView.Data.Model;
using GrampsView.Data.Repository;

using System.Diagnostics;
using System.Xml.Linq;

namespace GrampsView.Data.ExternalStorage
{
    /// - XML 1.71 Completed
    /// <summary>
    /// Source Storage Routines.
    /// </summary>
    public partial class StoreXML : IStoreXML
    {
        /// <summary>
        /// load events from external storage.
        /// </summary>
        /// <returns>
        /// Flag if loaded successfully.
        /// </returns>
        public async Task LoadSourcesAsync()
        {
            MyLog.DataLogEntryAdd(argMessage: "Loading Source data");
            {
                try
                {
                    // Run query
                    IEnumerable<XElement> de =
                        from el in LocalGrampsXMLdoc.Descendants(ns + "source")
                        select el;

                    // Loop through results to get the Source
                    foreach (XElement pSourceElement in de)
                    {
                        SourceModel loadSource = new SourceModel();

                        // Source attributes
                        loadSource.LoadBasics(GetBasics(pSourceElement));

                        if (loadSource.Id == "S0087")
                        {
                        }

                        //Debug.WriteLine(loadSource.Id);

                        // Media refs
                        loadSource.GMediaRefCollection = await GetObjectCollection(pSourceElement).ConfigureAwait(false);

                        // Note refs
                        loadSource.GNoteRefCollection = GetNoteCollection(pSourceElement);

                        // Repository refs
                        loadSource.GRepositoryRefCollection = GetRepositoryCollection(pSourceElement);

                        loadSource.GSAbbrev = GetElement(pSourceElement, "sabbrev");

                        loadSource.GSAuthor = GetElement(pSourceElement, "sauthor");

                        loadSource.GSourceAttributeCollection = GetAttributeCollection(pSourceElement);

                        loadSource.GSPubInfo = GetElement(pSourceElement, "spubinfo");

                        loadSource.GSTitle = GetElement(pSourceElement, "stitle");

                        // Tag refs
                        loadSource.GTagRefCollection = GetTagCollection(pSourceElement);

                        // save the event
                        DataStore.Instance.DS.SourceData.Add(loadSource);
                        Debug.WriteLine(loadSource.Id);
                    }
                }
                catch (Exception ex)
                {
                    MyNotifications.NotifyException("Load Source", ex);
                }
            }

            MyLog.DataLogEntryReplace("Source load complete");

            return;
        }
    }
}