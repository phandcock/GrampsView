namespace GrampsView.Data.Repository
{
    using GrampsView.Common.CustomClasses;

    using System.IO;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Essentials;

    public class ApplicationWideData : ObservableObject
    {
        private CurrentDataFolder _CurrentDataFolder = null;
        private CurrentImageFolder _CurrentImageFolder = null;
        private DisplayOrientation _CurrentOrientation = DisplayOrientation.Portrait;

        /// <summary>
        /// Gets or sets the current data folder.
        /// </summary>
        /// <value>
        /// The current data folder.
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
    }
}