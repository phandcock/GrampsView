﻿namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public partial class StoreXML : IStoreXML
    {
        public async Task LoadRepositoriesAsync()
        {
            _iocCommonNotifications.DataLogEntryAdd("Loading Repository data");
            {
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "repository")
                        select el;

                    foreach (XElement pRepositoryElement in de)
                    {
                        RepositoryModel loadRepository = new RepositoryModel();

                        loadRepository.LoadBasics(GetBasics(pRepositoryElement));

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
                    _iocCommonNotifications.DataLogEntryAdd(e.Message);

                    throw;
                }

                _iocCommonNotifications.DataLogEntryReplace("Repository load complete");

                return;
            }
        }
    }
}