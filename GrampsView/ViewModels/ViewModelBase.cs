namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Prism.Events;

    using System.ComponentModel;
    using System.Diagnostics;

    using Xamarin.CommunityToolkit.UI.Views;
    using Xamarin.Forms;

    [QueryProperty(nameof(BaseParamsHLink), nameof(BaseParamsHLink))]
    [QueryProperty(nameof(BaseParamsModel), nameof(BaseParamsModel))]
    public class ViewModelBase : CommonBindableBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Backing store for the base current state
        /// </summary>
        private ICommonLogging _CL = new CommonLogging();

        /// <summary>
        /// Backing store for the base current state
        /// </summary>
        private IEventAggregator _EventAggregator = new EventAggregator();

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

            //_EventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(BaseHandleDataLoadedEventInternal, ThreadOption.UIThread);
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
            get; set;
        }

        /// <summary>
        /// Gets or sets the state of the base current.
        /// </summary>
        /// <value>
        /// The state of the base current.
        /// </value>
        public LayoutState BaseCurrentLayoutState
        {
            get; set;
        }

        = LayoutState.None;

        /// <summary>
        /// Gets the base detail.
        /// </summary>
        /// <value>
        /// The base detail.
        /// </value>
        public CardGroup BaseDetail
        {
            get; set;
        }

        = new CardGroup();

        /// <summary>
        /// Gets or sets the base event aggregator.
        /// </summary>
        /// <value>
        /// The base event aggregator.
        /// </value>
        public IEventAggregator BaseEventAggregator
        {
            get; set;
        }

        public string BaseParamsHLink
        {
            get; set;
        }

        public string BaseParamsModel
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the base title.
        /// </summary>
        /// <value>
        /// The base title.
        /// </value>
        public string BaseTitle
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the base title icon.
        /// </summary>
        /// <value>
        /// The base title icon.
        /// </value>
        public string BaseTitleIcon
        {
            get; set;
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

        public virtual void BaseHandleDisAppearingEvent()
        {
            return;
        }

        internal void BaseHandleAppearingEventInternal()
        {
            if (!DetailDataLoadedFlag)
            {
                BaseCurrentLayoutState = LayoutState.Loading;

                BaseHandleAppearingEvent();

                DetailDataLoadedFlag = true;
            }
            else
            {
                BaseCurrentLayoutState = LayoutState.None;
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

        internal void BaseHandleDisAppearingEventInternal()
        {
            BaseHandleDisAppearingEvent();
        }

        private void OnBaseCLChanged()
        {
            Debug.Assert(_CL != null, "BaseCL is null.  Was this set in the constructor for the derived class?");
        }

        private void OnBaseEventAggregatorChanged()
        {
            Debug.Assert(_EventAggregator != null, "BaseEventAggregator is null.  Was this set in the constructor for the derived class?");
        }

        private void OnBaseTitleChanged()
        {
            if (!(BaseTitle == null))
            {
                BaseTitle = CommonRoutines.ReplaceLineSeperators(BaseTitle);

                BaseTitle.Substring(0, BaseTitle.Length > 50 ? 50 : BaseTitle.Length);
            }
        }
    }
}