﻿namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public partial class StoreXML : IStoreXML
    {
        public async Task LoadCitationsAsync()
        {
            _iocCommonNotifications.DataLogEntryAdd("Loading Citation data");
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
                        CitationModel loadCitation = new CitationModel();

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
                catch (Exception e)
                {
                    _iocCommonNotifications.NotifyException("Exception loading Citations form XML", e);
                    throw;
                }

                _iocCommonNotifications.DataLogEntryReplace("Citation load complete");

                return;
            }
        }
    }
}