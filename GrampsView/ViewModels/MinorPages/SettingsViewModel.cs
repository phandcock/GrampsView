//-----------------------------------------------------------------------
//
// View model for the Settings ViewModel
//
// <copyright file="SettingsViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Common;

    using Prism.Events;
    using Prism.Navigation;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class SettingsViewModel : ViewModelBase
    {
        private bool _ThemeButtonDarkChecked = false;

        private bool _ThemeButtonLightChecked = false;

        private bool _ThemeButtonSystemChecked = false;

        public SettingsViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
                                                                                            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Settings";
            BaseTitleIcon = CommonConstants.IconSettings;

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
                    _ThemeButtonDarkChecked = true;
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
                    _ThemeButtonLightChecked = true;
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
                    _ThemeButtonSystemChecked = true;

                    CommonLocalSettings.ApplicationTheme = OSAppTheme.Unspecified;
                    Application.Current.UserAppTheme = OSAppTheme.Unspecified;
                }
            }
        }
    }
}