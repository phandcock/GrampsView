namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    using System;

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

        private string _MediaPath;

        private bool _ShowImageElement = false;
        private bool _ShowMediaElement = false;

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

        public string MediaPath
        {
            get
            {
                return _MediaPath;
            }

            set
            {
                SetProperty(ref _MediaPath, value);
            }
        }

        public bool ShowImageElement
        {
            get
            {
                return _ShowImageElement;
            }

            set
            {
                SetProperty(ref _ShowImageElement, value);
            }
        }

        public bool ShowMediaElement
        {
            get
            {
                return _ShowMediaElement;
            }

            set
            {
                SetProperty(ref _ShowMediaElement, value);
                MediaPath = CurrentMediaObject.MediaStorageFilePath;
            }
        }

        /// <summary>
        /// Handles navigation in wards and sets up the event model parameter.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void BaseHandleAppearingEvent()
        {
            BaseCL.RoutineEntry("MediaDetailViewModel OnNavigatedTo");

            CurrentHLinkMedia = CommonRoutines.DeserialiseObject<HLinkMediaModel>(Uri.UnescapeDataString(BaseParamsHLink));

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
                    BaseDetail.Add(DV.MediaDV.GetModelInfoFormatted(CurrentMediaObject));
                }

                // Show MediaElement
                if ((CurrentMediaObject.FileMimeType == "video") || (CurrentMediaObject.FileMimeType == "audio"))
                {
                    ShowMediaElement = true;
                    ShowImageElement = false;
                }
                else
                {
                    ShowMediaElement = false;
                    ShowImageElement = true;
                }

                BaseCL.RoutineExit("MediaDetailViewModel OnNavigatedTo");
            }
        }
    }
}