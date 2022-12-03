using GrampsView.Data.DataView;
using GrampsView.Data.External.StoreXML;
using GrampsView.Data.Model;

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GrampsView.Data.ExternalStorage
{
    public partial class StoreXML : IStoreXML
    {
        public Task LoadRepositoriesAsync()
        {
            MyLog.DataLogEntryAdd("Loading Repository data");
            {
                try
                {
                    // Run query
                    System.Collections.Generic.IEnumerable<XElement> de =
                        from el in LocalGrampsXMLdoc.Descendants(ns + "repository")
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
                    MyLog.DataLogEntryAdd(e.Message);

                    throw;
                }

                MyLog.DataLogEntryReplace("Repository load complete");
                return Task.CompletedTask;
            }
        }
    }
}