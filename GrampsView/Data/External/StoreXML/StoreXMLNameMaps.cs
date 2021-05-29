﻿namespace GrampsView.Data.ExternalStorage
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
        public async Task LoadNameMapsAsync()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Loading NameMap data").ConfigureAwait(false);

            {
                // XNamespace ns = grampsXMLNameSpace;
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "namemaps")
                        select el;

                    // get Citation fields

                    // Loop through results to get the Citation Uri _baseUri = new Uri("ms-appx:///");
                    foreach (XElement pcitation in de)
                    {
                        NameMapModel loadNameMap = new NameMapModel();

                        // Citation attributes
                        loadNameMap.LoadBasics(GetBasics(pcitation));
                        //loadNameMap.Id = (string)pcitation.Attribute("id");
                        //loadNameMap.Change = GetDateTime(pcitation, "change");
                        //loadNameMap.Priv = SetPrivateObject((string)pcitation.Attribute("priv"));
                        //loadNameMap.Handle = (string)pcitation.Attribute("handle");

                        // Citation fields

                        // Don't sort here as the objects pointed to may not have been loaded. Sort
                        // in Post Load cleanup

                        // save the event
                        DV.NameMapDV.NameMapData.Add(loadNameMap);
                    }

                    // sort the collection eventRepository.Items.Sort(EventModel => EventModel);

                    // let everybody know
                }
                catch (Exception e)
                {
                    // TODO handle this
                    await DataStore.Instance.CN.DataLogEntryAdd(e.Message).ConfigureAwait(false);

                    throw;
                }
            }

            await DataStore.Instance.CN.DataLogEntryReplace("NameMap load complete");

            return;
        }
    }
}