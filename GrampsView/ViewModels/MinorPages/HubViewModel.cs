namespace GrampsView.ViewModels
{
    using GrampsView.Assets.Strings;
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;
    using Prism.Navigation;

    using static GrampsView.Common.CommonEnums;

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
        /// <param name="iocNavigationService">
        /// </param>
        public HubViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
       : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Hub";
            BaseTitleIcon = CommonConstants.IconHub;
        }

        //public InstructCardModel Instructions
        //{
        //    get
        //    {
        //        return new InstructCardModel
        //        {
        //            TextDetails = AppResources.HubPage_IntroductionText,
        //        };
        //    }
        //}

        public CardGroupBase<HLinkCitationModel> LatestCitationChanges { get { return DV.CitationDV.GetLatestChanges(); } }
        public CardGroupBase<HLinkEventModel> LatestEventChanges { get { return DV.EventDV.GetLatestChanges(); } }
        public CardGroupBase<HLinkFamilyModel> LatestFamilyChanges { get { return DV.FamilyDV.GetLatestChanges(); } }
        public CardGroupBase<HLinkMediaModel> LatestMediaChanges { get { return DV.MediaDV.GetLatestChanges(); } }
        public CardGroupBase<HLinkNoteModel> LatestNoteChanges { get { return DV.NoteDV.GetLatestChanges(); } }
        public CardGroupBase<HLinkPersonModel> LatestPersonChanges { get { return DV.PersonDV.GetLatestChanges(); } }
        public CardGroupBase<HLinkPlaceModel> LatestPlaceChanges { get { return DV.PlaceDV.GetLatestChanges(); } }
        public CardGroupBase<HLinkSourceModel> LatestSourceChanges { get { return DV.SourceDV.GetLatestChanges(); } }
        public CardGroupBase<HLinkTagModel> LatestTagChanges { get { return DV.TagDV.GetLatestChanges(); } }

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


        private IHLinkMediaModel _MediaCard = new HLinkMediaModel();

        public IHLinkMediaModel MediaCard
        {
            get
            {
                return _MediaCard;
            }
            set
            {
                SetProperty(ref _MediaCard, value);
            }
        }

        /// <summary>
        /// Populate the Hub View.
        /// </summary>
        public override void PopulateViewModel()
        {
        

            // Get Header CardLarge
            CardGroup hc = new CardGroup();
            HLinkHeaderModel HeaderCard = DV.HeaderDV.HeaderDataModel.HLink;
            HeaderCard.CardType = DisplayFormat.HeaderCardLarge;
            hc.Add(HeaderCard);

            MediaCard = DV.MediaDV.GetRandomFromCollection(null);
   

            BaseDetail.Add(hc);

            //if (!HeroImage.Valid)
            //{
            //    DataStore.CN.NotifyAlert("No images found in this data.  Consider adding some.");
            //}

            // Setup Latest Changes list

            // TODO fix this LatestChanges.Add(DV.BookMarkDV.GetLatestChanges());

            return;
        }
    }
}