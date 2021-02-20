/// <summary>
/// </summary>
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
    /// <seealso cref="GrampsView.Common.CommonBindableBase"/>
    /// /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.ExternalStorageNS.IGrampsStoreXML"/>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        public static FamilyModel SetHomeImage(FamilyModel argModel)
        {
            // Try media reference collection first
            ItemGlyph hlink = argModel.GMediaRefCollection.FirstHLinkHomeImage;

            if (!hlink.Valid)
            {
                hlink = argModel.GCitationRefCollection.FirstHLinkHomeImage;
            }

            if (!hlink.Valid)
            {
                hlink = argModel.GEventRefCollection.FirstHLinkHomeImage;
            }

            if (!hlink.Valid)
            {
                hlink = argModel.GNoteRefCollection.FirstHLinkHomeImage;
            }

            // Set the image if available
            if (hlink.Valid)
            {
                argModel.ModelItemGlyph = hlink;
            }

            return argModel;
        }

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
            await DataStore.Instance.CN.DataLogEntryAdd("Loading Family data").ConfigureAwait(false);
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
                        FamilyModel loadFamily = DV.FamilyDV.NewModel();

                        // Family attributes
                        loadFamily.LoadBasics(GetBasics(familyElement));
                        //loadFamily.Id = (string)familyElement.Attribute("id");

                        ////if (loadFamily.Id == "F0152")
                        ////{
                        ////}

                        //loadFamily.Handle = (string)familyElement.Attribute("handle");
                        //loadFamily.Change = GetDateTime((string)familyElement.Attribute("change"));
                        //loadFamily.Priv = SetPrivateObject((string)familyElement.Attribute("priv"));

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
                            loadFamily.GFather.HLinkKey = (string)tempFather.Attribute("hlink");
                        }

                        // mother element
                        XElement tempMother = familyElement.Element(ns + "mother");
                        if (tempMother != null)
                        {
                            loadFamily.GMother.HLinkKey = (string)tempMother.Attribute("hlink");
                        }

                        // ChildRef loading
                        var thisORElement =
                            from thisORElementEl in familyElement.Descendants(ns + "childref")
                            select thisORElementEl;

                        if (thisORElement.Any())
                        {
                            // load child object references
                            foreach (XElement thisLoadORElement in thisORElement)
                            {
                                HLinkPersonModel t = new HLinkPersonModel
                                {
                                    // load the hlink
                                    HLinkKey = (string)thisLoadORElement.Attribute("hlink"),
                                };
                                loadFamily.GChildRefCollection.Add(t);
                            }
                        }

                        // Citation References
                        loadFamily.GCitationRefCollection = GetCitationCollection(familyElement);

                        // Event References
                        loadFamily.GEventRefCollection = GetEventCollection(familyElement);

                        // ObjectRef loading
                        loadFamily.GMediaRefCollection = await GetObjectCollection(familyElement).ConfigureAwait(false);

                        loadFamily.GNoteRefCollection = GetNoteCollection(familyElement);

                        loadFamily.GTagRefCollection = GetTagCollection(familyElement);

                        // set the Home image or symbol now that everything is laoded
                        loadFamily = SetHomeImage(loadFamily);

                        // save the family
                        DV.FamilyDV.FamilyData.Add(loadFamily);
                        localGrampsCommonLogging.LogVariable("Family Name", loadFamily.Handle);
                    }
                }
                catch (Exception e)
                {
                    // TODO handle this
                    await DataStore.Instance.CN.DataLogEntryAdd(e.Message).ConfigureAwait(false);
                    throw;
                }
            }

            return true;
        }
    }
}