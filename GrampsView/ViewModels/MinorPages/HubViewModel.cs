// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.Events;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels;
using GrampsView.Models.HLinks.Interfaces;
using GrampsView.Models.HLinks.Models;

namespace GrampsView.ViewModels.MinorPages
{
    /// <summary>
    /// View model for the Hub Page.
    /// </summary>
    public class HubViewModel : ViewModelBase
    {
        /// <summary>Initializes a new instance of the <see cref="HubViewModel" /> class.</summary>
        /// <param name="iocCommonLogging">The ioc common logging.</param>
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
                 SharedSharp.Common.SharedSharpNavigation.NavigateAsyncNS(new SharedSharp.Views.SharedSharpMessageLogPage());
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

                CardGroupHLink<HLinkNoteModel> toDoCardGroup = new()
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