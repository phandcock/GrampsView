namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    /// <summary>
    /// </summary>
    public partial class StoreXML : IStoreXML
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
        public async Task LoadRepositoriesAsync()
        {
            await DataStore.Instance.CN.DataLogEntryAdd(nameof(LoadRepositoriesAsync)).ConfigureAwait(false);

            try
            {
                // Run query
                var de =
                    from el in localGrampsXMLdoc.Descendants(ns + "repository")
                    select el;

                foreach (XElement pRepositoryElement in de)
                {
                    RepositoryModel loadRepository = DV.RepositoryDV.NewModel();

                    // SecondaryColor attributes
                    loadRepository.LoadBasics(GetBasics(pRepositoryElement));
                    //loadRepository.Id = (string)pRepositoryElement.Attribute("id");
                    //loadRepository.Change = GetDateTime(pRepositoryElement, "change");
                    //loadRepository.Priv = SetPrivateObject((string)pRepositoryElement.Attribute("priv"));
                    //loadRepository.Handle = (string)pRepositoryElement.Attribute("handle");

                    if (loadRepository.Id == "R0000")
                    {
                    }

                    // Repository fields
                    loadRepository.GRName = GetElement(pRepositoryElement, "rname");
                    loadRepository.GType = GetElement(pRepositoryElement, "type");
                    loadRepository.GAddress = GetAddressCollection(pRepositoryElement);
                    loadRepository.GURL = GetURLCollection(pRepositoryElement);
                    loadRepository.GNoteRefCollection = GetNoteCollection(pRepositoryElement);
                    loadRepository.GTagRefCollection = GetTagCollection(pRepositoryElement);

                    // save the event
                    DV.RepositoryDV.RepositoryData.Add(loadRepository);
                }
            }
            catch (Exception e)
            {
                // TODO handle this
                await DataStore.Instance.CN.DataLogEntryAdd(e.Message).ConfigureAwait(false);

                throw;
            }

            return;
        }
    }
}