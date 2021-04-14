namespace GrampsView.Common
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class CardSizes : CommonBindableBase, INotifyPropertyChanged
    {
        // Ratio of Height to width is 3 times

        //private const double CardLargeHeightDefault = 420;
        //private const double CardLargeWidthDefault = 420;
        //private const double CardMediumHeightDefault = 300;
        //private const double CardMediumWidthDefault = 300;
        //private const double CardSingleHeightDefault = 20;
        //private const double CardSingleWidthDefault = 270;
        //private const double CardSmallHeightDefault = 270;
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

        = 0;

        public double CardLargeHeight
        {
            get; set;
        }

        = 0;

        public double CardLargeWidth
        {
            get; set;
        }

        = 0;

        public double CardMediumHeight
        {
            get; set;
        }

        = 0;

        public double CardMediumWidth
        {
            get; set;
        }

        = 0;

        public Int32 CardsAcrossColumns
        {
            get
            {
                int numCols = (int)Math.Floor(ScreenSize.Width / CardSmallWidthDefault);

                if (numCols < 1)
                {
                    numCols = 1;
                }

                return numCols;
            }
        }

        public double CardSingleHeight
        {
            get; set;
        }

        = 0;

        public double CardSingleWidth
        {
            get; set;
        }

        = 0;

        public double CardSmallHeight
        {
            get; set;
        }

        = 0;

        public double CardSmallWidth
        {
            get; set;
        }

        = CardSmallWidthDefault;

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
                switch (Device.Idiom)
                {
                    case TargetIdiom.Unsupported:

                    case TargetIdiom.Desktop:
                    case TargetIdiom.Tablet:
                        return new Size(ScaledWidth - 100, ScaledHeight - 100); // Window Size does not include headings

                    case TargetIdiom.Phone:
                        return new Size(ScaledWidth - 80, ScaledHeight); // Window Size does not include headings

                    default:
                        {
                            break;
                        }
                };

                return new Size(ScaledWidth, ScaledHeight); // Window Size does not include headings
            }
        }

        private double CardBaseWidth
        {
            get; set;
        }

        = CardSmallWidthDefault;

        public void ReCalculateCardWidths()
        {
            ScaledHeight = (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density);
            ScaledWidth = (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);

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

            OnPropertyChanged(nameof(CardsAcrossColumns));
        }

        private void SetCardBaseWidth()
        {
            double outVal;

            // Set width so that a whole number of cards fit across the screen

            outVal = ScreenSize.Width / CardsAcrossColumns;

            Debug.WriteLine("Card Base Width changed to " + outVal.ToString(System.Globalization.CultureInfo.CurrentCulture));
            CardBaseWidth = outVal;
        }

        private void SetCardLargeDoubleWidth()
        {
            double outVal = CardLargeDoubleWidth * 2;

            // Check size
            if (outVal > ScreenSize.Width)
            {
                outVal = ScreenSize.Width;
            }

            CardLargeDoubleWidth = outVal;
        }

        private void SetCardLargeHeight()
        {
            double outVal;

            outVal = CardMediumWidth / 3;

            CardLargeHeight = outVal;
        }

        private void SetCardLargeWidth()
        {
            double outVal;

            outVal = CardBaseWidth * 2;

            // Check size
            if (outVal > ScreenSize.Width)
            {
                outVal = ScreenSize.Width;
            }

            CardLargeWidth = outVal;
        }

        private void SetCardMediumHeight()
        {
            double outVal;

            outVal = CardMediumWidth / 3;

            CardMediumHeight = outVal;
        }

        private void SetCardMediumWidth()
        {
            double outVal;

            outVal = CardBaseWidth * 1.5;

            // Check size
            if (outVal > ScreenSize.Width)
            {
                outVal = ScreenSize.Width;
            }

            CardMediumWidth = outVal;
        }

        private void SetCardSingleHeight()
        {
            double outVal;

            outVal = CardSingleWidth / 6;

            CardSingleHeight = outVal;
        }

        private void SetCardSingleWidth()
        {
            double outVal = CardBaseWidth;

            Debug.WriteLine("Card Single Width changed to " + outVal.ToString(System.Globalization.CultureInfo.CurrentCulture));
            CardSingleWidth = outVal;
        }

        private void SetCardSmallHeight()
        {
            double outVal = CardBaseWidth / 3;

            CardSmallHeight = outVal;
        }

        private void SetCardSmallWidth()
        {
            double outVal = CardBaseWidth;

            Debug.WriteLine("Card Small Width changed to " + outVal.ToString(System.Globalization.CultureInfo.CurrentCulture));
            CardSmallWidth = outVal;
        }
    }
}