namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    /// <summary>
    /// Media Detail ViewModel
    /// </summary>
    public class MediaDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common logger.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        public MediaDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseCL.Progress("MediaDetailViewModel created");
        }

        public HLinkMediaModel CurrentHLinkMedia
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the current media object.
        /// </summary>
        /// <value>
        /// The current media object.
        /// </value>
        public IMediaModel CurrentMediaObject
        {
            get; set;
        }

        public IHLinkMediaModel MediaCard
        {
            get; set;
        }

        /// <summary>
        /// Handles navigation inwards and gets the media model parameter.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void HandleViewDataLoadEvent()
        {
            BaseCL.RoutineEntry("MediaDetailViewModel OnNavigatedTo");

            CurrentHLinkMedia = CommonRoutines.GetHLinkParameter<HLinkMediaModel>((BaseParamsHLink));

            // For cropped or internal media then show the original image
            IMediaModel tt = CurrentHLinkMedia.DeRef;
            if (tt.IsInternalMediaFile)
            {
                CurrentHLinkMedia = DV.MediaDV.GetModelFromHLinkKey(tt.InternalMediaFileOriginalHLink).HLink;
            }

            if (!(CurrentHLinkMedia is null))
            {
                CurrentMediaObject = CurrentHLinkMedia.DeRef;

                if (!(CurrentMediaObject is null))
                {
                    BaseModelBase = CurrentMediaObject;
                    BaseTitleIcon = CommonConstants.IconMedia;

                    MediaCard = CurrentMediaObject.ModelItemGlyph.ImageHLinkMediaModel;

                    // Get basic details
                    BaseDetail.Add(new CardListLineCollection("Media Detail")
                    {
                        new CardListLine("File Description:", CurrentMediaObject.GDescription),
                        new CardListLine("File Mime Type:", CurrentMediaObject.FileMimeType),
                        new CardListLine("File Content Type:", CurrentMediaObject.FileContentType),
                        new CardListLine("File Mime SubType:", CurrentMediaObject.FileMimeSubType),
                        new CardListLine("OriginalFilePath:", CurrentMediaObject.OriginalFilePath),
                    });

                    // Get date card
                    BaseDetail.Add(CurrentMediaObject.GDateValue.AsHLink("Media Date"));

                    // Add standard details
                    MediaModel t = CurrentMediaObject as MediaModel;
                    BaseDetail.Add(DV.MediaDV.GetModelInfoFormatted(t));
                }

                BaseCL.RoutineExit("MediaDetailViewModel OnNavigatedTo");
            }
        }
    }
}