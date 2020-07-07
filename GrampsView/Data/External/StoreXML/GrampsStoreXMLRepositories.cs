//-----------------------------------------------------------------------
//
// Storage routines for GrampsStoreXML
//
// <copyright file="GrampsStoreXMLRepositories.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.ExternalStorageNS
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using Xamarin.Forms;

    /// <summary>
    /// </summary>
    public partial class GrampsStoreXML : IGrampsStoreXML
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
            await DataStore.CN.MajorStatusAdd(nameof(LoadRepositoriesAsync)).ConfigureAwait(false);

            try
            {
                // Run query
                var de =
                    from el in localGrampsXMLdoc.Descendants(ns + "repository")
                    select el;

                foreach (XElement prepository in de)
                {
                    RepositoryModel loadRepository = DV.RepositoryDV.NewModel();

                    // SecondaryColor attributes
                    loadRepository.Id = (string)prepository.Attribute("id");
                    loadRepository.Change = GetDateTime(prepository, "change");
                    loadRepository.Priv = SetPrivateObject((string)prepository.Attribute("priv"));
                    loadRepository.Handle = (string)prepository.Attribute("handle");

                    if (loadRepository.Id == "R0000")
                    {
                    }

                    // Repository fields
                    loadRepository.GRName = GetElement(prepository, "rname");
                    loadRepository.GType = GetElement(prepository, "type");
                    loadRepository.GAddress = GetAddressCollection(prepository);
                    loadRepository.GURL = GetURLCollection(prepository);
                    loadRepository.GNoteRefCollection = GetNoteCollection(prepository);
                    loadRepository.GTagRefCollection = GetTagCollection(prepository);

                    // save the event
                    DV.RepositoryDV.RepositoryData.Add(loadRepository);
                }
            }
            catch (Exception e)
            {
                // TODO handle this
                await DataStore.CN.MajorStatusAdd(e.Message).ConfigureAwait(false);

                throw;
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);
            return;
        }
    }
}