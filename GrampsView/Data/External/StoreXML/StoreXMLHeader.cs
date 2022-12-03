using GrampsView.Common.CustomClasses;
using GrampsView.Data.Repository;
using GrampsView.Models.DataModels;

using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GrampsView.Data.ExternalStorage
{
    /// <summary>
    /// </summary>
    /// <seealso cref="GrampsView.Common.ObservableObject"/>
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
        public Task LoadHeaderDataAsync()
        {
            MyLog.DataLogEntryAdd("Loading Header Metadata");
            {
                try
                {
                    HeaderModel headerData = new HeaderModel();

                    // XNamespace ns = grampsXMLNameSpace;

                    // Run query
                    System.Collections.Generic.IEnumerable<XElement> de =
                        from el in LocalGrampsXMLdoc.Descendants(ns + "header")
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

                    DataStore.Instance.DS.HeaderData.Add(headerData);
                }
                catch (System.Exception ex)
                {
                    MyNotifications.NotifyException("Loading header from GRAMPSXML storage.  Header has not been loaded",ex,null);

                    throw;
                }
            }

            MyLog.DataLogEntryReplace("Header load complete");
            return Task.CompletedTask;
        }
    }
}