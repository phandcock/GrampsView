﻿namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Events;

    using Prism.Events;
    using Prism.Services.Dialogs;

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
        /// <param name="iocDialogService">
        /// Prism DIalog service
        /// </param>
        public HubViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, IDialogService iocDialogService)
       : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Hub";
            BaseTitleIcon = CommonConstants.IconHub;

            BaseDialogService = iocDialogService;

            //BaseEventAggregator.GetEvent<DialogBoxEvent>().Subscribe(ErrorActionDialog, ThreadOption.UIThread);
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
                CardGroupBase<INoteModel> t = DV.NoteDV.GetAllOfType(NoteModel.GTypeToDo);

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

        /// <summary>
        /// Populate the Hub View.
        /// </summary>
        public override void BaseHandleAppearingEvent()
        {
        }

        public void HandledDataLoadedEvent()
        {
            OnPropertyChanged(string.Empty);
        }
    }
}