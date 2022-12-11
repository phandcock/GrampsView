using GrampsView.Data.DataView;
using GrampsView.Data.External.StoreXML;
using GrampsView.Models.DataModels;
using GrampsView.Models.HLinks.Models;

using System.Xml.Linq;

using static GrampsView.Common.CommonEnums;

namespace GrampsView.Data.ExternalStorage
{
    public partial class StoreXML : IStoreXML
    {
        public async Task LoadEventsAsync()
        {
            MyLog.DataLogEntryAdd("Loading Event data");
            {
                try
                {
                    // Run query
                    System.Collections.Generic.IEnumerable<XElement> de =
                        from el in LocalGrampsXMLdoc.Descendants(ns + "event")
                        select el;

                    // get event fields TODO

                    // Loop through results to get the Persons Uri _baseUri = new Uri("ms-appx:///");
                    foreach (XElement pname in de)
                    {
                        EventModel loadEvent = new();

                        // Event attributes
                        loadEvent.LoadBasics(GetBasics(pname));

                        if (loadEvent.Id == "E0001")
                        {
                        }

                        // Event fields
                        loadEvent.GAttribute = GetAttributeCollection(pname);

                        loadEvent.GCitationRefCollection = GetCitationCollection(pname);

                        loadEvent.GDate = SetDate(pname);

                        loadEvent.GDescription = GetElement(pname.Element(ns + "description"));

                        loadEvent.GMediaRefCollection = await GetObjectCollection(pname).ConfigureAwait(false);

                        loadEvent.GNoteRefCollection = GetNoteCollection(pname);

                        XElement tt = pname.Element(ns + "place");
                        if (tt is not null)
                        {
                            HLinkPlaceModel t = new()
                            {
                                HLinkKey = GetHLinkKey(tt)
                            };
                            loadEvent.GPlace = t;
                        }

                        loadEvent.GTagRefCollection = GetTagCollection(pname);

                        loadEvent.GType = GetElement(pname.Element(ns + "type"));

                        loadEvent.EventType = EventModelType.UNKNOWN;
                        if (Enum.TryParse(loadEvent.GType, out EventModelType loadEventType))
                        {
                            loadEvent.EventType = loadEventType;
                        }

                        // save the event
                        DV.EventDV.EventData.Add(loadEvent);
                    }
                }
                catch (Exception ex)
                {
                    // TODO handle this
                    MyLog.DataLogEntryAdd(ex.Message);

                    MyNotifications.NotifyException("LoadEventsAsync", ex);

                    throw;
                }
            }

            MyLog.DataLogEntryReplace("Event load complete");
            return;
        }
    }
}