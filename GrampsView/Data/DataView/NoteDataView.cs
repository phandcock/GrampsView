namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;
    using GrampsView.Data.Repository;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

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
                return NoteData.Values.ToList();
            }
        }

        public RepositoryModelDictionary<NoteModel, HLinkNoteModel> NoteData
        {
            get
            {
                return DataStore.Instance.DS.NoteData;
            }
        }

        public override CardGroupBase<HLinkNoteModel> GetAllAsCardGroup()
        {
            CardGroupBase<HLinkNoteModel> t = new CardGroupBase<HLinkNoteModel>();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            // Sort TODO Sort t = HLinkCollectionSort(t);

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

            foreach (var item in DataDefaultSort)
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
        public CardGroupBase<INoteModel> GetAllOfType(string argType)
        {
            IEnumerable<INoteModel> q = DataViewData.Where(NoteModel => NoteModel.GType == argType);

            return new CardGroupBase<INoteModel>(q);
        }

        public override CardGroupBase<HLinkNoteModel> GetLatestChanges()
        {
            DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

            IEnumerable tt = DataViewData.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

            CardGroupBase<HLinkNoteModel> returnCardGroup = new CardGroupBase<HLinkNoteModel>();

            foreach (NoteModel item in tt)
            {
                returnCardGroup.Add(item.HLink);
            }

            returnCardGroup.Title = "Latest Note Changes";

            return returnCardGroup;
        }

        public override NoteModel GetModelFromHLinkString(string HLinkString)
        {
            return NoteData[HLinkString];
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

            IOrderedEnumerable<HLinkNoteModel> t = collectionArg.OrderBy(HLinkNoteModel => HLinkNoteModel.DeRef.TextShort);

            HLinkNoteModelCollection tt = new HLinkNoteModelCollection();

            foreach (HLinkNoteModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override CardGroupBase<HLinkNoteModel> Search(string queryString)
        {
            CardGroupBase<HLinkNoteModel> itemsFound = new CardGroupBase<HLinkNoteModel>();

            if (string.IsNullOrEmpty(queryString))
            {
                return itemsFound;
            }

            var temp = DataViewData.Where(x => x.GStyledText.GText.ToLower(CultureInfo.CurrentCulture).Contains(queryString)).OrderBy(y => y.GetDefaultText);

            if (temp.Any())
            {
                foreach (NoteModel tempMO in temp)
                {
                    itemsFound.Add(tempMO.HLink);
                }
            }

            return itemsFound;
        }

        public CardGroupBase<HLinkNoteModel> SearchTag(string queryString)
        {
            CardGroupBase<HLinkNoteModel> itemsFound = new CardGroupBase<HLinkNoteModel>();

            if (string.IsNullOrEmpty(queryString))
            {
                return itemsFound;
            }

            var temp = from gig in DataViewData
                       where gig.GTagRefCollection.Any(act => act.DeRef.GName == queryString)
                       select gig;

            if (temp.Any())
            {
                foreach (NoteModel tempMO in temp)
                {
                    itemsFound.Add( tempMO.HLink);
                }
            }

            return itemsFound;
        }
    }
}