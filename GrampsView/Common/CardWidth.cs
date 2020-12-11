namespace GrampsView.Common
{
    using GrampsView.Data.Repository;

    using System.ComponentModel;
    using System.Diagnostics;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class CardSizes : CommonBindableBase
    {
        // Ratio of Height to width is 3 times

        private const double CardLargeHeightDefault = 420;
        private const double CardLargeWidthDefault = 420;
        private const double CardMediumHeightDefault = 300;
        private const double CardMediumWidthDefault = 300;
        private const double CardSingleHeightDefault = 180;
        private const double CardSingleWidthDefault = 270;
        private const double CardSmallHeightDefault = 270;
        private const double CardSmallWidthDefault = 270;

        private static double _CardLargeDoubleWidth = CardLargeWidthDefault;
        private static double _CardLargeHeight = CardLargeHeightDefault;
        private static double _CardLargeWidth = CardLargeWidthDefault;
        private static double _CardMediumHeight = CardMediumHeightDefault;
        private static double _CardMediumWidth = CardMediumWidthDefault;
        private static double _CardSingleHeight = CardSingleHeightDefault;
        private static double _CardSingleWidth = CardSingleWidthDefault;
        private static double _CardSmallHeight = CardSmallHeightDefault;
        private static double _CardSmallWidth = CardSmallWidthDefault;

        // Singleton
        private static CardSizes _current;

        //public event PropertyChangedEventHandler PropertyChanged;

        public static CardSizes Current => _current ?? (_current = new CardSizes());

        public double CardLargeDoubleWidth
        {
            get
            {
                return _CardLargeDoubleWidth;
            }

            set
            {
                SetProperty(ref _CardLargeDoubleWidth, value);
            }
        }

        public double CardLargeHeight
        {
            get
            {
                return _CardLargeHeight;
            }

            set
            {
                SetProperty(ref _CardLargeHeight, value);
            }
        }

        public double CardLargeWidth
        {
            get
            {
                return _CardLargeWidth;
            }

            set
            {
                SetProperty(ref _CardLargeWidth, value);
            }
        }

        public double CardMediumHeight
        {
            get
            {
                return _CardMediumHeight;
            }

            set
            {
                SetProperty(ref _CardMediumHeight, value);
            }
        }

        public double CardMediumWidth
        {
            get
            {
                return _CardMediumWidth;
            }

            set
            {
                SetProperty(ref _CardMediumWidth, value);
            }
        }

        public double CardSingleHeight
        {
            get
            {
                return _CardSingleHeight;
            }

            set
            {
                SetProperty(ref _CardSingleHeight, value);
            }
        }

        public double CardSingleWidth
        {
            get
            {
                return _CardSingleWidth;
            }

            set
            {
                SetProperty(ref _CardSingleWidth, value);
            }
        }

        public double CardSmallHeight
        {
            get
            {
                return _CardSmallHeight;
            }

            set
            {
                SetProperty(ref _CardSmallHeight, value);
            }
        }

        public double CardSmallWidth
        {
            get
            {
                return _CardSmallWidth;
            }

            set
            {
                SetProperty(ref _CardSmallWidth, value);
            }
        }

        //public double MediaDetailImageHeight
        //{
        //    get
        //    {
        //        double outVal;

        // switch (Device.Idiom) { case TargetIdiom.Unsupported:

        // case TargetIdiom.Desktop: outVal = CardLargeHeight * 3; break;

        // case TargetIdiom.Tablet: outVal = CardLargeHeight; break;

        // case TargetIdiom.Phone:

        // outVal = CardLargeHeight; break;

        // default: outVal = CardLargeHeight; break; };

        //        return outVal;
        //    }
        //}

        //public double MediaDetailImageWidth
        //{
        //    get
        //    {
        //        double outVal;

        // switch (Device.Idiom) { case TargetIdiom.Unsupported:

        // case TargetIdiom.Desktop: outVal = CardLargeWidth * 3; break;

        // case TargetIdiom.Tablet: outVal = CardLargeWidth; break;

        // case TargetIdiom.Phone:

        // outVal = CardLargeWidth;

        // break;

        // default: outVal = CardLargeWidth; break; };

        // // Check size if (outVal > DeviceDisplay.MainDisplayInfo.Width) { outVal =
        // DeviceDisplay.MainDisplayInfo.Width; }

        //        return outVal;
        //    }
        //}

        public void ReCalculateCardWidths()
        {
            SetCardSingleWidth();
            SetCardSmallWidth();
            SetCardMediumWidth();
            SetCardLargeWidth();
            SetCardLargeDoubleWidth();

            SetCardSingleHeight();
            SetCardSmallHeight();
            SetCardMediumHeight();
            SetCardLargeHeight();
        }

        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged == null)
        //    {
        //        return;
        //    }

        //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}

        private void SetCardLargeDoubleWidth()
        {
            double outVal = _CardLargeWidth * 2;

            // Check size
            if (outVal > DeviceDisplay.MainDisplayInfo.Width)
            {
                outVal = DeviceDisplay.MainDisplayInfo.Width;
            }

            CardLargeDoubleWidth = outVal;
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

            CardLargeHeight = outVal;
        }

        private void SetCardLargeWidth()
        {
            double outVal;

            switch (Device.Idiom)
            {
                case TargetIdiom.Unsupported:

                case TargetIdiom.Desktop:
                    outVal = 540;
                    break;

                case TargetIdiom.Tablet:
                    outVal = 540;
                    break;

                case TargetIdiom.Phone:

                    switch (DataStore.Instance.AD.CurrentOrientation)
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

            CardLargeWidth = outVal;
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

            CardMediumHeight = outVal;
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
                    switch (DataStore.Instance.AD.CurrentOrientation)
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

            CardMediumWidth = outVal;
        }

        private void SetCardSingleHeight()
        {
            double outVal;

            switch (Device.Idiom)
            {
                case TargetIdiom.Unsupported:

                case TargetIdiom.Desktop:
                    outVal = CardSingleWidth / 6;
                    break;

                case TargetIdiom.Tablet:
                    outVal = CardSingleWidth / 6;
                    break;

                case TargetIdiom.Phone:
                    outVal = CardSingleWidth / (6 * DeviceDisplay.MainDisplayInfo.Density);
                    break;

                default:
                    outVal = CardSingleWidth / 6;
                    break;
            };

            CardSingleHeight = outVal;
        }

        private void SetCardSingleWidth()
        {
            double outVal;

            switch (Device.Idiom)
            {
                case TargetIdiom.Unsupported:

                case TargetIdiom.Desktop:
                case TargetIdiom.Tablet:
                    outVal = CardSingleWidthDefault;
                    break;

                case TargetIdiom.Phone:
                    switch (DataStore.Instance.AD.CurrentOrientation)
                    {
                        case DisplayOrientation.Portrait:
                            {
                                outVal = DeviceDisplay.MainDisplayInfo.Width;
                                break;
                            }
                        case DisplayOrientation.Landscape:
                            {
                                outVal = CardSingleWidthDefault;
                                break;
                            }
                        default:
                            {
                                outVal = CardSingleWidthDefault;
                                break;
                            }
                    }
                    break;

                default:
                    outVal = CardSingleWidthDefault;
                    break;
            };

            // Check size
            if (outVal > DeviceDisplay.MainDisplayInfo.Width)
            {
                outVal = DeviceDisplay.MainDisplayInfo.Width;
            }

            Debug.WriteLine("Card Single Width changed to " + outVal.ToString(System.Globalization.CultureInfo.CurrentCulture));
            CardSingleWidth = outVal;
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

            CardSmallHeight = outVal;
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
                    switch (DataStore.Instance.AD.CurrentOrientation)
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
            CardSmallWidth = outVal;
        }
    }
}