using GrampsView.Common;
using GrampsView.Data.Model;
using GrampsView.Events;

using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;

using System.Diagnostics;
using System.Diagnostics.Contracts;

using Xamarin.Forms.StateSquid;

namespace GrampsView.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible, IInitialize
    {
        /// <summary>
        /// Backing store for the base current state
        /// </summary>
        private State _BaseCurrentState = State.None;

        /// <summary>
        /// Backing store for the base current state
        /// </summary>
        private HLinkBase _BaseNavParamsHLink;

        /// <summary>
        /// Backing store for the base current state
        /// </summary>
        private object _BaseNavParamsModel;

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

        /// <summary>
        /// Backing store for the base current state
        /// </summary>
        private INavigationService _NavigationService;

        /// <summary>
        /// Backing store for the base current state
        /// </summary>
        private INavigationParameters _NavParams;

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
        public ViewModelBase(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
        {
            BaseCL = iocCommonLogging;
            BaseEventAggregator = iocEventAggregator;
            BaseNavigationService = iocNavigationService;

            _EventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(SetDataLoadedViewState, ThreadOption.BackgroundThread);
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
        public State BaseCurrentState
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
            get;
        }

        = new CardGroup();

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

        /// <summary>
        /// Gets or sets the base navigation service.
        /// </summary>
        /// <value>
        /// The base navigation service.
        /// </value>
        public INavigationService BaseNavigationService
        {
            get
            {
                return _NavigationService;
            }

            set
            {
                SetProperty(ref _NavigationService, value);
            }
        }

        /// <summary>
        /// Gets or sets the base nav parameters.
        /// </summary>
        /// <value>
        /// The base nav parameters.
        /// </value>
        public INavigationParameters BaseNavParams
        {
            get
            {
                return _NavParams;
            }

            set
            {
                SetProperty(ref _NavParams, value);
            }
        }

        /// <summary>
        /// Gets or sets the base nav parameters h link.
        /// </summary>
        /// <value>
        /// The base nav parameters h link.
        /// </value>
        public HLinkBase BaseNavParamsHLink
        {
            get
            {
                Debug.Assert(_BaseNavParamsHLink != null, "BaseNavParamsHLink is null.");

                return _BaseNavParamsHLink;
            }

            set
            {
                SetProperty(ref _BaseNavParamsHLink, value);
            }
        }

        ///// <summary>Gets or sets the base nav parameters model.</summary>
        ///// <value>The base nav parameters model.</value>
        //public object BaseNavParamsModel
        //{
        //    get
        //    {
        //        Contract.Assert(_BaseNavParamsModel != null, "BaseNavParamsModel is null.");

        // return _BaseNavParamsModel; }

        //    set
        //    {
        //        SetProperty(ref _BaseNavParamsModel, value);
        //    }
        //}

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
        private bool DetailDataLoadedFlag { get; set; }

        /// <summary>
        /// Bases the nav parameters h link default.
        /// </summary>
        /// <param name="argDefault">
        /// The argument default.
        /// </param>
        /// <returns>
        /// <br/>
        /// </returns>
        public HLinkBase BaseNavParamsHLinkDefault(HLinkBase argDefault)
        {
            if (_BaseNavParamsHLink is null)
            {
                return argDefault;
            }

            return BaseNavParamsHLink;
        }

        /// <summary>
        /// This method allows cleanup of any resources used by your View/ViewModel
        /// </summary>
        public virtual void Destroy()
        {
        }

        /// <summary>
        /// Initializes the specified parameters.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public void Initialize(INavigationParameters parameters)
        {
            Contract.Assert(parameters != null);

            // TODO See https://github.com/PrismLibrary/Prism/issues/1748

            BaseNavParams = parameters;

            parameters.TryGetValue(CommonConstants.NavigationParameterHLink, out _BaseNavParamsHLink);
            parameters.TryGetValue(CommonConstants.NavigationParameterModel, out _BaseNavParamsModel);
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
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            //TryFromJson(parameters, out localNavParams);

            //BaseCL.LogRoutineExit("Navigated from " + BaseNavParams.TargetView);
        }

        /// <summary>
        /// Called when [navigated to].
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            if (!DetailDataLoadedFlag)
            {
                DetailDataLoadedFlag = true;

                PopulateViewModel();
            }
            else
            {
                BaseCurrentState = State.None;
            }
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// Nothibg.
        /// </returns>
        public virtual void PopulateViewModel()
        {
            return;
        }

        /// <summary>
        /// Sets the state of the data loaded view.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void SetDataLoadedViewState(object value)
        {
            this.BaseCurrentState = State.None;
        }
    }
}