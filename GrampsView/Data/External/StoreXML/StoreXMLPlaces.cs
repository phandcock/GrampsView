namespace GrampsView.Data.ExternalStorage
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
        public async Task LoadPlacesAsync()
        {
            await _iocCommonNotifications.DataLogEntryAdd("Loading Place data").ConfigureAwait(false);

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

                            loadPlace.GPlaceParentCollection = GetPlaceRefCollection(pPlaceElement);
                            loadPlace.GPlaceParentCollection.Title = "Parent Places";

                            loadPlace.GTagRefCollection = GetTagCollection(pPlaceElement);

                            loadPlace.GURLCollection = GetURLCollection(pPlaceElement);

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

            await _iocCommonNotifications.DataLogEntryReplace("Place load complete");

            return;
        }
    }
}