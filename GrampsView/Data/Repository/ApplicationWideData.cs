namespace GrampsView.Data.Repository
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;

    using System.IO;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Essentials;

    public class ApplicationWideData : ObservableObject
    {
        private DisplayOrientation _CurrentOrientation = DisplayOrientation.Portrait;

        /// <summary>
        /// Gets or sets the get current data folder.
        /// </summary>
        /// <value>
        /// The get current data folder.
        /// </value>
        public CurrentDataFolder CurrentDataFolder
        {
            get;

            set;
        } = new CurrentDataFolder();

        public string CurrentImageAssetsFolderPath
        {
            get
            {
                return Path.Combine(CurrentDataFolder.Path, CommonConstants.fileToImageSubDirectory);
            }
        }

        public Stream CurrentInputStream
        {
            get;

            set;
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
            get
            {
                return _CurrentOrientation;
            }

            set
            {
                SetProperty(ref _CurrentOrientation, value);
            }
        }
    }
}