namespace GrampsView.Common
{
    using Xamarin.Forms;

    public static class CommonFontSize
    {
        public static double FontLarge
        {
            get
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        {
                            return 18.0;
                        }

                    case Device.iOS:
                        {
                            return 16.9;
                        }

                    case Device.UWP:
                        {
                            return 16.0;
                        }

                    default:
                        {
                            return 16.0;
                        }
                }
            }
        }

        public static double FontMedium
        {
            get
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        {
                            return 16.0;
                        }

                    case Device.iOS:
                        {
                            return 16;
                        }

                    case Device.UWP:
                        {
                            return 16; //  14.0;
                        }

                    default:
                        {
                            return 16; // 14.0;
                        }
                }
            }
        }

        public static double FontSmall
        {
            get
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        {
                            return 14; // 10.0;
                        }

                    case Device.iOS:
                        {
                            return 16; // 11;
                        }

                    case Device.UWP:
                        {
                            return 14; // 10;
                        }

                    default:
                        {
                            return 14; // 12.0;
                        }
                }
            }
        }

        public static double FontVerySmall
        {
            get
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        {
                            return 10.0;
                        }

                    case Device.iOS:
                        {
                            return 11;
                        }

                    case Device.UWP:
                        {
                            return 10;
                        }

                    default:
                        {
                            return 12.0;
                        }
                }
            }
        }
    }
}