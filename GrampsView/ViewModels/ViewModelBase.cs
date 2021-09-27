namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

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
        private string _BaseParamsHLink;
        private string _BaseTitle;

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
        public Group<object> BaseDetail
        {
            get; set;
        }

        = new Group<object>();

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

        public IModelBase BaseModelBase { get; set; } = new ModelBase();

        public string BaseParamsHLink
        {
            get

            {
                return _BaseParamsHLink;
            }

            set
            {
                SetProperty(ref _BaseParamsHLink, value);

                if (!string.IsNullOrEmpty(value))
                {
                    HandleBaseParamLinkChange();
                }
            }
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
            get
            {
                if (!string.IsNullOrEmpty(_BaseTitle))
                {
                    return _BaseTitle;
                }

                if (BaseModelBase.Valid)
                {
                    return BaseModelBase.DefaultTextShort;
                }

                return string.Empty;
            }
            set
            {
                SetProperty(ref _BaseTitle, value);
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
            get; set;
        }

        public bool TopMenuHubButtonVisible
        {
            get; private set;
        } = false;

        public Command TopMenuHubCommand
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
        }

        /// <summary>
        /// Handle the appearing event. Designed to be overridden at the modelview level.
        /// </summary>
        public virtual void HandleViewAppearingEvent()
        {
            return;
        }

        /// <summary>
        /// Handle the view loaded event. Designed to be overridden at the modelview level.
        /// </summary>
        public virtual void HandleViewDataLoadEvent()
        {
            return;
        }

        public void TopMenuHubCommandHandler()
        {
            CommonRoutines.NavigateHub();
        }

        public async Task TopMenuNoteCommandHandler()
        {
            string body = string.Empty;

            List<string> recipients = new List<string>
            {
                CommonLocalSettings.NoteEmailAddress
            };

            EmailMessage message = new EmailMessage
            {
                Subject = $"GrampsView Note for ({BaseModelBase.Id}) - {BaseModelBase.ToString()}",
                Body = body,
                To = recipients,
                //Cc = ccRecipients,
                //Bcc = bccRecipients
            };
            await Email.ComposeAsync(message);
        }

        internal void BaseHandleViewAppearingEventInternal()
        {
            HandleViewAppearingEvent();
        }

        private void HandleBaseParamLinkChange()
        {
            //if (BaseHandleLoadTriggered == false)
            //{
            //    BaseHandleLoadTriggered = true;

            HandleViewDataLoadEvent();
            //}
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

                BaseTitle = BaseTitle.Substring(0, BaseTitle.Length > 50 ? 50 : BaseTitle.Length);
            }
        }

        private void ViewSetup()
        {
            TopMenuHubCommand = new Command(TopMenuHubCommandHandler);

            // As UWP does not support shell swipes for desktop. TODO Fix this when we can
            if (Device.RuntimePlatform == Device.UWP)
            {
                TopMenuHubButtonVisible = true;
            }

            TopMenuNoteCommand = new AsyncCommand(TopMenuNoteCommandHandler);
        }
    }
}