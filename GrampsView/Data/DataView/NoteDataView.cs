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

namespace GrampsView.Data.DataView
{
    /// <summary>
    /// Data view into the Notes Repository.
    /// </summary>

    public class NoteDataView : DataViewBase<NoteModel, HLinkNoteModel, HLinkNoteModelCollection>, INoteDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteDataView"/> class.
        /// </summary>
        public NoteDataView()
        {
        }

        public override IReadOnlyList<NoteModel> DataDefaultSort
        {
            get
            {
                return DataViewData.OrderBy(NoteModel => NoteModel.GStyledText.GText).ToList();
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<NoteModel> DataViewData
        {
            get
            {
                List<NoteModel> returnValue = new List<NoteModel>();

                System.Collections.ObjectModel.ReadOnlyCollection<NoteDBModel> t = Ioc.Default.GetRequiredService<IStoreDB>().NoteAccess.ToList().AsReadOnly();

                foreach (NoteDBModel? item in t)
                {
                    returnValue.Add(item.DeSerialise());
                }

                return returnValue;
            }
        }

        public override HLinkNoteModelCollection GetLatestChanges
        {
            get
            {
                if (DatabaseAvailable)
                {
                    DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                    IEnumerable tt = DataViewData.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

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

        public override HLinkNoteModelCollection GetAllAsCardGroupBase()
        {
            HLinkNoteModelCollection t = new HLinkNoteModelCollection();

            foreach (NoteModel item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            // Sort TODO Sort t = HLinkCollectionSort(t);

            return t;
        }

        public override Group<HLinkNoteModelCollection> GetAllAsGroupedCardGroup()
        {
            Group<HLinkNoteModelCollection> t = new Group<HLinkNoteModelCollection>();

            var query = from item in DataViewData
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

            foreach (NoteModel item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        /// <summary>
        /// Gets all models of a particlar note type.
        /// </summary>
        /// <param name="argType">
        /// Note type to search for
        /// </param>
        /// <returns>
        /// </returns>
        public CardGroupModel<NoteModel> GetAllOfType(string argType)
        {
            CardGroupModel<NoteModel> t = new CardGroupModel<NoteModel>();

            IEnumerable<NoteModel> q = DataViewData.Where(NoteModel => NoteModel.GType == argType);

            foreach (NoteModel item in q)
            {
                t.Add(item);
            }

            return new CardGroupModel<NoteModel>(q);
        }

        public override NoteModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            return Ioc.Default.GetRequiredService<IStoreDB>().NoteAccess.Where(x => x.HLinkKeyValue == argHLinkKey.Value).First().DeSerialise();
        }

        public override NoteModel GetModelFromId(string argId)
        {
            NoteModel t = DataViewData.FirstOrDefault(X => X.Id == argId);

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

            IOrderedEnumerable<NoteModel> temp = DataViewData.Where(x => x.GStyledText.GText.ToLower(CultureInfo.CurrentCulture).Contains(queryString)).Distinct().OrderBy(y => y.ToString());

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

            IEnumerable<NoteModel> temp = from gig in DataViewData
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