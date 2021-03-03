namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    using System;

    /// <summary>
    /// Media Detail ViewModel
    /// </summary>
    public class MediaDetailViewModel : ViewModelBase
    {
        private HLinkMediaModel _CurrentHLinkMedia = new HLinkMediaModel();

        /// <summary>
        /// The local media object.
        /// </summary>
        private IMediaModel _CurrentMediaObject;

        private IHLinkMediaModel _MediaCard = new HLinkMediaModel();

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
        public IMediaModel CurrentMediaObject
        {
            get
            {
                return _CurrentMediaObject;
            }

            set
            {
                SetProperty(ref _CurrentMediaObject, value);
            }
        }

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
        /// Handles navigation inwards and gets the media model parameter.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void BaseHandleAppearingEvent()
        {
            BaseCL.RoutineEntry("MediaDetailViewModel OnNavigatedTo");

            CurrentHLinkMedia = CommonRoutines.DeserialiseObject<HLinkMediaModel>(Uri.UnescapeDataString(BaseParamsHLink));

            if (!(CurrentHLinkMedia is null))
            {
                CurrentMediaObject = CurrentHLinkMedia.DeRef;

                if (!(CurrentMediaObject is null))
                {
                    BaseTitle = CurrentMediaObject.GetDefaultText;
                    BaseTitleIcon = CommonConstants.IconMedia;

                    MediaCard = CurrentMediaObject.ModelItemGlyph.HLinkMedia;

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
                    BaseDetail.Add(CurrentMediaObject.GDateValue.AsCardListLine());

                    // Add standard details
                    MediaModel t = CurrentMediaObject as MediaModel;
                    BaseDetail.Add(DV.MediaDV.GetModelInfoFormatted(t));
                }

                BaseCL.RoutineExit("MediaDetailViewModel OnNavigatedTo");
            }
        }
    }
}