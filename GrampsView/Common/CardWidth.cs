namespace GrampsView.Common
{
    using GrampsView.Data.Repository;

    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class CardSizes : CommonBindableBase, INotifyPropertyChanged
    {
        // Ratio of Height to width is 3 times

        private const double CardLargeHeightDefault = 420;
        private const double CardLargeWidthDefault = 420;
        private const double CardMediumHeightDefault = 300;
        private const double CardMediumWidthDefault = 300;
        private const double CardSingleHeightDefault = 20;
        private const double CardSingleWidthDefault = 270;
        private const double CardSmallHeightDefault = 270;
        private const double CardSmallWidthDefault = 270;

        // Singleton
        private static CardSizes _current;

        private double ScaledHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
        private double ScaledWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
        public static CardSizes Current => _current ?? (_current = new CardSizes());

        public double CardLargeDoubleWidth
        {
            get; set;
        }

        = CardLargeWidthDefault;

        public double CardLargeHeight
        {
            get; set;
        }

        = CardLargeHeightDefault;

        public double CardLargeWidth
        {
            get; set;
        }

        = CardLargeWidthDefault;

        public double CardMediumHeight
        {
            get; set;
        }

        = CardMediumHeightDefault;

        public double CardMediumWidth
        {
            get; set;
        }

        = CardMediumWidthDefault;

        public double CardSingleHeight
        {
            get; set;
        }

        = CardSingleHeightDefault;

        public double CardSingleWidth
        {
            get; set;
        }

        = CardSingleWidthDefault;

        public double CardSmallHeight
        {
            get; set;
        }

        = CardSmallHeightDefault;

        public double CardSmallWidth
        {
            get; set;
        }

        = CardSmallWidthDefault;

        public Int32 CollectionViewNumColumns
        {
            get
            {
                Int32 numCols = (Int32)(ScaledWidth / CardSizes.Current.CardBaseWidth);  // +1 for padding

                if (numCols == 0)
                {
                    numCols = 1;
                }

                return numCols;
            }
        }

        public double MediaDetailImageHeight
        {
            get
            {
                double outVal;

                switch (Device.Idiom)
                {
                    case TargetIdiom.Unsupported:

                    case TargetIdiom.Desktop:
                        outVal = CardLargeHeight * 2;
                        break;

                    case TargetIdiom.Tablet:
                        outVal = CardLargeHeight;
                        break;

                    case TargetIdiom.Phone:

                        outVal = CardLargeHeight * 2;
                        break;

                    default:
                        outVal = CardLargeHeight;
                        break;
                };

                return outVal;
            }
        }

        public double MediaDetailImageWidth
        {
            get
            {
                double outVal;

                switch (Device.Idiom)
                {
                    case TargetIdiom.Unsupported:

                    case TargetIdiom.Desktop:
                        outVal = CardLargeWidth * 3;
                        break;

                    case TargetIdiom.Tablet:
                        outVal = CardLargeWidth;
                        break;

                    case TargetIdiom.Phone:

                        outVal = CardLargeWidth;

                        break;

                    default:
                        outVal = CardLargeWidth;
                        break;
                };

                // Check size
                if (outVal > ScaledWidth)
                {
                    outVal = ScaledWidth;
                }

                return outVal;
            }
        }

        public Size ScreenSize
        {
            get
            {
                return new Size(ScaledWidth - 100, ScaledHeight - 100); // Window Size does not include headings
            }
        }

        private double CardBaseWidth
        {
            get; set;
        }

        = CardSmallWidthDefault;

        public void ReCalculateCardWidths()
        {
            ScaledHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            ScaledWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

            SetCardBaseWidth();

            SetCardSingleWidth();
            SetCardSmallWidth();
            SetCardMediumWidth();
            SetCardLargeWidth();
            SetCardLargeDoubleWidth();

            SetCardSingleHeight();
            SetCardSmallHeight();
            SetCardMediumHeight();
            SetCardLargeHeight();

            OnPropertyChanged(nameof(CollectionViewNumColumns));
        }

        private void SetCardBaseWidth()
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
                                outVal = ScaledWidth;
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

            Debug.WriteLine("Card Base Width changed to " + outVal.ToString(System.Globalization.CultureInfo.CurrentCulture));
            CardBaseWidth = outVal;
        }

        private void SetCardLargeDoubleWidth()
        {
            double outVal = CardLargeDoubleWidth * 2;

            // Check size
            if (outVal > ScaledWidth)
            {
                outVal = ScaledWidth;
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
                    {
                        outVal = CardLargeWidth / 3;
                        break;
                    }
                case TargetIdiom.Tablet:
                    {
                        outVal = CardLargeWidth / 3;
                        break;
                    }
                case TargetIdiom.Phone:
                    {
                        outVal = CardLargeWidth / 3;
                        break;
                    }
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
                                outVal = ScaledWidth;
                                break;
                            }
                        case DisplayOrientation.Landscape:
                            {
                                outVal = CardLargeWidthDefault;
                                break;
                            }
                        default:
                            {
                                outVal = ScaledWidth;
                                break;
                            }
                    }

                    break;

                default:
                    outVal = CardLargeWidthDefault;
                    break;
            };

            // Check size
            if (outVal > ScaledWidth)
            {
                outVal = ScaledWidth;
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
                    {
                        outVal = CardMediumWidth / 3;
                        break;
                    }
                default:
                    {
                        outVal = CardMediumWidth / 3;
                        break;
                    }
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
                                outVal = ScaledWidth;
                                break;
                            }
                        case DisplayOrientation.Landscape:
                            {
                                outVal = CardMediumWidthDefault;
                                break;
                            }
                        default:
                            {
                                outVal = ScaledWidth;
                                break;
                            }
                    }

                    break;

                default:
                    outVal = CardMediumWidthDefault;
                    break;
            };

            // Check size
            if (outVal > ScaledWidth)
            {
                outVal = ScaledWidth;
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
                    outVal = CardSingleWidth / 6;
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
                                outVal = ScaledWidth;
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
            if (outVal > ScaledWidth)
            {
                outVal = ScaledWidth;
            }

            Debug.WriteLine("Card Single Width changed to " + outVal.ToString(System.Globalization.CultureInfo.CurrentCulture));
            CardSingleWidth = outVal;
        }

        private void SetCardSmallHeight()
        {
            double outVal = CardBaseWidth / 3;

            switch (Device.Idiom)
            {
                case TargetIdiom.Unsupported:
                    {
                        break;
                    }
                case TargetIdiom.Desktop:
                    {
                        break;
                    }
                case TargetIdiom.Tablet:
                    {
                        break;
                    }
                case TargetIdiom.Phone:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            };

            CardSmallHeight = outVal;
        }

        private void SetCardSmallWidth()
        {
            double outVal = CardBaseWidth;

            switch (Device.Idiom)
            {
                case TargetIdiom.Unsupported:

                case TargetIdiom.Desktop:
                case TargetIdiom.Tablet:

                    break;

                case TargetIdiom.Phone:
                    switch (DataStore.Instance.AD.CurrentOrientation)
                    {
                        case DisplayOrientation.Portrait:
                            {
                                // Expand to the full screen width
                                outVal = ScaledWidth;
                                break;
                            }
                        case DisplayOrientation.Landscape:
                            {
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    break;

                default:

                    break;
            };

            Debug.WriteLine("Card Small Width changed to " + outVal.ToString(System.Globalization.CultureInfo.CurrentCulture));
            CardSmallWidth = outVal;
        }
    }
}