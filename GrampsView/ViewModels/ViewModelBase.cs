namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Prism.Events;

    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.CommunityToolkit.UI.Views;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    [QueryProperty(nameof(BaseParamsHLink), nameof(BaseParamsHLink))]
    [QueryProperty(nameof(BaseParamsModel), nameof(BaseParamsModel))]
    public class ViewModelBase : ObservableObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        public ViewModelBase()
        {
            ViewSetup();
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
        public ViewModelBase(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
        {
            BaseCL = iocCommonLogging;
            BaseEventAggregator = iocEventAggregator;

            ViewSetup();
        }

        // TODO Checkout Xamarin.Forms.Mocks
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        public ViewModelBase(ICommonLogging iocCommonLogging)
        {
            BaseCL = iocCommonLogging;

            ViewSetup();
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

        public bool TopMenuHubButtonVisible
        {
            get; private set;
        } = false;

        public IAsyncCommand TopMenuHubCommand
        {
            get; private set;
        }

        public IAsyncCommand TopMenuNoteCommand
        {
            get; private set;
        }

        private bool BaseHandleLoadTriggered
        {
            get; set;
        } = false;

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

        public virtual void BaseHandleLoadEvent()
        {
            return;
        }

        public async Task TopMenuHubCommandHandler()
        {
            CommonRoutines.NavigateHub();
        }

        public async Task TopMenuNoteCommandHandler()
        {
            string body = string.Empty;
            List<string> recipients = new List<string>();

            EmailMessage message = new EmailMessage
            {
                Subject = "GrampsView: " + Shell.Current.Navigation.NavigationStack[Shell.Current.Navigation.NavigationStack.Count - 1].Title,
                Body = body,
                To = recipients,
                //Cc = ccRecipients,
                //Bcc = bccRecipients
            };
            await Email.ComposeAsync(message);
        }

        internal void BaseHandleAppearingEventInternal()
        {
            if (BaseHandleLoadTriggered == false)
            {
                BaseHandleLoadTriggered = true;

                BaseHandleLoadEvent();
            }

            BaseHandleAppearingEvent();

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

        // TODO work out how to add these to Frody
        private void OnBaseCLChanged()
        {
            Debug.Assert(BaseCL != null, "BaseCL is null.  Was this set in the constructor for the derived class?");
        }

        private void OnBaseEventAggregatorChanged()
        {
            Debug.Assert(BaseEventAggregator != null, "BaseEventAggregator is null.  Was this set in the constructor for the derived class?");
        }

        private void OnBaseTitleChanged()
        {
            if (!(BaseTitle == null))
            {
                BaseTitle = CommonRoutines.ReplaceLineSeperators(BaseTitle);

                BaseTitle.Substring(0, BaseTitle.Length > 50 ? 50 : BaseTitle.Length);
            }
        }

        private void ViewSetup()
        {
            TopMenuHubCommand = new AsyncCommand(TopMenuHubCommandHandler);

            // As UWP does not support shell swipes. TODO Fix this when we can
            if (Device.RuntimePlatform == Device.UWP)
            {
                TopMenuHubButtonVisible = true;
            }

            TopMenuNoteCommand = new AsyncCommand(TopMenuNoteCommandHandler);
        }
    }
}