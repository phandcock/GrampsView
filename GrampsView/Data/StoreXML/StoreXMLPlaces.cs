// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.DataView;
using GrampsView.Data.StoreXML;
using GrampsView.Models.DataModels;

using System.Xml.Linq;

namespace GrampsView.Data.ExternalStorage
{
    public partial class StoreXML : IStoreXML
    {
        public async Task LoadPlacesAsync()
        {
            MyLog.DataLogEntryAdd("Loading Place data");
            {
                try
                {
                    // Run query
                    System.Collections.Generic.IEnumerable<XElement> de =
                        from el in LocalGrampsXMLdoc.Descendants(ns + "placeobj")
                        select el;

                    // action any data found
                    if (!(de == null))
                    {
                        // get Place fields

                        // Loop through results to get the Place data;
                        foreach (XElement pPlaceElement in de)
                        {
                            PlaceModel loadPlace = new();

                            // Place attributes
                            loadPlace.LoadBasics(GetBasics(pPlaceElement));

                            //if (String.IsNullOrEmpty(loadPlace.Handle))
                            //{
                            //}

                            if (loadPlace.Id == "P0065")
                            {
                            }

                            // Load other Place fields
                            loadPlace.GType = (string)pPlaceElement.Attribute("type");

                            XElement gpNameElement = pPlaceElement.Element(ns + "pname");
                            loadPlace.GPName = GetAttribute(gpNameElement, "value");

                            loadPlace.GPlaceNames = GetPlaceNameModelCollection(pPlaceElement);

                            if (string.IsNullOrEmpty(loadPlace.GPlaceNames[0].DeRef.GValue))
                            {
                            }

                            loadPlace.GCode = GetElement(pPlaceElement, "code");

                            XElement coord = pPlaceElement.Element(ns + "coord");
                            if (coord is not null)
                            {
                                _ = double.TryParse(GetAttribute(coord, "lat"), out double latDouble);
                                _ = double.TryParse(GetAttribute(coord, "long"), out double longDouble);

                                loadPlace.GCoordLat = latDouble;
                                loadPlace.GCoordLong = longDouble;
                            }

                            loadPlace.GPlaceParentCollection = GetPlaceRefCollection(pPlaceElement);
                            loadPlace.GPlaceParentCollection.Title = "Enclosing Places";

                            loadPlace.GLocation = GetPlaceLocationModelCollection(pPlaceElement);

                            loadPlace.GMediaRefCollection = await GetObjectCollection(pPlaceElement).ConfigureAwait(false);

                            loadPlace.GURLCollection = GetURLCollection(pPlaceElement);

                            loadPlace.GNoteRefCollection = GetNoteCollection(pPlaceElement);

                            loadPlace.GCitationRefCollection.Clear();
                            loadPlace.GCitationRefCollection.AddRange(GetCitationCollection(pPlaceElement));

                            loadPlace.GTagRefCollection = GetTagCollection(pPlaceElement);

                            // save the event
                            DV.PlaceDV.PlaceData.Add(loadPlace);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MyNotifications.NotifyException("Exception loading Place data from the file", ex);
                }
            }

            MyLog.DataLogEntryReplace("Place load complete");

            return;
        }
    }
}