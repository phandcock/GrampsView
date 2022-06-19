namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.Messaging;

    using SharedSharp.CommonRoutines;
    using SharedSharp.Errors;
    using SharedSharp.Logging;

    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Forms;

    public class SettingsViewModel : ViewModelBase
    {
        private string _ThemeButtonChecked = string.Empty;

        public SettingsViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
                                    : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Settings";
            BaseTitleIcon = CommonConstants.IconSettings;

            TestButtonCommand = new AsyncCommand(TestButtonHandler);

            UpdateNoteEmailCommand = new Command<string>(UpdateNoteEmailHandler);

            // HandleViewAppearingEvent();
        }

        public IAsyncCommand ShowMessageLogCommand
        {
            get;
        }

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

        public IAsyncCommand TestButtonCommand
        {
            get;
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
                            LocalSettings.ApplicationTheme = OSAppTheme.Dark;
                            Application.Current.UserAppTheme = OSAppTheme.Dark;

                            break;
                        }
                    case "Light":
                        {
                            LocalSettings.ApplicationTheme = OSAppTheme.Light;
                            Application.Current.UserAppTheme = OSAppTheme.Light;

                            break;
                        }
                    case "System":
                        {
                            LocalSettings.ApplicationTheme = OSAppTheme.Unspecified;
                            Application.Current.UserAppTheme = OSAppTheme.Unspecified;

                            break;
                        }

                    default:
                        {
                            LocalSettings.ApplicationTheme = OSAppTheme.Unspecified;
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

        private async Task TestButtonHandler()
        {
            ErrorInfo t = new ErrorInfo
            {
                ErrorArea = "Test Alert",
                ErrorMessage = "Test Alert with detail and even more detail and more and more and more",
            };

            t.Add("Test Line 1", "Test Value 1");
            t.Add("Test LIne 2", "Test Value 2");

            App.Current.Services.GetService<IErrorNotifications>().NotifyAlert("Test Alert", t);

            t = new ErrorInfo
            {
                ErrorArea = "Test Error",
                ErrorMessage = "Test Error with detail and even more detail and more and more and more",
            };

            t.Add("Test Line 1", "Test Value 1");
            t.Add("Test LIne 2", "Test Value 2");

            App.Current.Services.GetService<IErrorNotifications>().NotifyError(t);

            t = new ErrorInfo
            {
                ErrorArea = "Test Exception",
                ErrorMessage = "Test Exception with detail and even more detail and more and more and more",
            };

            t.Add("Test Line 1", "Test Value 1");
            t.Add("Test LIne 2", "Test Value 2");

            App.Current.Services.GetService<IErrorNotifications>().NotifyException("Test Exception", new System.Exception(), t);
        }

        private void UpdateNoteEmailHandler(string argEmailAddress)
        {
            CommonLocalSettings.NoteEmailAddress = argEmailAddress;

            return;
        }
    }
}