using GrampsView.Common;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels;

using SharedSharp.ViewModels;

using System.ComponentModel;
using System.Diagnostics;

namespace GrampsView.ViewModels
{
    public class ViewModelBase : SharedSharpViewModelBase, INotifyPropertyChanged
    {
        private string _BaseTitle = string.Empty;

        /// <summary>Initializes a new instance of the <see cref="ViewModelBase" /> class.</summary>
        /// <param name="iocCommonLogging">The ioc common logging.</param>

        public ViewModelBase(ILog iocCommonLogging)
        {
            BaseCL = iocCommonLogging;

            TopMenuHubCommand = new Command(TopMenuHubCommandHandler);
            TopMenuNoteCommand = new AsyncRelayCommand(TopMenuNoteCommandHandler);

            ViewSetup();
        }

        public ViewModelBase()
        {
            TopMenuHubCommand = new Command(TopMenuHubCommandHandler);
            TopMenuNoteCommand = new AsyncRelayCommand(TopMenuNoteCommandHandler);

            ViewSetup();
        }

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

        public IModelBase BaseModelBase { get; set; } = new ModelBase();

        /// <summary>
        /// Gets or sets the base title.
        /// </summary>
        /// <value>
        /// The base title.
        /// </value>
        public override string BaseTitle
        {
            get => !string.IsNullOrEmpty(_BaseTitle) ? _BaseTitle : BaseModelBase.Valid ? BaseModelBase.DefaultTextShort : string.Empty;
            set => SetProperty(ref _BaseTitle, value);
        }

        public bool TopMenuHubButtonVisible
        {
            get; private set;
        } = false;

        public Command TopMenuHubCommand
        {
            get; private set;
        }

        public IAsyncRelayCommand TopMenuNoteCommand
        {
            get; private set;
        }

        public override void HandleViewModelParameters()
        {
            foreach (KeyValuePair<string, object> item in BasePassedArguments)
            {
                Debug.WriteLine($"BasePassedArguments - {item.Key}: {item.Value}");
            }

            if (BasePassedArguments.Count > 0)
            {
                //  WhatsNewText = (string)BasePassedArguments[SharedSharpConstants.ShellParameter1];
            }
        }

        public void TopMenuHubCommandHandler()
        {
            SharedSharpNavigation.NavigateHub();
        }

        public async Task TopMenuNoteCommandHandler()
        {
            string body = string.Empty;

            List<string> recipients = new()
            {
                CommonLocalSettings.NoteEmailAddress
            };

            EmailMessage message = new()
            {
                Subject = $"GrampsView Note for ({BaseModelBase.Id}) - {BaseModelBase}",
                Body = body,
                To = recipients,
                //Cc = ccRecipients,
                //Bcc = bccRecipients
            };
            await Email.ComposeAsync(message);
        }

        public void ViewSetup()
        {
            // As UWP does not support shell swipes for desktop. TODO Fix this when we can
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                TopMenuHubButtonVisible = true;
            }
        }

        /// <summary>
        /// Called when [basecl changed]. Frody automatically wires this up.
        /// </summary>
        private void OnBaseCLChanged()
        {
            Debug.Assert(BaseCL != null, "BaseCL is null.  Was this set in the constructor for the derived class?");
        }

        /// <summary>
        /// Called when [basetitlechanged]. Frody automatically wires this up.
        /// </summary>
        private void OnBaseTitleChanged()
        {
            if (!(BaseTitle == null))
            {
                BaseTitle = CommonRoutines.ReplaceLineSeperators(BaseTitle);

                BaseTitle = BaseTitle[..(BaseTitle.Length > 50 ? 50 : BaseTitle.Length)];
            }
        }
    }
}