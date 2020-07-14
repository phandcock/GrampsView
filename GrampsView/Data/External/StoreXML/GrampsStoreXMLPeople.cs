//-----------------------------------------------------------------------
//
// People load routines for the GrampsStoreXML
//
// <copyright file="GrampsStoreXMLPeople.cs" company="PlaceholderCompany">
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

            HLinkHomeImageModel hlink = argModel.GMediaRefCollection.FirstHLinkHomeImage;

            // Check Citation for Images
            if (!hlink.Valid)
            {
                hlink = argModel.GCitationRefCollection.FirstHLinkHomeImage;
            }

            // Action any Link
            if (hlink.Valid)
            {
                argModel.HomeImageHLink.HomeImageType = CommonEnums.HomeImageType.ThumbNail;
                argModel.HomeImageHLink.HLinkKey = hlink.HLinkKey;
            }

            return argModel;
        }

        /// <summary>
        /// load the person data from the external storage XML file.
        /// </summary>
        /// <param name="personRepository">
        /// The person repository.
        /// </param>
        /// <returns>
        /// Flag indicating if people data loaded successfully.
        /// </returns>
        public async Task LoadPeopleDataAsync()
        {
            localGrampsCommonLogging.LogRoutineEntry("loadPeopleData");

            await DataStore.CN.MajorStatusAdd("Loading People data").ConfigureAwait(false);
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
                        //loadPerson.Id = GetAttribute(pname.Attribute("id"));

                        if (loadPerson.Id == "I0571")
                        {
                        }

                        //loadPerson.Change = GetDateTime(pname, "change");
                        //loadPerson.Priv = SetPrivateObject(GetAttribute(pname.Attribute("priv")));
                        //loadPerson.Handle = GetAttribute(pname, "handle");

                        // if (loadPerson.Id == "I0922") { // Why parent hugh cameron display bad? } Address
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
                        loadPerson.GGender = GetElement(pname, "gender");

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
                    }

                    // let everybody know
                    localGrampsCommonLogging.LogRoutineExit("loadPeopleData");
                }
                catch (Exception ex)
                {
                    if (DV.PersonDV.PersonData.Count > 0)
                    {
                        // TODO Add this back + DV.PersonDV.PersonData[DV.PersonDV.PersonData.Count].GPersonNamesCollection.GetPrimaryName.FullName
                        DataStore.CN.NotifyException("Loading person from GRAMPSXML storage.  The last person successfully loaded was ", ex);
                        throw;
                    }
                    else
                    {
                        DataStore.CN.NotifyException("Loading person from GRAMPSXML storage.  No people have been loaded", ex);
                        throw;
                    }
                }
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);

            return;
        }
    }
}