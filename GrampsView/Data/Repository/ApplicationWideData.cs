namespace GrampsView.Data.Repository
{
    using GrampsView.Common;

    using System.IO;

    using Xamarin.Essentials;

    public class ApplicationWideData : CommonBindableBase
    {
        private DirectoryInfo _CurrentDataFolder;

        private Stream _CurrentInputStream;

        private string _CurrentInputStreamPath;

        private DisplayOrientation _CurrentOrientation = DisplayOrientation.Portrait;

        /// <summary>
        /// Gets or sets the get current data folder.
        /// </summary>
        /// <value>
        /// The get current data folder.
        /// </value>
        public DirectoryInfo CurrentDataFolder
        {
            get
            {
                return _CurrentDataFolder;
            }

            set
            {
                SetProperty(ref _CurrentDataFolder, value);
            }
        }

        public bool CurrentDataFolderValid
        {
            get
            {
                return (!(CurrentDataFolder == null) && (CurrentDataFolder.Exists));
            }
        }

        public Stream CurrentInputStream
        {
            get
            {
                return _CurrentInputStream;
            }

            set
            {
                SetProperty(ref _CurrentInputStream, value);
            }
        }

        public string CurrentInputStreamPath
        {
            get
            {
                return _CurrentInputStreamPath;
            }

            set
            {
                SetProperty(ref _CurrentInputStreamPath, value);
            }
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

        /// <summary>
        /// Loads the data store from existing known details
        /// </summary>
        public void LoadDataStore()
        {
            CurrentDataFolder = new DirectoryInfo(FileSystem.CacheDirectory);
        }
    }
}