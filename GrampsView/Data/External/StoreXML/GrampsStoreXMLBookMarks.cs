// <copyright file="GrampsStoreXMLBookMarks.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.ExternalStorageNS
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using GrampsView.Data.DataView;

    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    /// <summary>
    /// Loads BookMark XML.
    /// </summary>
    /// <seealso cref="GrampsView.Common.CommonBindableBase"/>
    /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.ExternalStorageNS.IGrampsStoreXML"/>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        /// <summary>
        /// Loads the BookMark data asynchronous.
        /// </summary>
        /// <returns>
        /// True if loaded ok.
        /// </returns>
        public async Task LoadBookMarksAsync()
        {
            await DataStore.CN.MajorStatusAdd("Loading BookMark data").ConfigureAwait(false);
            {
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "bookmark")
                        select el;

                    // set BookMark count field
                    int bookMarkCount = 0;

                    // Loop through results to get the Citation Uri _baseUri = new Uri("ms-appx:///");
                    foreach (XElement argBookMark in de)
                    {
                        //BookMarkModel loadBookMark = new BookMarkModel();

                        // BookMark Handle
                        bookMarkCount++;
                        //loadBookMark.Handle = "BookMark" + Convert.ToString(bookMarkCount, System.Globalization.CultureInfo.CurrentCulture).Trim();

                        // BookMark fields
                        string GTarget = GetAttribute(argBookMark.Attribute("target"));
                        string GHLink = GetAttribute(argBookMark.Attribute("hlink"));
                        HLinkBackLink newHlinkBackLink = SetBookMarkTarget(GTarget, GHLink);

                        if (newHlinkBackLink.Valid)
                        {
                            DataStore.DS.BookMarkCollection.Add(newHlinkBackLink);
                        }
                        else
                        {
                            DataStore.CN.NotifyError("Bad BookMark HLink: " + argBookMark.ToString());
                        }

                        // save the event
                        //DV.BookMarkDV.BookMarkData.Add(loadBookMark);
                    }

                    // sort the collection eventRepository.Items.Sort(EventModel => EventModel);

                    // let everybody know
                }
                catch (Exception e)
                {
                    // TODO handle this
                    await DataStore.CN.MajorStatusAdd(e.Message).ConfigureAwait(false);

                    throw;
                }
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);
            return;
        }

        public HLinkBackLink SetBookMarkTarget(string argGTarget, string argGHlink)
        {
            switch (argGTarget)
            {
                case "person":
                    {
                        HLinkPersonModel p1 = new HLinkPersonModel
                        {
                            HLinkKey = argGHlink
                        };

                        return new HLinkBackLink(p1);
                    }

                case "family":
                    {
                        HLinkFamilyModel p1 = new HLinkFamilyModel
                        {
                            HLinkKey = argGHlink
                        };

                        return new HLinkBackLink(p1);
                    }

                case "event":
                    {
                        HLinkEventModel p1 = new HLinkEventModel
                        {
                            HLinkKey = argGHlink
                        };

                        return new HLinkBackLink(p1);
                    }

                case "source":
                    {
                        HLinkSourceModel p1 = new HLinkSourceModel
                        {
                            HLinkKey = argGHlink
                        };

                        return new HLinkBackLink(p1);
                    }

                case "citation":
                    {
                        HLinkCitationModel p1 = new HLinkCitationModel
                        {
                            HLinkKey = argGHlink
                        };

                        return new HLinkBackLink(p1);
                    }

                case "place":
                    {
                        HLinkPlaceModel p1 = new HLinkPlaceModel
                        {
                            HLinkKey = argGHlink
                        };

                        return new HLinkBackLink(p1);
                    }

                case "media":
                    {
                        HLinkMediaModel p1 = new HLinkMediaModel
                        {
                            HLinkKey = argGHlink
                        };

                        return new HLinkBackLink(p1);
                    }

                case "repository":
                    {
                        HLinkRepositoryModel p1 = new HLinkRepositoryModel
                        {
                            HLinkKey = argGHlink
                        };

                        return new HLinkBackLink(p1);
                    }

                case "note":
                    {
                        HLinkNoteModel p1 = new HLinkNoteModel
                        {
                            HLinkKey = argGHlink
                        };

                        return new HLinkBackLink(p1);
                    }
            }

            return new HLinkBackLink();
        }
    }
}