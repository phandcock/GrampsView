using GrampsView.Data.Repository;
using GrampsView.Models.DataModels;

using System.Xml.Linq;

namespace GrampsView.Data.ExternalStorage
{
    public partial class StoreXML : IStoreXML
    {
        [Obsolete]
        public async Task LoadCitationsAsync()
        {
            myCommonLogging.DataLogEntryAdd("Loading Citation data");
            {
                try
                {
                    // Run query
                    IEnumerable<XElement> de =
                        from el in localGrampsXMLdoc.Descendants(ns + "citation")
                        select el;

                    // Loop through results to get the Citation
                    foreach (XElement pcitation in de)
                    {
                        CitationModel loadCitation = new();

                        // Citation attributes
                        loadCitation.LoadBasics(GetBasics(pcitation));

                        if (loadCitation.Id == "C0144")
                        {
                        }

                        // Citation fields
                        loadCitation.GDateContent = GetDate(pcitation);

                        loadCitation.GPage = GetElement(pcitation.Element(ns + "page"));

                        loadCitation.GConfidence = GetDataConfidence(pcitation);

                        loadCitation.GNoteRefCollection = GetNoteCollection(pcitation);

                        // ObjectRef loading
                        loadCitation.GMediaRefCollection = await GetObjectCollection(pcitation).ConfigureAwait(false);

                        loadCitation.GSourceAttributeCollection = GetSrcAttributeCollection(pcitation);

                        loadCitation.GSourceRef.HLinkKey = GetHLinkKey(pcitation.Element(ns + "sourceref"));

                        loadCitation.GTagRef = GetTagCollection(pcitation);

                        // Save the citation
                        DataStore.Instance.DS.CitationData.Add(loadCitation);
                    }
                }
                catch (Exception ex)
                {
                    myCommonNotifications.NotifyException("Exception loading Citations form XML", ex, null);
                    throw;
                }

                myCommonLogging.DataLogEntryReplace("Citation load complete");

                return;
            }
        }
    }
}