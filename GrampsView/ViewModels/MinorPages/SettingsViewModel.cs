// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;

using SharedSharp;

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

            DisplayMessageLogButtonCommand = new AsyncRelayCommand(DisplayMessageLogButtonCommandHandler);

            UpdateNoteEmailCommand = new Command<string>(UpdateNoteEmailHandler);

            // HandleViewAppearingEvent();
        }

        public IAsyncRelayCommand DisplayMessageLogButtonCommand
        {
            get;
        }

        //public IAsyncRelayCommand ShowMessageLogCommand
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
                            SharedSharpSettings.ApplicationTheme = AppTheme.Dark;
                            Application.Current.UserAppTheme = AppTheme.Dark;

                            break;
                        }
                    case "Light":
                        {
                            SharedSharpSettings.ApplicationTheme = AppTheme.Light;
                            Application.Current.UserAppTheme = AppTheme.Light;

                            break;
                        }
                    case "System":
                        {
                            SharedSharpSettings.ApplicationTheme = AppTheme.Unspecified;
                            Application.Current.UserAppTheme = AppTheme.Unspecified;

                            break;
                        }

                    default:
                        {
                            SharedSharpSettings.ApplicationTheme = AppTheme.Unspecified;
                            Application.Current.UserAppTheme = AppTheme.Unspecified;

                            break;
                        }
                }
            }
        }

        public IAsyncRelayCommand UCNavigateCommand
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
            SharedSharpNavigation.NavigateAsyncNS(new SharedSharp.Views.SharedSharpMessageLogPage());
            return Task.CompletedTask;
        }

        private void UpdateNoteEmailHandler(string argEmailAddress)
        {
            CommonLocalSettings.NoteEmailAddress = argEmailAddress;

            return;
        }
    }
}