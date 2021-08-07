namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Events;

    using Prism.Events;

    /// <summary>
    /// View model for the Hub Page.
    /// </summary>
    public class HubViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HubViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public HubViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
       : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Hub";
            BaseTitleIcon = CommonConstants.IconHub;

            BaseEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(HandledDataLoadedEvent, ThreadOption.UIThread);
        }

        public CardListLineCollection HeaderCard
        {
            get
            {
                return DV.HeaderDV.HeaderDataModel.AsCardListLineCollection;
            }
        }

        public CardGroupBase<HLinkCitationModel> LatestCitationChanges
        {
            get
            {
                return DV.CitationDV.GetLatestChanges;
            }
        }

        public CardGroupBase<HLinkEventModel> LatestEventChanges
        {
            get
            {
                return DV.EventDV.GetLatestChanges;
            }
        }

        public CardGroupBase<HLinkFamilyModel> LatestFamilyChanges
        {
            get
            {
                return DV.FamilyDV.GetLatestChanges;
            }
        }

        public CardGroupBase<HLinkMediaModel> LatestMediaChanges
        {
            get
            {
                return DV.MediaDV.GetLatestChanges;
            }
        }

        public CardGroupBase<HLinkNoteModel> LatestNoteChanges
        {
            get
            {
                return DV.NoteDV.GetLatestChanges;
            }
        }

        public CardGroupBase<HLinkPersonModel> LatestPersonChanges
        {
            get
            {
                return DV.PersonDV.GetLatestChanges;
            }
        }

        public CardGroupBase<HLinkPlaceModel> LatestPlaceChanges
        {
            get
            {
                return DV.PlaceDV.GetLatestChanges;
            }
        }

        public CardGroupBase<HLinkRepositoryModel> LatestRepositoryChanges
        {
            get
            {
                return DV.RepositoryDV.GetLatestChanges;
            }
        }

        public CardGroupBase<HLinkSourceModel> LatestSourceChanges
        {
            get
            {
                return DV.SourceDV.GetLatestChanges;
            }
        }

        public CardGroupBase<HLinkTagModel> LatestTagChanges
        {
            get
            {
                return DV.TagDV.GetLatestChanges;
            }
        }

        public IHLinkMediaModel MediaCard
        {
            get
            {
                return DV.MediaDV.GetRandomFromCollection(DV.MediaDV.GetAllNotClippedAsHLink());
            }
        }

        public CardGroupBase<HLinkNoteModel> ToDoList
        {
            get
            {
                // Setup ToDo list
                CardGroupBase<INoteModel> t = DV.NoteDV.GetAllOfType(CommonConstants.NoteTypeToDo);

                CardGroupBase<HLinkNoteModel> toDoCardGroup = new CardGroupBase<HLinkNoteModel>()
                {
                    Title = "ToDo list",
                };

                foreach (NoteModel item in t)
                {
                    toDoCardGroup.Add(item.HLink);
                }

                return toDoCardGroup;
            }
        }

        public void HandledDataLoadedEvent()
        {
            OnPropertyChanged(string.Empty);

            //var t = this;

            //var tt = HeaderCard;

            //OnPropertyChanged(string.Empty);
        }
    }
}