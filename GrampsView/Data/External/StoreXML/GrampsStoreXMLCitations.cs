namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    /// <summary>
    /// Load Citations from external storage routines.
    /// </summary>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        /// <summary>
        /// Load Citations from external storage.
        /// </summary>
        /// <returns>
        /// Flag if loaded successfully.
        /// </returns>
        public async Task LoadCitationsAsync()
        {
            try
            {
                // Run query
                var de =
                    from el in localGrampsXMLdoc.Descendants(ns + "citation")
                    select el;

                await DataStore.Instance.CN.DataLogEntryAdd("Loading Citation Entries").ConfigureAwait(false);

                // Loop through results to get the Citation
                foreach (XElement pcitation in de)
                {
                    CitationModel loadCitation = DV.CitationDV.NewModel();

                    // Citation attributes
                    loadCitation.LoadBasics(GetBasics(pcitation));

                    if (loadCitation.Id == "C0656")
                    {
                    }

                    // Citation fields
                    loadCitation.GDateContent = GetDate(pcitation);

                    loadCitation.GPage = GetElement(pcitation.Element(ns + "page"));

                    loadCitation.GConfidence = GetElement(pcitation.Element(ns + "confidence"));

                    loadCitation.GNoteRefCollection = GetNoteCollection(pcitation);

                    // ObjectRef loading
                    loadCitation.GMediaRefCollection = await GetObjectCollection(pcitation).ConfigureAwait(false);

                    loadCitation.GSourceAttributeCollection = GetSrcAttributeCollection(pcitation);

                    loadCitation.GSourceRef.HLinkKey = GetAttribute(pcitation.Element(ns + "sourceref"), "hlink");

                    // await DataStore.Instance.CN.DataLogEntryReplace(String.Format("Loading
                    // Citation for: {0}", loadCitation.GSourceRef.DeRef.GetDefaultText));

                    loadCitation.GTagRef = GetTagCollection(pcitation);

                    // save the citation
                    DataStore.Instance.DS.CitationData.Add(loadCitation);
                }

                await DataStore.Instance.CN.DataLogEntryDelete().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                DataStore.Instance.CN.NotifyException("Exception loading Citations form XML", e);
                throw;
            }

            await DataStore.Instance.CN.DataLogEntryAdd("Citation loading complete").ConfigureAwait(false);

            return;
        }
    }
}