using CommunityToolkit.Mvvm.Messaging;

using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.Events;

using Microsoft.Extensions.DependencyInjection;

using SharedSharp.Errors.Interfaces;
using SharedSharp.Model;

namespace GrampsView.ViewModels.MinorPages
{
    /// <summary>
    /// View model for the Hub Page.
    /// </summary>
    public class HubViewModel : ViewModelBase
    {
        public IErrorNotifications _iocErrorNotifications;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public HubViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging, IMessenger iocEventAggregator, IErrorNotifications iocErrorNotifications)
       : base(iocCommonLogging)
        {
            _iocErrorNotifications = iocErrorNotifications;

            BaseTitle = "Hub";
            BaseTitleIcon = Constants.IconHub;

            App.Current.Services.GetService<IMessenger>().Register<DataLoadCompleteEvent>(this, (r, m) =>
            {
                HandledDataLoadedEvent();
            });

            App.Current.Services.GetService<IMessenger>().Register<DataLoadStartEvent>(this, (r, m) =>
             {
                 _iocErrorNotifications.DataLogShow();
             });
        }

        public CardListLineCollection HeaderCard => DV.HeaderDV.HeaderDataModel.AsCardListLineCollection;

        // TODO cleanup naming. See personcitationchanges
        public CardGroupHLink<HLinkCitationModel> LatestCitationChanges => DV.CitationDV.GetLatestChanges;

        public CardGroupHLink<HLinkEventModel> LatestEventChanges => DV.EventDV.GetLatestChanges;

        public HLinkFamilyModelCollection LatestFamilyChanges => DV.FamilyDV.GetLatestChanges;

        public CardGroupHLink<HLinkMediaModel> LatestMediaChanges => DV.MediaDV.GetLatestChanges;

        public HLinkBaseCollection<HLinkNoteModel> LatestNoteChanges => DV.NoteDV.GetLatestChanges;

        public HLinkPersonModelCollection LatestPersonChanges => DV.PersonDV.GetLatestChanges;

        public CardGroupHLink<HLinkPlaceModel> LatestPlaceChanges => DV.PlaceDV.GetLatestChanges;

        public CardGroupHLink<HLinkRepositoryModel> LatestRepositoryChanges => DV.RepositoryDV.GetLatestChanges;

        public CardGroupHLink<HLinkSourceModel> LatestSourceChanges => DV.SourceDV.GetLatestChanges;

        public CardGroupHLink<HLinkTagModel> LatestTagChanges => DV.TagDV.GetLatestChanges;

        public IHLinkMediaModel MediaCard => DV.MediaDV.GetRandomFromCollection(DV.MediaDV.GetAllNotClippedAsHLink());

        public CardGroupHLink<HLinkNoteModel> ToDoList
        {
            get
            {
                // Setup ToDo list
                CardGroupModel<NoteModel> t = DV.NoteDV.GetAllOfType(Constants.NoteTypeToDo);

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

            _ = _iocErrorNotifications.DataLogHide();
        }
    }
}