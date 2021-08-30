namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public partial class StoreXML : IStoreXML
    {
        /// <summary>
        /// load families from external storage.
        /// </summary>
        /// <param name="familyRepository">
        /// The family repository.
        /// </param>
        /// <returns>
        /// Flag indicating if the family data was loaded.
        /// </returns>
        public async Task<bool> LoadFamiliesAsync()
        {
            // RepositoryModelType<FamilyModel, HLinkFamilyModel>
            await _iocCommonNotifications.DataLogEntryAdd("Loading Family data").ConfigureAwait(false);
            {
                // Load notes
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "family")
                        select el;

                    // get family fields TODO

                    // Loop through results to get the Families
                    foreach (XElement familyElement in de)
                    {
                        FamilyModel loadFamily = new FamilyModel();

                        // Family attributes
                        loadFamily.LoadBasics(GetBasics(familyElement));

                        if (loadFamily.Id == "F0151")
                        {
                        }

                        // Family fields

                        // relationship type
                        XElement tempRelationship = familyElement.Element(ns + "rel");
                        if (tempRelationship != null)
                        {
                            loadFamily.GFamilyRelationship = (string)tempRelationship.Attribute("type");
                        }

                        // father element
                        XElement tempFather = familyElement.Element(ns + "father");
                        if (tempFather != null)
                        {
                            loadFamily.GFather.HLinkGlyphItem.ImageType = Common.CommonEnums.HLinkGlyphType.TempLoading;
                            loadFamily.GFather.HLinkKey = new HLinkKey((string)tempFather.Attribute("hlink"));
                        }

                        // mother element
                        XElement tempMother = familyElement.Element(ns + "mother");
                        if (tempMother != null)
                        {
                            loadFamily.GMother.HLinkGlyphItem.ImageType = Common.CommonEnums.HLinkGlyphType.TempLoading;
                            loadFamily.GMother.HLinkKey = new HLinkKey((string)tempMother.Attribute("hlink"));
                        }

                        loadFamily.GDate = GetDate(familyElement.Element(ns + "date"));

                        loadFamily.GAttributeCollection = GetAttributeCollection(familyElement);

                        loadFamily.GChildRefCollection = GetChildRefCollection(familyElement);

                        // Citation References
                        loadFamily.GCitationRefCollection = GetCitationCollection(familyElement);

                        // Event References
                        loadFamily.GEventRefCollection = GetEventCollection(familyElement);

                        loadFamily.GLDSOrdCollection = GetLDSOrdCollection(familyElement);

                        // ObjectRef loading
                        loadFamily.GMediaRefCollection = await GetObjectCollection(familyElement).ConfigureAwait(false);

                        loadFamily.GNoteRefCollection = GetNoteCollection(familyElement);

                        loadFamily.GTagRefCollection = GetTagCollection(familyElement);

                        // set the Home image or symbol now that everything is laoded loadFamily = SetHomeImage(loadFamily);

                        // save the family
                        DV.FamilyDV.FamilyData.Add(loadFamily);
                        _iocCommonLogging.LogVariable("Family Name", loadFamily.HLinkKey.Value);
                    }
                }
                catch (Exception e)
                {
                    // TODO handle this
                    await _iocCommonNotifications.DataLogEntryAdd(e.Message).ConfigureAwait(false);
                    throw;
                }
            }

            _iocCommonNotifications.DataLogEntryReplace("Family load complete");

            return true;
        }
    }
}