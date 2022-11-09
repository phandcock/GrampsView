using CommunityToolkit.Mvvm.Messaging;

using GrampsView.Common;

using Microsoft.Extensions.DependencyInjection;

using SharedSharp.Logging.Interfaces;

using System.Threading.Tasks;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace GrampsView.ViewModels.MinorPages
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _ThemeButtonChecked = string.Empty;

        public SettingsViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging, IMessenger iocEventAggregator)
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
            get => CommonLocalSettings.SortHLinkCollections;
            set => CommonLocalSettings.SortHLinkCollections = value;
        }

        public string ThemeButtonChecked
        {
            get => _ThemeButtonChecked;

            set
            {
                _ = SetProperty(ref _ThemeButtonChecked, value);

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
            get => CommonLocalSettings.NoteEmailAddress;

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
            get => CommonLocalSettings.UseFirstImageFlag;
            set => CommonLocalSettings.UseFirstImageFlag = value;
        }

        private Task DisplayMessageLogButtonCommandHandler()
        {
            App.Current.Services.GetService<ILog>().DataLogShow();
            return Task.CompletedTask;
        }

        private void UpdateNoteEmailHandler(string argEmailAddress)
        {
            CommonLocalSettings.NoteEmailAddress = argEmailAddress;

            return;
        }
    }
}