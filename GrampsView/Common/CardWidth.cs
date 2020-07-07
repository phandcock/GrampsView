namespace GrampsView.Common
{
    using GrampsView.Data.Repository;

    using System.ComponentModel;
    using System.Diagnostics;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class CardSizes : INotifyPropertyChanged
    {
        //Ratio of Height to width is 3 times

        private const double CardLargeHeightDefault = 420;
        private const double CardLargeWidthDefault = 420;
        private const double CardMediumHeightDefault = 300;
        private const double CardMediumWidthDefault = 300;
        private const double CardSmallHeightDefault = 270;
        private const double CardSmallWidthDefault = 270;

        private static double _CardLargeDoubleWidth = CardLargeWidthDefault;
        private static double _CardLargeHeight = CardLargeHeightDefault;
        private static double _CardLargeWidth = CardLargeWidthDefault;
        private static double _CardMediumHeight = CardMediumHeightDefault;
        private static double _CardMediumWidth = CardMediumWidthDefault;
        private static double _CardSmallHeight = CardSmallHeightDefault;
        private static double _CardSmallWidth = CardSmallWidthDefault;

        // Singleton
        private static CardSizes _current;

        public event PropertyChangedEventHandler PropertyChanged;

        public static CardSizes Current => _current ?? (_current = new CardSizes());

        public double CardLargeDoubleWidth
        {
            get
            {
                return _CardLargeDoubleWidth;
            }
        }

        public double CardLargeHeight
        {
            get
            {
                return _CardLargeHeight;
            }
        }

        public double CardLargeWidth
        {
            get
            {
                return _CardLargeWidth;
            }
        }

        public double CardMediumHeight
        {
            get
            {
                return _CardMediumHeight;
            }
        }

        public double CardMediumWidth
        {
            get
            {
                return _CardMediumWidth;
            }
        }

        public double CardSmallHeight
        {
            get
            {
                return _CardSmallHeight;
            }
        }

        public double CardSmallWidth
        {
            get
            {
                return _CardSmallWidth;
            }
        }

        public void ReCalculateCardWidths()
        {
            SetCardSmallWidth();
            SetCardMediumWidth();
            SetCardLargeWidth();
            SetCardLargeDoubleWidth();

            SetCardSmallHeight();
            SetCardMediumHeight();
            SetCardLargeHeight();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetCardLargeDoubleWidth()
        {
            double outVal = _CardLargeWidth * 2;

            // Check size
            if (outVal > DeviceDisplay.MainDisplayInfo.Width)
            {
                outVal = DeviceDisplay.MainDisplayInfo.Width;
            }

            _CardLargeDoubleWidth = outVal;

            OnPropertyChanged(nameof(CardLargeDoubleWidth));
        }

        private void SetCardLargeHeight()
        {
            double outVal;

            switch (Device.Idiom)
            {
                case TargetIdiom.Unsupported:

                case TargetIdiom.Desktop:
                    outVal = CardLargeWidth / 3;
                    break;

                case TargetIdiom.Tablet:
                    outVal = CardLargeWidth / 3;
                    break;

                case TargetIdiom.Phone:

                    outVal = CardLargeWidth / (2 * DeviceDisplay.MainDisplayInfo.Density);
                    break;

                default:
                    outVal = CardLargeWidth / 3;
                    break;
            };

            _CardLargeHeight = outVal;

            OnPropertyChanged(nameof(CardLargeHeight));
        }

        private void SetCardLargeWidth()
        {
            double outVal;

            switch (Device.Idiom)
            {
                case TargetIdiom.Unsupported:

                case TargetIdiom.Desktop:
                    outVal = 660;
                    break;

                case TargetIdiom.Tablet:
                    outVal = 540;
                    break;

                case TargetIdiom.Phone:

                    switch (DataStore.AD.CurrentOrientation)
                    {
                        case DisplayOrientation.Portrait:
                            {
                                outVal = DeviceDisplay.MainDisplayInfo.Width;
                                break;
                            }
                        case DisplayOrientation.Landscape:
                            {
                                outVal = CardLargeWidthDefault;
                                break;
                            }
                        default:
                            {
                                outVal = DeviceDisplay.MainDisplayInfo.Width;
                                break;
                            }
                    }

                    break;

                default:
                    outVal = CardLargeWidthDefault;
                    break;
            };

            // Check size
            if (outVal > DeviceDisplay.MainDisplayInfo.Width)
            {
                outVal = DeviceDisplay.MainDisplayInfo.Width;
            }

            _CardLargeWidth = outVal;

            OnPropertyChanged(nameof(CardLargeWidth));
        }

        private void SetCardMediumHeight()
        {
            double outVal;

            switch (Device.Idiom)
            {
                case TargetIdiom.Unsupported:

                case TargetIdiom.Desktop:
                    outVal = CardMediumWidth / 3;
                    break;

                case TargetIdiom.Tablet:
                    outVal = CardMediumWidth / 3;
                    break;

                case TargetIdiom.Phone:

                    outVal = CardMediumWidth / (3 * DeviceDisplay.MainDisplayInfo.Density);
                    break;

                default:
                    outVal = CardMediumWidth / 3;
                    break;
            };

            _CardMediumHeight = outVal;

            OnPropertyChanged(nameof(CardMediumHeight));
        }

        private void SetCardMediumWidth()
        {
            double outVal;

            switch (Device.Idiom)
            {
                case TargetIdiom.Unsupported:

                case TargetIdiom.Desktop:
                    outVal = 360;
                    break;

                case TargetIdiom.Tablet:
                    outVal = 360;
                    break;

                case TargetIdiom.Phone:
                    switch (DataStore.AD.CurrentOrientation)
                    {
                        case DisplayOrientation.Portrait:
                            {
                                outVal = DeviceDisplay.MainDisplayInfo.Width;
                                break;
                            }
                        case DisplayOrientation.Landscape:
                            {
                                outVal = CardMediumWidthDefault;
                                break;
                            }
                        default:
                            {
                                outVal = DeviceDisplay.MainDisplayInfo.Width;
                                break;
                            }
                    }

                    break;

                default:
                    outVal = CardMediumWidthDefault;
                    break;
            };

            // Check size
            if (outVal > DeviceDisplay.MainDisplayInfo.Width)
            {
                outVal = DeviceDisplay.MainDisplayInfo.Width;
            }

            _CardMediumWidth = outVal;

            OnPropertyChanged(nameof(CardMediumWidth));
        }

        private void SetCardSmallHeight()
        {
            double outVal;

            switch (Device.Idiom)
            {
                case TargetIdiom.Unsupported:

                case TargetIdiom.Desktop:
                    outVal = CardSmallWidth / 3;
                    break;

                case TargetIdiom.Tablet:
                    outVal = CardSmallWidth / 3;
                    break;

                case TargetIdiom.Phone:
                    outVal = CardSmallWidth / (3 * DeviceDisplay.MainDisplayInfo.Density);
                    break;

                default:
                    outVal = CardSmallWidth / 3;
                    break;
            };

            _CardSmallHeight = outVal;

            OnPropertyChanged(nameof(CardSmallHeight));
        }

        private void SetCardSmallWidth()
        {
            double outVal;

            switch (Device.Idiom)
            {
                case TargetIdiom.Unsupported:

                case TargetIdiom.Desktop:
                case TargetIdiom.Tablet:
                    outVal = CardSmallWidthDefault;
                    break;

                case TargetIdiom.Phone:
                    switch (DataStore.AD.CurrentOrientation)
                    {
                        case DisplayOrientation.Portrait:
                            {
                                outVal = DeviceDisplay.MainDisplayInfo.Width;
                                break;
                            }
                        case DisplayOrientation.Landscape:
                            {
                                outVal = CardSmallWidthDefault;
                                break;
                            }
                        default:
                            {
                                outVal = CardSmallWidthDefault;
                                break;
                            }
                    }
                    break;

                default:
                    outVal = CardSmallWidthDefault;
                    break;
            };

            // Check size
            if (outVal > DeviceDisplay.MainDisplayInfo.Width)
            {
                outVal = DeviceDisplay.MainDisplayInfo.Width;
            }

            Debug.WriteLine("Card Width changed to " + outVal.ToString(System.Globalization.CultureInfo.CurrentCulture));
            _CardSmallWidth = outVal;

            OnPropertyChanged(nameof(CardSmallWidth));
        }
    }
}