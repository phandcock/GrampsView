// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.DBModels;
using GrampsView.Events;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.HLinks.Interfaces;
using GrampsView.Models.HLinks.Models;
using GrampsView.ModelsDB.Collections.HLinks;
using GrampsView.ModelsDB.HLinks.Models;

using SharedSharp.Views;

namespace GrampsView.ViewModels.MinorPages
{
    /// <summary>
    /// View model for the Hub Page.
    /// </summary>
    public class HubViewModel : ViewModelBase
    {
        public CardListLineCollection HeaderCard => DV.HeaderDV.HeaderDataModel.AsCardListLineCollection;

        // TODO clean up naming. See person citation changes
        public DBCardGroupHLink<HLinkCitationDBModel> LatestCitationChanges => DL.CitationDL.GetLatestChanges;

        public DBCardGroupHLink<HLinkEventDBModel> LatestEventChanges => DL.EventDL.GetLatestChanges;

        public HLinkFamilyDBModelCollection LatestFamilyChanges => DL.FamilyDL.GetLatestChanges;

        public CardGroupHLink<HLinkMediaModel> LatestMediaChanges => DV.MediaDV.GetLatestChanges;

        public HLinkNoteDBModelCollection LatestNoteChanges => DL.NoteDL.GetLatestChanges;

        public HLinkPersonModelCollection LatestPersonChanges => DV.PersonDV.GetLatestChanges;

        public CardGroupHLink<HLinkPlaceModel> LatestPlaceChanges => DV.PlaceDV.GetLatestChanges;

        public CardGroupHLink<HLinkRepositoryModel> LatestRepositoryChanges => DV.RepositoryDV.GetLatestChanges;

        public CardGroupHLink<HLinkSourceModel> LatestSourceChanges => DV.SourceDV.GetLatestChanges;

        public CardGroupHLink<HLinkTagModel> LatestTagChanges => DV.TagDV.GetLatestChanges;

        public IHLinkMediaModel MediaCard => DV.MediaDV.GetRandomFromCollection(DV.MediaDV.GetAllNotClippedAsHLink());

        public DBCardGroupHLink<HLinkNoteDBModel> ToDoList
        {
            get
            {
                // Setup ToDo list
                DBCardGroupModel<NoteDBModel> t = DL.NoteDL.GetAllOfType(Constants.NoteTypeToDo);

                DBCardGroupHLink<HLinkNoteDBModel> toDoCardGroup = new()
                {
                    Title = "ToDo list",
                };

                foreach (NoteDBModel item in t)
                {
                    toDoCardGroup.Add(item.HLink);
                }

                return toDoCardGroup;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HubViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        public HubViewModel(ILog iocCommonLogging, IMessenger iocMessenger)
            : base(iocCommonLogging)
        {
            BaseTitle = "Hub";
            BaseTitleIcon = Constants.IconHub;

            iocMessenger.Register<DataLoadCompleteEvent>(this, (r, m) =>
            {
                HandledDataLoadedEvent();
            });

            iocMessenger.Register<AppStartLoadDataEvent>(this, (r, m) =>
             {
                 SharedSharp.Navigation.SharedNavigation.NavigateAsync(nameof(SharedSharpMessageLogPage));
             });
        }

        public void HandledDataLoadedEvent()
        {
            OnPropertyChanged(string.Empty);

            Application.Current?.MainPage?.Dispatcher.DispatchAsync(new Action(() =>
            {
                Shell.Current.Navigation.PopToRootAsync();
            }));
        }
    }
}