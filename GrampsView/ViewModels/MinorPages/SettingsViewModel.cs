namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;
    using GrampsView.Views;

    using Microsoft.AppCenter.Distribute;

    using Prism.Commands;
    using Prism.Events;

    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.Extensions;
    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Forms;

    public class SettingsViewModel : ViewModelBase
    {
        //private bool _LocalCanForceUpdate = true;

        //private bool _TestButton = true;

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

            BaseHandleAppearingEvent();
        }

        public bool CanHandleTestButton
        {
            get; set;
        }

        = true;

        public DelegateCommand ForceUpdateCheckCommand
        {
            get; private set;
        }

        public bool LocalCanForceUpdate
        {
            get; set;
        }

        = true;

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

        public DelegateCommand TestCommand
        {
            get; private set;
        }

        //public IHLinkMediaModel TestMedia
        //{
        //    get
        //    {
        //        var t = DataStore.Instance.DS.MediaData.GetRandomItem().HLink;

        //        return t;
        //    }
        //}

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

        public IAsyncCommand UCNavigateCommand
        {
            get; private set;
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

        public override void BaseHandleAppearingEvent()
        {
            switch (CommonLocalSettings.ApplicationTheme)
            {
                case OSAppTheme.Light:
                    {
                        ThemeButtonLightChecked = true;
                        ThemeButtonDarkChecked = false;
                        ThemeButtonSystemChecked = false;
                        break;
                    }

                case OSAppTheme.Dark:
                    {
                        ThemeButtonDarkChecked = true;
                        ThemeButtonLightChecked = false;
                        ThemeButtonSystemChecked = false;
                        break;
                    }

                default:
                    {
                        ThemeButtonDarkChecked = false;
                        ThemeButtonLightChecked = false;
                        ThemeButtonSystemChecked = true;
                        break;
                    }
            }
        }

        public void ForceUpdate()
        {
            Distribute.CheckForUpdate();
        }

        public void TestButtonHandler()
        {
            ErrorInfo t = new ErrorInfo
            {
                Name = "Test Alert",
                Text = "Test Alert with detail and even more detail and more and more and more",
            };

            t.Add("Test Line 1", "Test Value 1");
            t.Add("Test LIne 2", "Test Value 2");

            DataStore.Instance.CN.NotifyAlert("Test Alert", t);

            t = new ErrorInfo
            {
                Name = "Test Error",
                Text = "Test Error with detail and even more detail and more and more and more",
            };

            t.Add("Test Line 1", "Test Value 1");
            t.Add("Test LIne 2", "Test Value 2");

            DataStore.Instance.CN.NotifyError(t);

            t = new ErrorInfo
            {
                Name = "Test Exception",
                Text = "Test Exception with detail and even more detail and more and more and more",
            };

            t.Add("Test Line 1", "Test Value 1");
            t.Add("Test LIne 2", "Test Value 2");

            DataStore.Instance.CN.NotifyException("Test Exception", new System.Exception(), t);
        }

        private async Task UCNavigate()
        {
            await Application.Current.MainPage.Navigation.ShowPopupAsync(new MessageLogPopup());
            return;
        }
    }
}