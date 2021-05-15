﻿namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    /// <summary>
    /// </summary>
    /// <seealso cref="GrampsView.Common.CommonBindableBase"/>
    /// /// ///
    /// <seealso cref="GrampsView.Data.ExternalStorage.IStoreXML"/>
    public partial class StoreXML : IStoreXML
    {
        /// <summary>
        /// Loads the header meta data.
        /// </summary>
        /// <returns>
        /// Flag indicating if the header data was loaded.
        /// </returns>
        public async Task LoadHeaderDataAsync()
        {
            await DataStore.CN.DataLogEntryAdd("Loading Header Metadata").ConfigureAwait(false);
            {
                try
                {
                    HeaderModel headerData = new HeaderModel();

                    // XNamespace ns = grampsXMLNameSpace;

                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "header")
                        select el;

                    // there should only be one but ...
                    foreach (XElement pname in de)
                    {
                        headerData = new HeaderModel
                        {
                            HLinkKey = new HLinkKey("~HeaderData")
                        };

                        // header element
                        XElement localcreated = pname.Element(ns + "created");

                        headerData.GCreatedVersion = (string)localcreated.Attribute("version");
                        headerData.GCreatedDate = (string)localcreated.Attribute("date");

                        // researcher element
                        XElement researcher = pname.Element(ns + "researcher");

                        headerData.GResearcherName = (string)researcher.Element(ns + "resname");
                        headerData.GResearcherAddress = (string)researcher.Element(ns + "resaddr");
                        headerData.GResearcherLocality = (string)researcher.Element(ns + "reslocality");
                        headerData.GResearcherCity = (string)researcher.Element(ns + "rescity");
                        headerData.GResearcherState = (string)researcher.Element(ns + "resstate");
                        headerData.GResearcherCountry = (string)researcher.Element(ns + "rescountry");
                        headerData.GResearcherPostal = (string)researcher.Element(ns + "respostal");
                        headerData.GResearcherPhone = (string)researcher.Element(ns + "resphone");
                        headerData.GResearcherEmail = (string)researcher.Element(ns + "resemail");

                        // mediapath element
                        headerData.GMediaPath = (string)pname.Element(ns + "mediapath");
                    }

                    DataStore.DS.HeaderData.Add(headerData);
                }
                catch (System.Exception ex)
                {
                    DataStore.CN.NotifyException("Loading header from GRAMPSXML storage.  Header has not been loaded", ex);

                    throw;
                }
            }

            await DataStore.CN.DataLogEntryReplace("Header load complete");

            return;
        }
    }
}