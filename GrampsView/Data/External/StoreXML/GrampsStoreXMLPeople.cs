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
    /// People load Routines.
    /// </summary>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        public static PersonModel SetHomeImage(PersonModel argModel)
        {
            if (argModel is null)
            {
                throw new ArgumentNullException(nameof(argModel));
            }

            ItemGlyph hlink = argModel.ModelItemGlyph;

            // check media
            ItemGlyph t = argModel.GMediaRefCollection.FirstHLinkHomeImage;
            if (!hlink.ValidImage & t.ValidImage)
            {
                hlink = t;
            }

            // Check Citation for Images
            t = argModel.GCitationRefCollection.FirstHLinkHomeImage;
            if (!hlink.ValidImage & t.ValidImage)
            {
                hlink = t;
            }

            // Action any Link
            if (hlink.Valid)
            {
                argModel.ModelItemGlyph = hlink;
            }

            return argModel;
        }

        /// <summary>
        /// Load the person data from the external storage XML file.
        /// </summary>
        /// <returns>
        /// Flag indicating if people data loaded successfully.
        /// </returns>
        public async Task LoadPeopleDataAsync()
        {
            localGrampsCommonLogging.RoutineEntry("loadPeopleData");

            await DataStore.Instance.CN.DataLogEntryAdd("Loading People data").ConfigureAwait(false);
            {
                string defaultImage = string.Empty;

                // Run query
                var de =
                    from el in localGrampsXMLdoc.Descendants(ns + "person")
                    select el;

                try
                {
                    foreach (XElement pname in de)
                    {
                        PersonModel loadPerson = DV.PersonDV.NewModel();

                        // Person attributes
                        loadPerson.LoadBasics(GetBasics(pname));

                        if (loadPerson.Id == "I0600")
                        {
                        }

                        // Address
                        loadPerson.GAddress = GetAddressCollection(pname);

                        // Get attribute collection
                        loadPerson.GAttributeCollection = GetAttributeCollection(pname);

                        // Childof
                        XElement tempChildOf = pname.Element(ns + "childof");
                        if (tempChildOf != null)
                        {
                            loadPerson.GChildOf.HLinkKey = (string)tempChildOf.Attribute("hlink");
                        }

                        // CitationRef collection
                        loadPerson.GCitationRefCollection = GetCitationCollection(pname);

                        // EventRef
                        loadPerson.GEventRefCollection = GetEventCollection(pname);

                        // gender
                        switch (GetElement(pname, "gender"))
                        {
                            case "F":
                                {
                                    loadPerson.GGender = CommonEnums.Gender.Female;
                                    break;
                                }

                            case "M":
                                {
                                    loadPerson.GGender = CommonEnums.Gender.Male;
                                    break;
                                }

                            default:
                                {
                                    loadPerson.GGender = CommonEnums.Gender.Unknown;
                                    break;
                                }
                        }

                        // TODO load LDS collection

                        // media object collection loading
                        loadPerson.GMediaRefCollection = await GetObjectCollection(pname).ConfigureAwait(false);

                        // Name
                        loadPerson.GPersonNamesCollection = GetPersonNameCollection(pname);

                        // NoteRefs Collection
                        loadPerson.GNoteRefCollection = GetNoteCollection(pname);

                        // Parentin
                        var localPIElement =
                            from pIElementEl in pname.Descendants(ns + "parentin")
                            select pIElementEl;

                        if (localPIElement.Any())
                        {
                            // load parentIn references
                            foreach (XElement loadPIElement in localPIElement)
                            {
                                HLinkFamilyModel t = new HLinkFamilyModel
                                {
                                    HLinkKey = (string)loadPIElement.Attribute("hlink"),
                                };
                                loadPerson.GParentInRefCollection.Add(t);
                            }

                            loadPerson.GParentInRefCollection.SortAndSetFirst();
                        }

                        // PersonRef
                        loadPerson.GPersonRefCollection = GetPersonRefCollection(pname);

                        // TagRef
                        loadPerson.GTagRefCollection = GetTagCollection(pname);

                        // URL
                        loadPerson.GURLCollection = GetURLCollection(pname);

                        // HomeImageLink
                        loadPerson = SetHomeImage(loadPerson);

                        // load the person
                        DV.PersonDV.PersonData.Add(loadPerson);

                        //var tt = (DataStore.Instance.DS.PersonNameData.Where(x => x.Value.GSurName.GetPrimarySurname == "Ainger"));
                        //if (tt.Count() > 0)
                        //{
                        //}
                    }

                    // let everybody know
                    localGrampsCommonLogging.RoutineExit("loadPeopleData");
                }
                catch (Exception ex)
                {
                    if (DV.PersonDV.PersonData.Count > 0)
                    {
                        // TODO Add this back + DV.PersonDV.PersonData[DV.PersonDV.PersonData.Count].GPersonNamesCollection.GetPrimaryName.FullName
                        DataStore.Instance.CN.NotifyException("Loading person from GRAMPSXML storage.  The last person successfully loaded was ", ex);
                        throw;
                    }
                    else
                    {
                        DataStore.Instance.CN.NotifyException("Loading person from GRAMPSXML storage.  No people have been loaded", ex);
                        throw;
                    }
                }
            }

            //var tt = (DataStore.Instance.DS.PersonNameData.Where(x => x.Value.GSurName.GetPrimarySurname == "Ainger"));
            //if (tt.Count() > 0)
            //{
            //}

            return;
        }
    }
}