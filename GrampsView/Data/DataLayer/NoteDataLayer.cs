// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Model;
using GrampsView.Data.StoreDB;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels;
using GrampsView.Models.DBModels;

using Microsoft.EntityFrameworkCore;

using System.Collections;
using System.Globalization;

namespace GrampsView.Data.DataLayer
{
    /// <summary>
    /// Data view into the Notes Repository.
    /// </summary>

    public class NoteDataLayer : DataLayerBase<NoteModel, HLinkNoteModel, HLinkNoteModelCollection>, INoteDataLayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteDataView"/> class.
        /// </summary>
        public NoteDataLayer()
        {
        }

        public override void ResetCache()
        {
            _DataAsDefaultSort = new List<NoteModel>();
            _DataAsList = new List<NoteModel>();
        }

        public override IReadOnlyList<NoteModel> DataAsDefaultSort
        {
            get
            {
                // Cache it
                if (_DataAsDefaultSort.Count > 0)
                {
                    return _DataAsDefaultSort;
                }

                _DataAsDefaultSort = DataAsList.OrderBy(NoteModel => NoteModel.GStyledText.GText).ToList();

                return _DataAsDefaultSort;
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<NoteModel> DataAsList
        {
            get
            {
                // Cache it
                if (_DataAsList.Count > 0)
                {
                    return _DataAsList;
                }

                _DataAsList = new List<NoteModel>();

                System.Collections.ObjectModel.ReadOnlyCollection<NoteDBModel> t = NoteAccess.ToList().AsReadOnly();

                foreach (NoteDBModel? item in t)
                {
                    _DataAsList.Add(item.DeSerialise());
                }

                return _DataAsList;
            }
        }

        public override HLinkNoteModelCollection GetLatestChanges
        {
            get
            {
                if (DatabaseAvailable)
                {
                    DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                    IEnumerable tt = DataAsList.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

                    HLinkNoteModelCollection returnCardGroup = new HLinkNoteModelCollection();

                    foreach (NoteModel item in tt)
                    {
                        returnCardGroup.Add(item.HLink);
                    }

                    returnCardGroup.Title = "Latest Note Changes";

                    return returnCardGroup;
                }

                return new HLinkNoteModelCollection();
            }
        }

        public DbSet<NoteDBModel> NoteAccess
        {
            get
            {
                return Ioc.Default.GetRequiredService<IStoreDB>().NoteAccess;
            }
        }

        private List<NoteModel> _DataAsDefaultSort { get; set; } = new List<NoteModel>();
        private List<NoteModel> _DataAsList { get; set; } = new List<NoteModel>();

        public override HLinkNoteModelCollection GetAllAsCardGroupBase()
        {
            HLinkNoteModelCollection t = new();

            foreach (NoteModel item in DataAsDefaultSort)
            {
                t.Add(item.HLink);
            }

            // Sort TODO Sort t = HLinkCollectionSort(t);

            return t;
        }

        public override Group<HLinkNoteModelCollection> GetAllAsGroupedCardGroup()
        {
            Group<HLinkNoteModelCollection> t = new Group<HLinkNoteModelCollection>();

            var query = from item in DataAsList
                        orderby item.GType, item.ToString()
                        group item by item.GType into g
                        select new
                        {
                            GroupName = g.Key,
                            Items = g
                        };

            foreach (var g in query)
            {
                HLinkNoteModelCollection info = new HLinkNoteModelCollection
                {
                    Title = g.GroupName,
                };

                foreach (NoteModel? item in g.Items)
                {
                    info.Add(item.HLink);
                }

                t.Add(info);
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
        public HLinkNoteModelCollection GetAllAsHLink()
        {
            HLinkNoteModelCollection t = new HLinkNoteModelCollection();

            foreach (NoteModel item in DataAsDefaultSort)
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
        public CardGroupModel<NoteModel> GetAllOfType(string argType)
        {
            CardGroupModel<NoteModel> t = new CardGroupModel<NoteModel>();

            IEnumerable<NoteModel> q = DataAsList.Where(NoteModel => NoteModel.GType == argType);

            foreach (NoteModel item in q)
            {
                t.Add(item);
            }

            return new CardGroupModel<NoteModel>(q);
        }

        public override NoteModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            IQueryable<NoteDBModel> t = Ioc.Default.GetRequiredService<IStoreDB>().NoteAccess.Where(x => x.HLinkKeyValue == argHLinkKey.Value);

            if (t.Any())
            {
                return t.First().DeSerialise();
            }

            return new NoteModel();
        }

        public override NoteModel GetModelFromId(string argId)
        {
            NoteModel t = DataAsList.FirstOrDefault(X => X.Id == argId);

            if (t is null)
            {
                return new NoteModel();
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
        public override HLinkNoteModelCollection HLinkCollectionSort(HLinkNoteModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkNoteModel> t = collectionArg.OrderBy(HLinkNoteModel => HLinkNoteModel.DeRef.GStyledText.TextShort);

            HLinkNoteModelCollection tt = new HLinkNoteModelCollection();

            foreach (HLinkNoteModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override HLinkNoteModelCollection Search(string queryString)
        {
            HLinkNoteModelCollection itemsFound = new HLinkNoteModelCollection();

            if (string.IsNullOrEmpty(queryString))
            {
                return itemsFound;
            }

            IOrderedEnumerable<NoteModel> temp = DataAsList.Where(x => x.GStyledText.GText.ToLower(CultureInfo.CurrentCulture).Contains(queryString)).Distinct().OrderBy(y => y.ToString());

            if (temp.Any())
            {
                foreach (NoteModel tempMO in temp)
                {
                    itemsFound.Add(tempMO.HLink);
                }
            }

            return itemsFound;
        }

        public List<SearcHandlerItem> SearchShell(string argQuery)
        {
            List<SearcHandlerItem> returnValue = new List<SearcHandlerItem>();

            foreach (HLinkNoteModel item in Search(argQuery))
            {
                returnValue.Add(new SearcHandlerItem(item));
            }

            return returnValue;
        }

        public CardGroupHLink<HLinkNoteModel> SearchTag(string argQuery)
        {
            CardGroupHLink<HLinkNoteModel> itemsFound = new CardGroupHLink<HLinkNoteModel>
            {
                Title = "Notes"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            IEnumerable<NoteModel> temp = from gig in DataAsList
                                          where gig.GTagRefCollection.Any(act => act.DeRef.GName == argQuery)
                                          select gig;

            if (temp.Any())
            {
                foreach (NoteModel tempMO in temp)
                {
                    itemsFound.Add(tempMO.HLink);
                }
            }

            return itemsFound;
        }
    }
}