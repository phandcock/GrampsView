// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.DataLayer.Interfaces;
using GrampsView.Data.StoreDB;
using GrampsView.DBModels;
using GrampsView.Models.Collections.HLinks;
using GrampsView.ModelsDB.HLinks.Models;

using System.Collections;
using System.Globalization;

namespace GrampsView.Data.DataLayer
{
    /// <summary>
    /// Data view into the Notes Repository.
    /// </summary>

    public class NoteDataLayer : DataLayerBase<NoteDBModel, HLinkNoteDBModel, HLinkNoteDBModelCollection>, INoteDataLayer
    {
        public override IReadOnlyList<NoteDBModel> DataAsDefaultSort
        {
            get
            {
                // Cache it
                if (_DataAsDefaultSort.Count > 0)
                {
                    return _DataAsDefaultSort;
                }

                _DataAsDefaultSort = DataAsList.OrderBy(NoteDBModel => NoteDBModel.GStyledText.GText).ToList();

                return _DataAsDefaultSort;
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<NoteDBModel> DataAsList
        {
            get
            {
                // Cache it
                if (_DataAsList.Count > 0)
                {
                    return _DataAsList;
                }

                _DataAsList = new List<NoteDBModel>();

                System.Collections.ObjectModel.ReadOnlyCollection<NoteDBModel> t = Ioc.Default.GetRequiredService<IStoreDB>().NoteAccess.ToList().AsReadOnly();

                foreach (NoteDBModel? item in t)
                {
                    _DataAsList.Add(item);
                }

                return _DataAsList;
            }
        }

        public override HLinkNoteDBModelCollection GetLatestChanges
        {
            get
            {
                if (localStoreDB.IsOpen)
                {
                    DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                    IEnumerable tt = DataAsList.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

                    HLinkNoteDBModelCollection returnCardGroup = new HLinkNoteDBModelCollection();

                    foreach (NoteDBModel item in tt)
                    {
                        returnCardGroup.Add(item.HLink);
                    }

                    returnCardGroup.Title = "Latest Note Changes";

                    return returnCardGroup;
                }

                return new HLinkNoteDBModelCollection();
            }
        }

        private List<NoteDBModel> _DataAsDefaultSort { get; set; } = new List<NoteDBModel>();

        private List<NoteDBModel> _DataAsList { get; set; } = new List<NoteDBModel>();

        public override HLinkNoteDBModelCollection GetAllAsCardGroupBase()
        {
            HLinkNoteDBModelCollection t = new();

            foreach (NoteDBModel item in DataAsDefaultSort)
            {
                t.Add(item.HLink);
            }

            // Sort TODO Sort t = HLinkCollectionSort(t);

            return t;
        }

        public override Group<HLinkNoteDBModelCollection> GetAllAsGroupedCardGroup()
        {
            Group<HLinkNoteDBModelCollection> t = new Group<HLinkNoteDBModelCollection>();

            IQueryable<IGrouping<string, NoteDBModel>> query = Ioc.Default.GetRequiredService<IStoreDB>().NoteAccess.GroupBy(x => x.GType);

            if (query.Any())
            {
                foreach (IGrouping<string, NoteDBModel> g in query)
                {
                    HLinkNoteDBModelCollection info = new HLinkNoteDBModelCollection
                    {
                        Title = g.Key,
                    };

                    foreach (NoteDBModel item in g)
                    {
                        info.Add(item.HLink);
                    }

                    t.Add(info);
                }
            }

            return t;
        }

        /// <summary>
        /// </summary>
        /// <value>
        /// The note data.
        /// </value>
        /// <returns>
        /// HLInknotemodel collection
        /// </returns>
        public HLinkNoteDBModelCollection GetAllAsHLink()
        {
            HLinkNoteDBModelCollection t = new HLinkNoteDBModelCollection();

            foreach (NoteDBModel item in DataAsDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        /// <summary>
        /// Gets all models of a particular note type.
        /// </summary>
        /// <param name="argType">
        /// Note type to search for
        /// </param>
        /// <returns>
        /// </returns>
        public DBCardGroupModel<NoteDBModel> GetAllOfType(string argType)
        {
            DBCardGroupModel<NoteDBModel> t = new DBCardGroupModel<NoteDBModel>();

            IEnumerable<NoteDBModel> q = DataAsList.Where(NoteDBModel => NoteDBModel.GType == argType);

            foreach (NoteDBModel item in q)
            {
                t.Add(item);
            }

            return new DBCardGroupModel<NoteDBModel>(q);
        }

        public override NoteDBModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            IQueryable<NoteDBModel> t = Ioc.Default.GetRequiredService<IStoreDB>().NoteAccess.Where(x => x.HLinkKeyValue == argHLinkKey.Value);

            if (t.Any())
            {
                return t.First();
            }

            return new NoteDBModel();
        }

        public override NoteDBModel GetModelFromId(string argId)
        {
            NoteDBModel t = DataAsList.FirstOrDefault(X => X.Id == argId);

            if (t is null)
            {
                return new NoteDBModel();
            }

            return t;
        }

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// Sorted hlink collection.
        /// </returns>
        public override HLinkNoteDBModelCollection HLinkCollectionSort(HLinkNoteDBModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkNoteDBModel> t = collectionArg.OrderBy(HLinkNoteModel => HLinkNoteModel.DeRef.GStyledText.TextShort);

            HLinkNoteDBModelCollection tt = new HLinkNoteDBModelCollection();

            foreach (HLinkNoteDBModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override void ResetCache()
        {
            _DataAsDefaultSort = new List<NoteDBModel>();
            _DataAsList = new List<NoteDBModel>();
        }

        public override HLinkNoteDBModelCollection Search(string queryString)
        {
            HLinkNoteDBModelCollection itemsFound = new HLinkNoteDBModelCollection();

            if (string.IsNullOrEmpty(queryString))
            {
                return itemsFound;
            }

            IOrderedEnumerable<NoteDBModel> temp = DataAsList.Where(x => x.GStyledText.GText.ToLower(CultureInfo.CurrentCulture).Contains(queryString)).Distinct().OrderBy(y => y.ToString());

            if (temp.Any())
            {
                foreach (NoteDBModel tempMO in temp)
                {
                    itemsFound.Add(tempMO.HLink);
                }
            }

            return itemsFound;
        }

        public List<SearcHandlerItem> SearchShell(string argQuery)
        {
            List<SearcHandlerItem> returnValue = new List<SearcHandlerItem>();

            foreach (HLinkNoteDBModel item in Search(argQuery))
            {
                returnValue.Add(new SearcHandlerItem(item));
            }

            return returnValue;
        }

        public DBCardGroupHLink<HLinkNoteDBModel> SearchTag(string argQuery)
        {
            DBCardGroupHLink<HLinkNoteDBModel> itemsFound = new DBCardGroupHLink<HLinkNoteDBModel>
            {
                Title = "Notes"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            IEnumerable<NoteDBModel> temp = from gig in DataAsList
                                            where gig.GTagRefCollection.Any(act => act.DeRef.GName == argQuery)
                                            select gig;

            if (temp.Any())
            {
                foreach (NoteDBModel tempMO in temp)
                {
                    itemsFound.Add(tempMO.HLink);
                }
            }

            return itemsFound;
        }
    }
}