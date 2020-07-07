namespace GrampsView.Data.Repository
{
    using GrampsView.Common;
    using Plugin.FilePicker.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Xamarin.Essentials;

    public class ApplicationWideData : CommonBindableBase
    {
        private DirectoryInfo _CurrentDataFolder;

        private FileData _CurrentInputFile = null;

        private DirectoryInfo _CurrentInputFolder;

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

        public FileData CurrentInputFile
        {
            get
            {
                return _CurrentInputFile;
            }

            set
            {
                SetProperty(ref _CurrentInputFile, value);
            }
        }

        public bool CurrentInputFileValid
        {
            get
            {
                return (!(CurrentInputFile == null));
            }
        }

        public DirectoryInfo CurrentInputFolder
        {
            get
            {
                return _CurrentInputFolder;
            }

            set
            {
                SetProperty(ref _CurrentInputFolder, value);
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
        /// Loads the data store from existign known details
        /// </summary>
        public void LoadDataStore()
        {
            CurrentDataFolder = new DirectoryInfo(Xamarin.Essentials.FileSystem.CacheDirectory);

            CurrentDataFolder.CreateSubdirectory("Cropped");
        }
    }
}