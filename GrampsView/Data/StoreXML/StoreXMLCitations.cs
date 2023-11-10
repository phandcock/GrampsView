// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.DataView;
using GrampsView.Data.StoreDB;
using GrampsView.Data.StoreXML;
using GrampsView.DBModels;

using System.Diagnostics;
using System.Xml.Linq;

namespace GrampsView.Data.ExternalStorage
{
    public partial class StoreXML : IStoreXML
    {
        public async Task LoadCitationsAsync()
        {
            MyLog.DataLogEntryAdd("Loading Citation data");
            {
                try
                {
                    // Run query
                    IEnumerable<XElement> de =
                        from el in LocalGrampsXMLdoc.Descendants(ns + "citation")
                        select el;

                    // Loop through results to get the Citation
                    foreach (XElement pcitation in de)
                    {
                        CitationDBModel loadCitation = new();

                        // Citation attributes
                        loadCitation.LoadBasics(GetDBBasics(pcitation));

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
                        CitationDBModel t = new CitationDBModel(loadCitation);
                        Debug.WriteLine(t.HLinkKeyValue);
                        DL.CitationDL.CitationAccess.Add(t);
                    }
                }
                catch (Exception ex)
                {
                    MyNotifications.NotifyException("Exception loading Citations form XML", ex);
                }

                Ioc.Default.GetRequiredService<IStoreDB>().SaveChanges();

                MyLog.DataLogEntryReplace("Citation load complete");

                return;
            }
        }
    }
}