using GrampsView.Data.External.StoreFile;

using Xamarin.Forms;

[assembly: Dependency(typeof(StoreFileDirectory))]

namespace GrampsView.Data.External.StoreFile
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using Android.Database;
    using Android.Util;

    using GrampsView.Common;

    using Plugin.FilePicker;
    using Plugin.FilePicker.Abstractions;

    public class StoreFileDirectory : IStoreFileDataFolderDirectory
    {
        private DirectoryInfo CurrentThumbNailFolder { get; set; }

        private DirectoryInfo DataStorageFolder { get; set; }

        public static String GetPath(Android.Net.Uri uri)  // throws URISyntaxException
        {
            Android.Content.Context context = Android.App.Application.Context;

            if ("content" == uri.Scheme.ToLower())
            {
                String[] projection = { "_data" };

                ICursor cursor = null;

                try
                {
                    cursor = context.ContentResolver.Query(uri, projection, null, null, null);
                    int column_index = cursor.GetColumnIndexOrThrow("_data");
                    if (cursor.MoveToFirst())
                    {
                        return cursor.GetString(column_index);
                    }
                }
                catch (Exception e)
                {
                    // Eat it
                }
            }
            else if ("file" == uri.Scheme.ToLower())
            {
                return uri.Path;
            }

            return null;
        }

        public SafeHandle DataFolderDirectoryFileOpen(string filePath) => throw new NotImplementedException();

        public string DataFolderDirectoryGet()
        {
            if (!(DataStorageFolder is null))
            {
                return DataStorageFolder.FullName;
            }

            return string.Empty;
        }

        public async Task<bool> DataFolderDirectoryLoad()
        {
            string t = Xamarin.Essentials.Preferences.Get(CommonConstants.SettingsDataStorageFolder, DataStorageFolder.FullName);

            DataStorageFolder = new DirectoryInfo(t);

            CurrentThumbNailFolder = new DirectoryInfo(DataStorageFolder.FullName + Path.DirectorySeparatorChar + CommonConstants.StorageThumbNailFolder);

            return true;
        }

        /// <summary>
        /// Datas the folder directory pick.
        /// </summary>
        /// <returns>
        /// Pick the data sotoragefolder.
        /// </returns>
        public async Task<bool> DataFolderDirectoryPick()
        {
            FileData inputPath = await CrossFilePicker.Current.PickFile();

            if (inputPath != null)
            {
                //DataStorageFolder = GetDirectoryInfoFromPath(Path.GetDirectoryName(inputPath.FilePath));
                Android.Net.Uri ttt = Android.Net.Uri.Parse(inputPath.FilePath);
                var contentUri = GetPath(ttt);

                Plugin.NetStandardStorage.Implementations.File t = new Plugin.NetStandardStorage.Implementations.File(inputPath.FilePath);

                // TODO too painful for now just hardcode it
                var _path = Android.OS.Environment.ExternalStorageDirectory;

                var ttttt = GetSDCard(); //.getExternalStorageDirectory();

                DataStorageFolder = new DirectoryInfo("/card/test");

                // TODO Fix this // StoreFileNames.DataFolderSetToNew(inputFolder);

                //Get the folder.Recreate it if it has been accidently or otherwise deleted.
                CurrentThumbNailFolder = new DirectoryInfo(DataStorageFolder.FullName + Path.DirectorySeparatorChar + CommonConstants.StorageThumbNailFolder);

                await DataFolderDirectorySave();

                return true;
            }
            return false;
        }

        public async Task<bool> DataFolderDirectorySave()
        {
            Xamarin.Essentials.Preferences.Set(CommonConstants.SettingsDataStorageFolder, DataStorageFolder.FullName);

            return true;
        }

        public void DataFolderDirectorySet(string dataStoreSetting)
        {
            DataStorageFolder = new DirectoryInfo(dataStoreSetting);
        }

        /// <summary>
        /// Datas the folder file exists.
        /// </summary>
        /// <param name="fileToFind">
        /// The file to find.
        /// </param>
        /// <returns>
        /// The filename if it exists or else string.empty.
        /// </returns>
        public async Task<string> DataFolderFileExists(string fileToFind)
        {
            IEnumerable<FileInfo> t = DataStorageFolder.EnumerateFiles();

            return string.Empty;
        }

        private static DirectoryInfo GetDirectoryInfoFromPath(string folderPath)
        {
            return new DirectoryInfo(folderPath);
        }

        private string GetSDCard()
        {
            var _path = Android.App.Application.Context.GetExternalFilesDirs(null);

            string Datapath;
            int foo, bar;
            foreach (var spath in _path)
            {
                if (spath == null) //just loop on if there is no card inserted
                    continue;
                Datapath = spath.AbsolutePath.ToString();
                var libraryPath = Datapath;
                foo = Datapath.IndexOf("emulated");
                if (foo >= 0)
                { //emulated path, use this if nothing else is available
                    bar = Datapath.IndexOf("/", foo, StringComparison.CurrentCulture);
                    foo = bar + 1;
                    bar = Datapath.IndexOf("/", foo, StringComparison.CurrentCulture);
                    var r1 = Datapath.Substring(0, bar + 1);
                }
                else
                { //external SD card, break out after we find first one
                    foo = 1;
                    bar = Datapath.IndexOf("/", foo, StringComparison.CurrentCulture);
                    foo = bar + 1;
                    bar = Datapath.IndexOf("/", foo, StringComparison.CurrentCulture);
                    var r2 = Datapath.Substring(0, bar + 1);
                }
            }

            ///////////////////////////////////////
            /////Retrieve the External Storages root directory:
            var primaryExternalStorage = Android.OS.Environment.ExternalStorageDirectory;

            ///
            String externalStorageRootDir;
            if ((externalStorageRootDir = primaryExternalStorage.Parent) == null)
            {  // no parent...
                Log.Debug("TAG", "External Storage: " + primaryExternalStorage + "\n");
            }
            else
            {
                //var externalStorageRoot = new android.io.File(externalStorageRootDir);
                //final File[] files = externalStorageRoot.ListFiles();

                //for (final File file : files)
                //{
                //    if (file.isDirectory() && file.canRead() && (file.listFiles().length > 0))
                //    {  // it is a real directory (not a USB drive)...
                //        Log.d(TAG, "External Storage: " + file.getAbsolutePath() + "\n");
                //    }
                //}
            }

            return null;
        }
    }
}