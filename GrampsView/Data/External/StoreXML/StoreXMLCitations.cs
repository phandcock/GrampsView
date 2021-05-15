namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public partial class StoreXML : IStoreXML
    {
        public async Task LoadCitationsAsync()
        {
            await DataStore.CN.DataLogEntryAdd("Loading Citation data").ConfigureAwait(false);
            {
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "citation")
                        select el;

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

                        loadCitation.GSourceRef.HLinkKey = GetHLinkKey(pcitation.Element(ns + "sourceref").Attribute("hlink"));

                        // await DataStore.CN.DataLogEntryReplace(String.Format("Loading Citation
                        // for: {0}", loadCitation.GSourceRef.DeRef.GetDefaultText));

                        loadCitation.GTagRef = GetTagCollection(pcitation);

                        // save the citation
                        DataStore.DS.CitationData.Add(loadCitation);
                    }
                }
                catch (Exception e)
                {
                    DataStore.CN.NotifyException("Exception loading Citations form XML", e);
                    throw;
                }

                await DataStore.CN.DataLogEntryReplace("Citation load complete");

                return;
            }
        }
    }
}