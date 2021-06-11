namespace GrampsView.Common
{
    using GrampsView.Data.Repository;

    using System;
    using System.ComponentModel;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Forms;

    public class CardSizes : ObservableObject, INotifyPropertyChanged
    {
        // Ratio of Height to width is 3 times

        private const double CardSmallWidthDefault = 270;

        // Singleton
        private static CardSizes _current;

        private double WindowHeight = 100;
        private double WindowWidth = 100;
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
                int numCols = (int)Math.Floor(WindowSize.Width / CardSmallWidth);

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
                if (outVal > WindowWidth)
                {
                    outVal = WindowWidth;
                }

                return outVal;
            }
        }

        public Size ScreenSize
        {
            get
            {
                return new Size(((DataStore.Instance.ES.DisplayInfo.Width / DataStore.Instance.ES.DisplayInfo.Density) - 100), ((DataStore.Instance.ES.DisplayInfo.Height / DataStore.Instance.ES.DisplayInfo.Density) - 100));
            }
        }

        public Size WindowSize
        {
            get
            {
                switch (Device.Idiom)
                {
                    case TargetIdiom.Unsupported:

                    case TargetIdiom.Desktop:
                    case TargetIdiom.Tablet:
                        return new Size(WindowWidth - 100, WindowHeight - 100); // Window Size does not include headings

                    case TargetIdiom.Phone:
                        return new Size(WindowWidth, WindowHeight - 100); // Window Size does not include headings

                    default:
                        {
                            break;
                        }
                };

                return new Size(WindowWidth, WindowHeight); // Window Size does not include headings
            }
        }

        private double CardBaseWidth
        {
            get; set;
        }

        = CardSmallWidthDefault;

        public void ReCalculateCardWidths(double width, double height)
        {
            WindowHeight = Math.Floor(height);               // (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density);
            WindowWidth = Math.Floor(width);               //   (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);

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
            // Set base width
            CardBaseWidth = CardSmallWidthDefault;

            // Set width so that a whole number of cards fit across the screen

            int numCols = (int)Math.Floor(WindowSize.Width / CardBaseWidth);

            if (numCols < 1)
            {
                numCols = 1;
            }

            CardBaseWidth = Math.Floor((WindowSize.Width - (numCols * 20)) / numCols);     //adjust for margin and padding for each card

            //Debug.WriteLine("Card Base Width changed to " + CardBaseWidth.ToString(System.Globalization.CultureInfo.CurrentCulture));
        }

        private void SetCardLargeDoubleWidth()
        {
            double outVal = CardLargeWidth * 4;

            // Check size
            if (outVal > WindowSize.Width)
            {
                outVal = WindowSize.Width;
            }

            CardLargeDoubleWidth = outVal;
        }

        private void SetCardLargeHeight()
        {
            CardLargeHeight = Math.Floor(CardLargeWidth / 3);
        }

        private void SetCardLargeWidth()
        {
            double outVal;

            outVal = CardBaseWidth * 2;

            // Check size
            if (outVal > WindowSize.Width)
            {
                outVal = WindowSize.Width;
            }

            CardLargeWidth = outVal;
        }

        private void SetCardMediumHeight()
        {
            CardMediumHeight = Math.Floor(CardMediumWidth / 3);
        }

        private void SetCardMediumWidth()
        {
            double outVal;

            outVal = CardBaseWidth * 1.5;

            // Check size
            if (outVal > WindowSize.Width)
            {
                outVal = WindowSize.Width;
            }

            CardMediumWidth = outVal;
        }

        private void SetCardSingleHeight()
        {
            CardSingleHeight = Math.Floor(CardSingleWidth / 6);
        }

        private void SetCardSingleWidth()
        {
            double outVal = CardBaseWidth;

            //Debug.WriteLine("Card Single Width changed to " + outVal.ToString(System.Globalization.CultureInfo.CurrentCulture));
            CardSingleWidth = outVal;
        }

        private void SetCardSmallHeight()
        {
            CardSmallHeight = Math.Floor(CardSmallWidth / 3);
        }

        private void SetCardSmallWidth()
        {
            double outVal = CardBaseWidth;

            //Debug.WriteLine("Card Small Width changed to " + outVal.ToString(System.Globalization.CultureInfo.CurrentCulture));
            CardSmallWidth = outVal;
        }
    }
}