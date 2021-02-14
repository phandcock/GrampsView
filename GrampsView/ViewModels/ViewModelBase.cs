namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Events;

    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;

    using System.Diagnostics;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.UI.Views;
    using Xamarin.Forms;

    [QueryProperty(nameof(BaseParamsHLink), nameof(BaseParamsHLink))]
    public class ViewModelBase : BindableBase
    {
        /// <summary>
        /// Backing store for the base current state
        /// </summary>
        private LayoutState _BaseCurrentState = LayoutState.None;

        private CardGroup _BaseDetail = new CardGroup();

        /// <summary>
        /// Backing store for the base current state
        /// </summary>
        private string _BaseTitle = string.Empty;

        /// <summary>
        /// Backing store for the base current state
        /// </summary>
        private string _BaseTitleIcon = string.Empty;

        /// <summary>
        /// Backing store for the base current state
        /// </summary>
        private ICommonLogging _CL;

        /// <summary>
        /// Backing store for the base current state
        /// </summary>
        private IDialogService _DialogService;

        /// <summary>
        /// Backing store for the base current state
        /// </summary>
        private IEventAggregator _EventAggregator;

        private string _ParamsHLink;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        public ViewModelBase()
        {
            BaseDetail.Title = "Unknown Details";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// The ioc navigation service.
        /// </param>
        public ViewModelBase(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
        {
            BaseCL = iocCommonLogging;
            BaseEventAggregator = iocEventAggregator;

            _EventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(BaseHandleDataLoadedEventInternal, ThreadOption.UIThread);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        public ViewModelBase(ICommonLogging iocCommonLogging)
        {
            BaseCL = iocCommonLogging;
        }

        /// <summary>
        /// Gets or sets the base common logger.
        /// </summary>
        /// <value>
        /// The base cl.
        /// </value>
        public ICommonLogging BaseCL
        {
            get
            {
                Debug.Assert(_CL != null, "BaseCL is null.  Was this set in the constructor for the derived class?");

                return _CL;
            }

            private set
            {
                SetProperty(ref _CL, value);
            }
        }

        /// <summary>
        /// Gets or sets the state of the base current.
        /// </summary>
        /// <value>
        /// The state of the base current.
        /// </value>
        public LayoutState BaseCurrentState
        {
            get
            {
                return _BaseCurrentState;
            }

            set
            {
                _BaseCurrentState = value;
                RaisePropertyChanged(nameof(BaseCurrentState));
            }
        }

        /// <summary>
        /// Gets the base detail.
        /// </summary>
        /// <value>
        /// The base detail.
        /// </value>
        public CardGroup BaseDetail
        {
            get
            {
                return _BaseDetail;
            }

            set
            {
                SetProperty(ref _BaseDetail, value);
            }
        }

        /// <summary>
        /// Gets or sets the base dialog service.
        /// </summary>
        /// <value>
        /// The base dialog service.
        /// </value>
        public IDialogService BaseDialogService
        {
            get
            {
                Debug.Assert(_DialogService != null, "DialogService is null.  Was this set in the constructor for the derived class?");

                return _DialogService;
            }

            set
            {
                SetProperty(ref _DialogService, value);
            }
        }

        /// <summary>
        /// Gets or sets the base event aggregator.
        /// </summary>
        /// <value>
        /// The base event aggregator.
        /// </value>
        public IEventAggregator BaseEventAggregator
        {
            get
            {
                Debug.Assert(_EventAggregator != null, "BaseEventAggregator is null.  Was this set in the constructor for the derived class?");

                return _EventAggregator;
            }

            private set
            {
                SetProperty(ref _EventAggregator, value);
            }
        }

        public string BaseParamsHLink
        {
            get
            {
                return _ParamsHLink;
            }

            set
            {
                SetProperty(ref _ParamsHLink, value); // , BaseOnNavigatedTo);
            }
        }

        /// <summary>
        /// Gets or sets the base title.
        /// </summary>
        /// <value>
        /// The base title.
        /// </value>
        public string BaseTitle
        {
            get
            {
                return _BaseTitle;
            }
            set
            {
                if (!(value == null))
                {
                    value = CommonRoutines.ReplaceLineSeperators(value);

                    SetProperty(ref _BaseTitle, value.Substring(0, value.Length > 50 ? 50 : value.Length));
                }
            }
        }

        /// <summary>
        /// Gets or sets the base title icon.
        /// </summary>
        /// <value>
        /// The base title icon.
        /// </value>
        public string BaseTitleIcon
        {
            get
            {
                return _BaseTitleIcon;
            }

            set
            {
                SetProperty(ref _BaseTitleIcon, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [detail data loaded flag].
        /// </summary>
        /// <value>
        /// <c>true</c> if [detail data loaded flag]; otherwise, <c>false</c>.
        /// </value>
        private bool DetailDataLoadedFlag
        {
            get; set;
        }

        /// <summary>
        /// Called when [navigated to].
        /// </summary>
        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// Nothibg.
        /// </returns>
        public virtual void BaseHandleAppearingEvent()
        {
            return;
        }

        public virtual async Task BaseHandledDataLoadedEvent()
        {
        }

        internal void BaseHandleAppearingEventInternal()
        {
            if (!DetailDataLoadedFlag)
            {
                BaseCurrentState = LayoutState.Loading;

                BaseHandleAppearingEvent();

                DetailDataLoadedFlag = true;
            }
            else
            {
                BaseCurrentState = LayoutState.None;
            }

            //// Setup for loading if no data is loaded
            //if (!DataStore.Instance.DS.IsDataLoaded)
            //{
            //    BaseCurrentState = LayoutState.None;
            //}
            //else
            //{
            //    BaseCurrentState = LayoutState.Loading;
            //}
        }

        /// <summary>
        /// Sets the state of the data loaded view.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        internal async void BaseHandleDataLoadedEventInternal()
        {
            DetailDataLoadedFlag = false;

            this.BaseCurrentState = LayoutState.None;

            // Trigger refresh of View fields via INotifyPropertyChanged
            RaisePropertyChanged(string.Empty);

            await BaseHandledDataLoadedEvent();
        }
    }
}