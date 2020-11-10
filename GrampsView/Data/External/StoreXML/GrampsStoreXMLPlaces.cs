﻿namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

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
            await DataStore.Instance.CN.DataLogEntryAdd(nameof(LoadPlacesAsync)).ConfigureAwait(false);
            {
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

                            //if (String.IsNullOrEmpty(loadPlace.Handle))
                            //{
                            //}

                            //if (loadPlace.Id == "P0018")
                            //{
                            //}

                            loadPlace.GCode = GetElement(pPlaceElement, "code");

                            loadPlace.GType = (string)pPlaceElement.Attribute("type");

                            // Load other Place fields
                            loadPlace.GPTitle = GetElement(pPlaceElement, "ptitle");

                            XElement pName = pPlaceElement.Element(ns + "pname");

                            loadPlace.GLocation = GetPlaceLocationModelCollection(pPlaceElement);

                            loadPlace.GPlaceNames = GetPlaceNameModelCollection(pPlaceElement);

                            loadPlace.GCitationRefCollection.Clear();
                            loadPlace.GCitationRefCollection.AddRange(GetCitationCollection(pPlaceElement));

                            loadPlace.GMediaRefCollection = await GetObjectCollection(pPlaceElement).ConfigureAwait(false);

                            loadPlace.GNoteRefCollection = GetNoteCollection(pPlaceElement);

                            loadPlace.GPlaceRefCollection = GetPlaceRefCollection(pPlaceElement);

                            loadPlace.GTagRefCollection = GetTagCollection(pPlaceElement);

                            loadPlace.GURLCollection = GetURLCollection(pPlaceElement);

                            // save the event
                            DV.PlaceDV.PlaceData.Add(loadPlace);
                        }
                    }
                }
                catch (Exception e)
                {
                    DataStore.Instance.CN.NotifyException("Exception loading Place data from the file", e);

                    throw;
                }
            }

            return;
        }
    }
}