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
    /// Private Storage Routines.
    /// </summary>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        public static EventModel SetHomeImage(EventModel argModel)
        {
            if (argModel is null)
            {
                throw new ArgumentNullException(nameof(argModel));
            }

            // Setup home images

            // Try media reference collection first
            ItemGlyph hlink = argModel.GMediaRefCollection.FirstHLinkHomeImage;

            // Check Media for Images
            if (!hlink.Valid)
            {
                hlink = argModel.GMediaRefCollection.FirstHLinkHomeImage;
            }

            // Check Citation for Images
            if (!hlink.Valid)
            {
                hlink = argModel.GCitationRefCollection.FirstHLinkHomeImage;

                //hlink = DV.CitationDV.GetFirstImageFromCollection(argModel.GCitationRefCollection);
            }

            // Handle the link if we can
            if (hlink.Valid)
            {
                argModel.ModelItemGlyph = hlink;
            }

            return argModel;
        }

        /// <summary>
        /// load events from external storage.
        /// </summary>
        /// <param name="eventRepository">
        /// The event repository.
        /// </param>
        /// <returns>
        /// Flag of loaded successfully.
        /// </returns>
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
                        EventModel loadEvent = DV.EventDV.NewModel();

                        // Event attributes
                        loadEvent.LoadBasics(GetBasics(pname));
                        //loadEvent.Id = GetAttribute(pname.Attribute("id"));
                        //loadEvent.Change = GetDateTime(GetAttribute(pname, "change"));
                        //loadEvent.Priv = SetPrivateObject(GetAttribute(pname.Attribute("priv")));
                        //loadEvent.Handle = GetAttribute(pname.Attribute("handle"));

                        if (loadEvent.Id == "E0715")
                        {
                        }

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

                        // set the Home image or symbol now that everythign is laoded
                        loadEvent = SetHomeImage(loadEvent);

                        // save the event
                        DV.EventDV.EventData.Add(loadEvent);
                    }

                    // sort the collection eventRepository.Items.Sort(EventModel => EventModel);
                }
                catch (Exception e)
                {
                    // TODO handle this
                    await DataStore.Instance.CN.DataLogEntryAdd(e.Message).ConfigureAwait(false);

                    DataStore.Instance.CN.NotifyException("LoadEventsAsync", e);

                    throw;
                }
            }
        }
    }
}