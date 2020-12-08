namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    /// <summary>
    /// Private Storage Routines.
    /// </summary>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        public static SourceModel SetHomeImage(SourceModel argModel)
        {
            if (argModel is null)
            {
                throw new ArgumentNullException(nameof(argModel));
            }

            // Get default image if available
            HLinkHomeImageModel hlink = argModel.GMediaRefCollection.FirstHLinkHomeImage;

            // Set default
            if (hlink.Valid)
            {
                argModel.HomeImageHLink.HomeImageType = CommonEnums.HomeImageType.ThumbNail;
                argModel.HomeImageHLink.HLinkKey = hlink.HLinkKey;
            }

            return argModel;
        }

        /// <summary>
        /// load events from external storage.
        /// </summary>
        /// <returns>
        /// Flag of loaded successfully.
        /// </returns>
        public async Task LoadSourcesAsync()
        {
            await DataStore.Instance.CN.DataLogEntryAdd(argMessage: "Loading Source data").ConfigureAwait(false);
            {
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "source")
                        select el;

                    // TODO get BookMark fields

                    await DataStore.Instance.CN.DataLogEntryAdd(argMessage: "Loading Source entry").ConfigureAwait(false);

                    // Loop through results to get the Citation
                    foreach (XElement pSourceElement in de)
                    {
                        SourceModel loadSource = DV.SourceDV.NewModel();

                        // Citation attributes
                        loadSource.LoadBasics(GetBasics(pSourceElement));

                        if (loadSource.Id == "S0057")
                        {
                        }

                        loadSource.GSourceAttributeCollection = GetAttributeCollection(pSourceElement);

                        // Media refs
                        loadSource.GMediaRefCollection = await GetObjectCollection(pSourceElement).ConfigureAwait(false);

                        // Note refs
                        loadSource.GNoteRefCollection = GetNoteCollection(pSourceElement);

                        loadSource.GSAbbrev = GetElement(pSourceElement, "sabbrev");

                        loadSource.GSAuthor = GetElement(pSourceElement, "sauthor");

                        loadSource.GSPubInfo = GetElement(pSourceElement, "spubinfo");

                        loadSource.GSTitle = GetElement(pSourceElement, "stitle");

                        DataStore.Instance.CN.MinorMessage = "Loading Source entry: " + loadSource.GSTitle;

                        // Tag refs
                        loadSource.GTagRefCollection = GetTagCollection(pSourceElement);

                        // Repository refs
                        loadSource.GRepositoryRefCollection = GetRepositoryCollection(pSourceElement);

                        // set the Home image or symbol now that everything is laoded
                        loadSource = SetHomeImage(loadSource);

                        // save the event
                        DV.SourceDV.SourceData.Add(loadSource);
                    }

                    await DataStore.Instance.CN.DataLogEntryDelete().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    // TODO handle this
                    await DataStore.Instance.CN.DataLogEntryAdd(e.Message).ConfigureAwait(false);

                    throw;
                }
            }

            return;
        }
    }
}