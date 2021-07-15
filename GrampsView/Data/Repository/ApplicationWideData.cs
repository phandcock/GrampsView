namespace GrampsView.Data.Repository
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;

    using System.IO;
    using System.Threading;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Essentials;

    public class ApplicationWideData : ObservableObject
    {
        private CurrentDataFolder _CurrentDataFolder = null;
        private CurrentImageFolder _CurrentImageFolder = null;
        private DisplayOrientation _CurrentOrientation = DisplayOrientation.Portrait;

        /// <summary>
        /// Gets or sets the get current data folder.
        /// </summary>
        /// <value>
        /// The get current data folder.
        /// </value>
        public CurrentDataFolder CurrentDataFolder
        {
            get
            {
                if (_CurrentDataFolder == null)
                {
                    _CurrentDataFolder = new CurrentDataFolder();
                }

                return _CurrentDataFolder;
            }

            set => SetProperty(ref _CurrentDataFolder, value);
        }

        public CurrentImageFolder CurrentImageAssetsFolder
        {
            get
            {
                if (_CurrentImageFolder == null)
                {
                    _CurrentImageFolder = new CurrentImageFolder();
                }

                return _CurrentImageFolder;
            }
        }

        public Stream CurrentInputStream
        {
            get;

            set;
        }

        public string CurrentInputStreamFileType
        {
            get
            {
                return Path.GetExtension(CurrentInputStreamPath);
            }
        }

        public string CurrentInputStreamPath
        {
            get;

            set;
        }

        public bool CurrentInputStreamValid
        {
            get
            {
                return !(CurrentInputStream == null);
            }
        }

        public DisplayOrientation CurrentOrientation
        {
            get => _CurrentOrientation;

            set => SetProperty(ref _CurrentOrientation, value);
        }

        public Xamarin.Forms.Size ScreenSize
        {
            get; set;
        } = new Xamarin.Forms.Size(300, 500);

        public void ScreenSizeInit()
        {
            int screenWidth = (int)(DataStore.Instance.ES.DisplayInfo.Width / DataStore.Instance.ES.DisplayInfo.Density);

            int screenHeight = (int)(DataStore.Instance.ES.DisplayInfo.Height / DataStore.Instance.ES.DisplayInfo.Density);

            Thread.Sleep(500);

            switch (DataStore.Instance.ES.DisplayInfo.Orientation)
            {
                case DisplayOrientation.Portrait:
                    {
                        switch (DataStore.Instance.ES.DisplayInfo.Rotation)
                        {
                            case DisplayRotation.Rotation0:
                                {
                                    DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Portrait;
                                    ScreenSize = new Xamarin.Forms.Size(screenWidth, screenHeight);
                                    CardSizes.Current.ReCalculateCardWidths();
                                    break;
                                }

                            case DisplayRotation.Rotation90:
                                {
                                    DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Portrait;
                                    ScreenSize = new Xamarin.Forms.Size(screenWidth, screenHeight);
                                    CardSizes.Current.ReCalculateCardWidths();
                                    break;
                                }

                            case DisplayRotation.Rotation180:
                                {
                                    DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Portrait;
                                    ScreenSize = new Xamarin.Forms.Size(screenWidth, screenHeight);
                                    CardSizes.Current.ReCalculateCardWidths();
                                    break;
                                }

                            case DisplayRotation.Rotation270:
                                {
                                    DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Landscape;
                                    ScreenSize = new Xamarin.Forms.Size(screenHeight, screenWidth);
                                    CardSizes.Current.ReCalculateCardWidths();
                                    break;
                                }
                        }
                        break;
                    }

                case DisplayOrientation.Landscape:
                    {
                        switch (DataStore.Instance.ES.DisplayInfo.Rotation)
                        {
                            case DisplayRotation.Rotation0:
                                {
                                    DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Landscape;
                                    ScreenSize = new Xamarin.Forms.Size(screenWidth, screenHeight);
                                    CardSizes.Current.ReCalculateCardWidths();
                                    break;
                                }

                            case DisplayRotation.Rotation90:
                                {
                                    DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Landscape;
                                    ScreenSize = new Xamarin.Forms.Size(screenWidth, screenHeight);
                                    CardSizes.Current.ReCalculateCardWidths();
                                    break;
                                }

                            case DisplayRotation.Rotation180:
                                {
                                    DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Portrait;
                                    ScreenSize = new Xamarin.Forms.Size(screenHeight, screenWidth);
                                    CardSizes.Current.ReCalculateCardWidths();
                                    break;
                                }

                            case DisplayRotation.Rotation270:
                                {
                                    DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Landscape;
                                    ScreenSize = new Xamarin.Forms.Size(screenWidth, screenHeight);
                                    CardSizes.Current.ReCalculateCardWidths();
                                    break;
                                }
                        }
                        break;
                    }
            }

            // TODO Handle UWP windows resize
        }
    }
}