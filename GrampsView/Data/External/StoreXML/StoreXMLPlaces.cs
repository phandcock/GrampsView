namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public partial class StoreXML : IStoreXML
    {
        public async Task LoadPlacesAsync()
        {
            _iocCommonNotifications.DataLogEntryAdd("Loading Place data");
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
                            PlaceModel loadPlace = new PlaceModel();

                            // Place attributes
                            loadPlace.LoadBasics(GetBasics(pPlaceElement));

                            //if (String.IsNullOrEmpty(loadPlace.Handle))
                            //{
                            //}

                            if (loadPlace.Id == "P0273")
                            {
                            }

                            // Load other Place fields
                            loadPlace.GType = (string)pPlaceElement.Attribute("type");

                            loadPlace.GPTitle = GetElement(pPlaceElement, "ptitle");

                            loadPlace.GPlaceNames = GetPlaceNameModelCollection(pPlaceElement);

                            if (string.IsNullOrEmpty(loadPlace.GPlaceNames[0].DeRef.GValue))
                            {
                            }

                            loadPlace.GCode = GetElement(pPlaceElement, "code");

                            XElement coord = pPlaceElement.Element(ns + "coord");
                            if (!(coord is null))
                            {
                                double.TryParse(GetAttribute(coord, "lat"), out double latDouble);
                                double.TryParse(GetAttribute(coord, "long"), out double longDouble);

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
                catch (Exception e)
                {
                    _iocCommonNotifications.NotifyException("Exception loading Place data from the file", e);

                    throw;
                }
            }

            _iocCommonNotifications.DataLogEntryReplace("Place load complete");

            return;
        }
    }
}