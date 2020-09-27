// <copyright file="MediaDetailViewModel.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;
    using Prism.Navigation;

    using System.Diagnostics.Contracts;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// Defines the EVent Detail Page View ViewModel.
    /// </summary>
    public class MediaDetailViewModel : ViewModelBase
    {
        private HLinkMediaModel _CurrentHLinkMedia = new HLinkMediaModel();

        /// <summary>
        /// The local media object.
        /// </summary>
        private MediaModel _MediaObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// The navigation service.
        /// </param>
        public MediaDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseCL.LogProgress("MediaDetailViewModel created");
        }

        public HLinkMediaModel CurrentHLinkMedia
        {
            get
            {
                return _CurrentHLinkMedia;
            }

            set
            {
                SetProperty(ref _CurrentHLinkMedia, value);
            }
        }

        /// <summary>
        /// Gets or sets the current media object.
        /// </summary>
        /// <value>
        /// The current media object.
        /// </value>
        public MediaModel CurrentMediaObject
        {
            get
            {
                return _MediaObject;
            }

            set
            {
                SetProperty(ref _MediaObject, value);
            }
        }

        /// <summary>
        /// Gets or sets the h link parameter.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <value>
        /// The h link parameter.
        /// </value>
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            // Clear large Bitmap Image
            if (CurrentMediaObject != null)
            {
                CurrentMediaObject.FullImageClean();
            }
        }

        /// <summary>
        /// Handles navigation in wards and sets up the event model parameter.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void PopulateViewModel()
        {
            BaseCL.LogRoutineEntry("MediaDetailViewModel OnNavigatedTo");

            CurrentHLinkMedia = BaseNavParamsHLink as HLinkMediaModel;

            if (!(CurrentHLinkMedia is null))
            {
                CurrentMediaObject = DV.MediaDV.GetModelFromHLink(CurrentHLinkMedia);

                // Trigger refresh of View fields via INotifyPropertyChanged
                RaisePropertyChanged(string.Empty);

                if (!(CurrentMediaObject is null))
                {
                    BaseTitle = CurrentMediaObject.GetDefaultText;
                    BaseTitleIcon = CommonConstants.IconMedia;

                    // Get basic details
                    //CardGroup t = new CardGroup { Title = "Header Details" };

                    BaseDetail.Add(new CardListLineCollection
                    {
                        new CardListLine("Card Type:", "Media Detail"),
                        new CardListLine("File Description:", CurrentMediaObject.GDescription),
                        new CardListLine("File Mime Type:", CurrentMediaObject.FileMimeType),
                        new CardListLine("File Content Type:", CurrentMediaObject.FileContentType),
                        new CardListLine("File Mime SubType:", CurrentMediaObject.FileMimeSubType),
                        new CardListLine("OriginalFilePath:", CurrentMediaObject.OriginalFilePath),
                    });

                    // Get date card
                    BaseDetail.Add(CurrentMediaObject.GDateValue.AsCardListLine());

                    // Set up note re opening in photo app
                    CardListLineCollection t1 = new CardListLineCollection
                        {
                            new CardListLine(string.Empty, "Note: Double click the file image / symbol to open it.")
                        };

                    BaseDetail.Add(t1);

                    // Add standard details
                    BaseDetail.Add(DV.MediaDV.GetModelInfoFormatted(CurrentMediaObject));

                    //BaseDetail.Add(t);

                    // Get media image
                    HLinkHomeImageModel personImage = CurrentMediaObject.HomeImageHLink;
                    Contract.Assert(CurrentMediaObject.HomeImageHLink != null, CurrentMediaObject.Id);
                    personImage.CardType = DisplayFormat.MediaImageFullCard;
                    BaseDetail.Add(personImage);

                    // Add HLink Details
                    //BaseDetail.Add(CurrentHLinkMedia.GAttributeRefCollection);
                    //BaseDetail.Add(CurrentHLinkMedia.GCitationRefCollection.GetCardGroup("HLink Citations"));
                    //BaseDetail.Add(CurrentHLinkMedia.GNoteRefCollection.GetCardGroup("HLink Notes"));

                    //// Add Model details
                    //BaseDetail.Add(CurrentMediaObject.GCitationRefCollection.GetCardGroup());
                    //BaseDetail.Add(CurrentMediaObject.GNoteRefCollection.GetCardGroup());
                    //BaseDetail.Add(CurrentMediaObject.GTagRefCollection.GetCardGroup());
                }

                BaseCL.LogRoutineExit("MediaDetailViewModel OnNavigatedTo");
            }
        }
    }
}