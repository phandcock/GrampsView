//-----------------------------------------------------------------------
//
// Storage routines for the GrampsStoreXML
//
// <copyright file="GrampsStoreXMLEvents.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

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
        public static EventModel SetHomeImage(EventModel argModel)
        {
            if (argModel is null)
            {
                throw new ArgumentNullException(nameof(argModel));
            }

            // Setup home images

            // Try media reference collection first
            HLinkHomeImageModel hlink = argModel.GMediaRefCollection.FirstHLinkHomeImage;

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
                argModel.HomeImageHLink.HomeImageType = CommonEnums.HomeImageType.ThumbNail;
                argModel.HomeImageHLink.HLinkKey = hlink.HLinkKey;
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
            await DataStore.CN.MajorStatusAdd("Loading Event data").ConfigureAwait(false);
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
                    await DataStore.CN.MajorStatusAdd(e.Message).ConfigureAwait(false);

                    DataStore.CN.NotifyException("LoadEventsAsync", e);

                    throw;
                }
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);
        }
    }
}