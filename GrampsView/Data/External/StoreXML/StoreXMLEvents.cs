﻿namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using static GrampsView.Common.CommonEnums;

    public partial class StoreXML : IStoreXML
    {
        public async Task LoadEventsAsync()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Loading Event data").ConfigureAwait(false);
            {
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "event")
                        select el;

                    // get event fields TODO

                    // Loop through results to get the Persons Uri _baseUri = new Uri("ms-appx:///");
                    foreach (XElement pname in de)
                    {
                        EventModel loadEvent = new EventModel();

                        // Event attributes
                        loadEvent.LoadBasics(GetBasics(pname));

                        //if (loadEvent.Id == "E0715")
                        //{
                        //}

                        // Event fields
                        loadEvent.GAttribute = GetAttributeCollection(pname);

                        loadEvent.GCitationRefCollection = GetCitationCollection(pname);

                        loadEvent.GDate = SetDate(pname);

                        loadEvent.GDescription = GetElement(pname.Element(ns + "description"));

                        loadEvent.GMediaRefCollection = await GetObjectCollection(pname).ConfigureAwait(false);

                        loadEvent.GNoteRefCollection = GetNoteCollection(pname);

                        loadEvent.GPlace.SetBase(HLink(pname.Element(ns + "place")));

                        loadEvent.GTagRefCollection = GetTagCollection(pname);

                        loadEvent.GType = GetElement(pname.Element(ns + "type"));

                        Enum.TryParse(loadEvent.GType, out EventModelType loadEventType);
                        loadEvent.EventType = loadEventType;

                        // save the event
                        DV.EventDV.EventData.Add(loadEvent);
                    }
                }
                catch (Exception e)
                {
                    // TODO handle this
                    await DataStore.Instance.CN.DataLogEntryAdd(e.Message).ConfigureAwait(false);

                    DataStore.Instance.CN.NotifyException("LoadEventsAsync", e);

                    throw;
                }
            }

            await DataStore.Instance.CN.DataLogEntryReplace("Event load complete");
            return;
        }
    }
}