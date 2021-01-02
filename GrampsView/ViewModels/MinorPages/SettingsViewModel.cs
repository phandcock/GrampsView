namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;
    using GrampsView.Views;

    using Microsoft.AppCenter.Distribute;

    using Prism.Commands;
    using Prism.Events;

    using System.Windows.Input;

    using Xamarin.Forms;

    public class SettingsViewModel : ViewModelBase
    {
        private bool _LocalCanForceUpdate = true;

        private bool _TestButton = true;

        private bool _ThemeButtonDarkChecked;

        private bool _ThemeButtonLightChecked;

        private bool _ThemeButtonSystemChecked;

        public SettingsViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
                                : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Settings";
            BaseTitleIcon = CommonConstants.IconSettings;

            TestCommand = new DelegateCommand(TestButtonHandler).ObservesCanExecute(() => CanHandleTestButton);

            ForceUpdateCheckCommand = new DelegateCommand(ForceUpdate).ObservesCanExecute(() => LocalCanForceUpdate);

            UCNavigateCommand = new DelegateCommand(UCNavigate);

            PopulateViewModel();
        }

        public bool CanHandleTestButton
        {
            get { return _TestButton; }
            set { SetProperty(ref _TestButton, value); }
        }

        public DelegateCommand ForceUpdateCheckCommand { get; private set; }

        public bool LocalCanForceUpdate
        {
            get { return _LocalCanForceUpdate; }
            set { SetProperty(ref _LocalCanForceUpdate, value); }
        }

        public DelegateCommand TestCommand { get; private set; }

        public bool ThemeButtonDarkChecked
        {
            get
            {
                return _ThemeButtonDarkChecked;
            }

            set
            {
                if (_ThemeButtonDarkChecked != value)
                {
                    _ThemeButtonLightChecked = false;
                    SetProperty(ref _ThemeButtonDarkChecked, true);
                    _ThemeButtonSystemChecked = false;

                    CommonLocalSettings.ApplicationTheme = OSAppTheme.Dark;
                    Application.Current.UserAppTheme = OSAppTheme.Dark;
                }
            }
        }

        public bool ThemeButtonLightChecked
        {
            get
            {
                return _ThemeButtonLightChecked;
            }

            set
            {
                if (_ThemeButtonLightChecked != value)
                {
                    SetProperty(ref _ThemeButtonLightChecked, true);
                    _ThemeButtonDarkChecked = false;
                    _ThemeButtonSystemChecked = false;

                    CommonLocalSettings.ApplicationTheme = OSAppTheme.Light;
                    Application.Current.UserAppTheme = OSAppTheme.Light;
                }
            }
        }

        public bool ThemeButtonSystemChecked
        {
            get
            {
                return _ThemeButtonSystemChecked;
            }

            set
            {
                if (_ThemeButtonSystemChecked != value)
                {
                    _ThemeButtonLightChecked = false;
                    _ThemeButtonDarkChecked = false;
                    SetProperty(ref _ThemeButtonSystemChecked, true);

                    CommonLocalSettings.ApplicationTheme = OSAppTheme.Unspecified;
                    Application.Current.UserAppTheme = OSAppTheme.Unspecified;
                }
            }
        }

        public ICommand UCNavigateCommand { get; private set; }

        public void ForceUpdate()
        {
            Distribute.CheckForUpdate();
        }

        public override void PopulateViewModel()
        {
            switch (CommonLocalSettings.ApplicationTheme)
            {
                case OSAppTheme.Light:
                    {
                        ThemeButtonLightChecked = true;
                        break;
                    }

                case OSAppTheme.Dark:
                    {
                        ThemeButtonDarkChecked = true;
                        break;
                    }

                default:
                    {
                        ThemeButtonSystemChecked = true;
                        break;
                    }
            }
        }

        public void TestButtonHandler()
        {
            DataStore.Instance.CN.NotifyAlert("Test Alert");

            ActionDialogArgs t = new ActionDialogArgs
            {
                Name = "Test Error",
                Text = "Test Error with detail and even more detail and more and more and more",
            };

            t.Add("Test Line 1", "Test Value 1");
            t.Add("Test LIne 2", "Test Value 2");

            DataStore.Instance.CN.NotifyDialogBox(t);
        }

        private async void UCNavigate()
        {
            await CommonRoutines.NavigateAsync(nameof(MessageLogPage));
        }
    }
}