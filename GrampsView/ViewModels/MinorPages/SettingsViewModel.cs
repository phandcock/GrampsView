namespace GrampsView.ViewModels
{
    using CommunityToolkit.Mvvm.Messaging;

    using GrampsView.Common;

    using Microsoft.Extensions.DependencyInjection;

    using SharedSharp.Errors;
    using SharedSharp.Logging;

    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Forms;

    public class SettingsViewModel : ViewModelBase
    {
        private string _ThemeButtonChecked = string.Empty;

        public SettingsViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
                                    : base(iocCommonLogging)
        {
            BaseTitle = "Settings";
            BaseTitleIcon = Constants.IconSettings;

            DisplayMessageLogButtonCommand = new AsyncCommand(DisplayMessageLogButtonCommandHandler);

            UpdateNoteEmailCommand = new Command<string>(UpdateNoteEmailHandler);

            // HandleViewAppearingEvent();
        }

        public IAsyncCommand DisplayMessageLogButtonCommand
        {
            get;
        }

        //public IAsyncCommand ShowMessageLogCommand
        //{
        //    get;
        //}

        public bool SortCollectionsFlag
        {
            get
            {
                return CommonLocalSettings.SortHLinkCollections;
            }
            set
            {
                CommonLocalSettings.SortHLinkCollections = value;
            }
        }

        public string ThemeButtonChecked
        {
            get
            {
                return _ThemeButtonChecked;
            }

            set
            {
                SetProperty(ref _ThemeButtonChecked, value);

                switch (_ThemeButtonChecked)
                {
                    case "Dark":
                        {
                            SharedSharp.Common.SharedSharpSettings.ApplicationTheme = OSAppTheme.Dark;
                            Application.Current.UserAppTheme = OSAppTheme.Dark;

                            break;
                        }
                    case "Light":
                        {
                            SharedSharp.Common.SharedSharpSettings.ApplicationTheme = OSAppTheme.Light;
                            Application.Current.UserAppTheme = OSAppTheme.Light;

                            break;
                        }
                    case "System":
                        {
                            SharedSharp.Common.SharedSharpSettings.ApplicationTheme = OSAppTheme.Unspecified;
                            Application.Current.UserAppTheme = OSAppTheme.Unspecified;

                            break;
                        }

                    default:
                        {
                            SharedSharp.Common.SharedSharpSettings.ApplicationTheme = OSAppTheme.Unspecified;
                            Application.Current.UserAppTheme = OSAppTheme.Unspecified;

                            break;
                        }
                }
            }
        }

        public IAsyncCommand UCNavigateCommand
        {
            get; private set;
        }

        public string UpdateNoteEmailAddress
        {
            get
            {
                return CommonLocalSettings.NoteEmailAddress;
            }

            set
            {
                if (UpdateNoteEmailValidValue)
                {
                    CommonLocalSettings.NoteEmailAddress = value;
                }
            }
        }

        public Command<string> UpdateNoteEmailCommand
        {
            get;
        }

        public bool UpdateNoteEmailValidValue
        {
            get; set;
        }

        public bool UseFirstImageFlag
        {
            get
            {
                return CommonLocalSettings.UseFirstImageFlag;
            }
            set
            {
                CommonLocalSettings.UseFirstImageFlag = value;
            }
        }

        private async Task DisplayMessageLogButtonCommandHandler()
        {
            App.Current.Services.GetService<IErrorNotifications>().DataLogShow();
        }

        private void UpdateNoteEmailHandler(string argEmailAddress)
        {
            CommonLocalSettings.NoteEmailAddress = argEmailAddress;

            return;
        }
    }
}