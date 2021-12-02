namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using SharedSharp.Errors;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    /// <summary>
    /// Loads BookMark XML.
    /// </summary>
    public partial class StoreXML : IStoreXML
    {
        public static HLinkBackLink SetBookMarkTarget(string argGTarget, HLinkKey argHLinkKey)
        {
            switch (argGTarget)
            {
                case "person":
                    {
                        HLinkPersonModel p1 = DV.PersonDV.GetModelFromHLinkKey(argHLinkKey).HLink;

                        return new HLinkBackLink(p1);
                    }

                case "family":
                    {
                        HLinkFamilyModel p1 = DV.FamilyDV.GetModelFromHLinkKey(argHLinkKey).HLink;

                        return new HLinkBackLink(p1);
                    }

                case "event":
                    {
                        HLinkEventModel p1 = DV.EventDV.GetModelFromHLinkKey(argHLinkKey).HLink;

                        return new HLinkBackLink(p1);
                    }

                case "source":
                    {
                        HLinkSourceModel p1 = DV.SourceDV.GetModelFromHLinkKey(argHLinkKey).HLink;

                        return new HLinkBackLink(p1);
                    }

                case "citation":
                    {
                        HLinkCitationModel p1 = DV.CitationDV.GetModelFromHLinkKey(argHLinkKey).HLink;

                        return new HLinkBackLink(p1);
                    }

                case "place":
                    {
                        HLinkPlaceModel p1 = DV.PlaceDV.GetModelFromHLinkKey(argHLinkKey).HLink;

                        return new HLinkBackLink(p1);
                    }

                case "media":
                    {
                        HLinkMediaModel p1 = DV.MediaDV.GetModelFromHLinkKey(argHLinkKey).HLink;

                        return new HLinkBackLink(p1);
                    }

                case "repository":
                    {
                        HLinkRepositoryModel p1 = DV.RepositoryDV.GetModelFromHLinkKey(argHLinkKey).HLink;

                        return new HLinkBackLink(p1);
                    }

                case "note":
                    {
                        HLinkNoteModel p1 = DV.NoteDV.GetModelFromHLinkKey(argHLinkKey).HLink;

                        return new HLinkBackLink(p1);
                    }
            }

            return new HLinkBackLink();
        }

        /// <summary>
        /// Loads the BookMark data asynchronous.
        /// </summary>
        /// <returns>
        /// True if loaded ok.
        /// </returns>
        public async Task LoadBookMarksAsync()
        {
            _iocCommonNotifications.DataLogEntryAdd("Loading BookMark data");
            {
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "bookmark")
                        select el;

                    // set BookMark count field
                    int bookMarkCount = 0;

                    // Loop through results
                    foreach (XElement argBookMark in de)
                    {
                        // BookMark Handle
                        bookMarkCount++;

                        // BookMark fields
                        string GTarget = GetAttribute(argBookMark.Attribute("target"));
                        HLinkKey GHLink = new HLinkKey(GetAttribute(argBookMark.Attribute("hlink")));
                        HLinkBackLink newHlinkBackLink = SetBookMarkTarget(GTarget, GHLink);

                        if (newHlinkBackLink.Valid)
                        {
                            DataStore.Instance.DS.BookMarkCollection.Add(newHlinkBackLink);
                        }
                        else
                        {
                            _iocCommonNotifications.NotifyError(new ErrorInfo("Bad BookMark")
                                {
                                    { "HLink",  argBookMark.ToString() }
                                });
                        }

                        _iocCommonNotifications.DataLogEntryReplace($"Loading bookmark type: {newHlinkBackLink.HLinkType}");
                    }

                    DataStore.Instance.DS.BookMarkCollection.Title = string.Empty;
                }
                catch (Exception e)
                {
                    // TODO handle this
                    _iocCommonNotifications.DataLogEntryAdd(e.Message);

                    throw;
                }
            }

            _iocCommonNotifications.DataLogEntryReplace("Bookmark load complete");

            return;
        }
    }
}