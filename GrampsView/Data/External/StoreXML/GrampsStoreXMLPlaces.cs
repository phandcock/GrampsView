//-----------------------------------------------------------------------
//
// Storage routines for the GrampsStoreXML
//
// <copyright file="GrampsStoreXMLPlaces.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

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
        /// <summary>
        /// load events from external storage.
        /// </summary>
        /// <param name="eventRepository">
        /// The event repository.
        /// </param>
        /// <returns>
        /// Flag of loaded successfully.
        /// </returns>
        public async Task LoadPlacesAsync()
        {
            await DataStore.CN.MajorStatusAdd(nameof(LoadPlacesAsync)).ConfigureAwait(false);
            {
                // XNamespace ns = grampsXMLNameSpace;
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "placeobj")
                        select el;

                    // action any data found
                    if (!(de == null))
                    {
                        // get Place fields

                        // Loop through results to get the Place data;
                        foreach (XElement pPlaceElement in de)
                        {
                            PlaceModel loadPlace = DV.PlaceDV.NewModel();

                            // Place attributes
                            loadPlace.LoadBasics(GetBasics(pPlaceElement));
                            //loadPlace.Id = (string)pPlaceElement.Attribute("id");

                            //loadPlace.Change = GetDateTime(pPlaceElement, "change");
                            //loadPlace.Priv = SetPrivateObject((string)pPlaceElement.Attribute("priv"));
                            //loadPlace.Handle = (string)pPlaceElement.Attribute("handle");

                            if (String.IsNullOrEmpty(loadPlace.Handle))
                            {
                            }

                            if (loadPlace.Id == "P0018")
                            {
                            }

                            loadPlace.GCode = GetElement(pPlaceElement, "code");

                            loadPlace.GType = (string)pPlaceElement.Attribute("type");

                            // Load other Place fields
                            loadPlace.GPTitle = GetElement(pPlaceElement, "ptitle");

                            XElement pName = pPlaceElement.Element(ns + "pname");
                            loadPlace.GName = (string)pName.Attribute("value");

                            loadPlace.GCitationRefCollection = GetCitationCollection(pPlaceElement);

                            loadPlace.GMediaRefCollection = await GetObjectCollection(pPlaceElement).ConfigureAwait(false);

                            loadPlace.GNoteRefCollection = GetNoteCollection(pPlaceElement);

                            loadPlace.GPlaceRefCollection = GetPlaceRefCollection(pPlaceElement);

                            loadPlace.GTagRefCollection = GetTagCollection(pPlaceElement);

                            loadPlace.GURLCollection = GetURLCollection(pPlaceElement);

                            // save the event
                            DV.PlaceDV.PlaceData.Add(loadPlace);
                        }

                        // sort the collection eventRepository.Items.Sort(EventModel => EventModel);

                        // let everybody know
                    }
                }
                catch (Exception e)
                {
                    // TODO handle this
                    await DataStore.CN.MajorStatusAdd(e.Message).ConfigureAwait(false);

                    throw;
                }
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);
            return;
        }
    }
}