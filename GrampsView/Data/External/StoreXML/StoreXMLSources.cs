namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

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
            await _iocCommonNotifications.DataLogEntryAdd(argMessage: "Loading Source data").ConfigureAwait(false);
            {
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "source")
                        select el;

                    // Loop through results to get the Source
                    foreach (XElement pSourceElement in de)
                    {
                        SourceModel loadSource = new SourceModel();

                        // Source attributes
                        loadSource.LoadBasics(GetBasics(pSourceElement));

                        if (loadSource.Id == "S0008")
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
                    }
                }
                catch (Exception e)
                {
                    // TODO handle this
                    await _iocCommonNotifications.DataLogEntryAdd(e.Message).ConfigureAwait(false);

                    throw;
                }
            }

            await _iocCommonNotifications.DataLogEntryReplace("Source load complete");

            return;
        }
    }
}