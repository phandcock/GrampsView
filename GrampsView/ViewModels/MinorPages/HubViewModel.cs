namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
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

        // TODO cleanup naming. See personcitationchanges
        public CardGroupHLink<HLinkCitationModel> LatestCitationChanges
        {
            get
            {
                return DV.CitationDV.GetLatestChanges;
            }
        }

        public CardGroupHLink<HLinkEventModel> LatestEventChanges
        {
            get
            {
                return DV.EventDV.GetLatestChanges;
            }
        }

        public HLinkFamilyModelCollection LatestFamilyChanges
        {
            get
            {
                return DV.FamilyDV.GetLatestChanges;
            }
        }

        public CardGroupHLink<HLinkMediaModel> LatestMediaChanges
        {
            get
            {
                return DV.MediaDV.GetLatestChanges;
            }
        }

        public HLinkBaseCollection<HLinkNoteModel> LatestNoteChanges
        {
            get
            {
                return DV.NoteDV.GetLatestChanges;
            }
        }

        public HLinkPersonModelCollection LatestPersonChanges
        {
            get
            {
                return DV.PersonDV.GetLatestChanges;
            }
        }

        public CardGroupHLink<HLinkPlaceModel> LatestPlaceChanges
        {
            get
            {
                return DV.PlaceDV.GetLatestChanges;
            }
        }

        public CardGroupHLink<HLinkRepositoryModel> LatestRepositoryChanges
        {
            get
            {
                return DV.RepositoryDV.GetLatestChanges;
            }
        }

        public CardGroupHLink<HLinkSourceModel> LatestSourceChanges
        {
            get
            {
                return DV.SourceDV.GetLatestChanges;
            }
        }

        public CardGroupHLink<HLinkTagModel> LatestTagChanges
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

        public CardGroupHLink<HLinkNoteModel> ToDoList
        {
            get
            {
                // Setup ToDo list
                CardGroupModel<NoteModel> t = DV.NoteDV.GetAllOfType(CommonConstants.NoteTypeToDo);

                CardGroupHLink<HLinkNoteModel> toDoCardGroup = new CardGroupHLink<HLinkNoteModel>()
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
        }
    }
}