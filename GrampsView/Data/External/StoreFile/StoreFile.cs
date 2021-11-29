namespace GrampsView.Data
{
    using FFImageLoading.Cache;

    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using ICSharpCode.SharpZipLib.GZip;
    using ICSharpCode.SharpZipLib.Tar;

    using Microsoft.Extensions.DependencyInjection;

    using SharedSharp.Errors;

    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    public partial class StoreFile : ObservableObject, IStoreFile
    {
        /// <summary>
        /// Deletes all local copies of GRAMPS data.
        /// </summary>
        /// <returns>
        /// Flag if data cleared.
        /// </returns>
        public async Task<bool> DataStorageInitialiseAsync()
        {
            App.Current.Services.GetService<IErrorNotifications>().DataLogEntryAdd("Deleting existing datastorage");
            {
                try
                {
                    foreach (FileInfo item in DataStore.Instance.AD.CurrentDataFolder.Value.GetFiles())
                    {
                        item.Delete();
                    }

                    foreach (DirectoryInfo item in DataStore.Instance.AD.CurrentDataFolder.Value.GetDirectories())
                    {
                        System.Threading.Thread.Sleep(100);
                        Debug.WriteLine($"About to delete  directory: {item.FullName}");
                        item.Delete(true);
                    }

                    // Create standard directories
                    DataStore.Instance.AD.CurrentDataFolder.Value.CreateSubdirectory(CommonConstants.DirectoryImageCache);

                    await DataStore.Instance.FFIL.InvalidateCacheAsync(CacheType.All).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    App.Current.Services.GetService<IErrorNotifications>().NotifyException("DataStorageInitialiseAsync", ex);
                    throw;
                }
            }

            // Wait for Android. TODO FInd a better answer for why crash if load file twice Dispose error
            await Task.Delay(2000);

            App.Current.Services.GetService<IErrorNotifications>().DataLog.Remove();

            return true;
        }

        /// <summary>
        /// This routine is heavily customized to decompress GRAMPS whole of database export file
        /// (i.e. .gramps) files.
        /// </summary>
        /// <param name="inputFile">
        /// Input GRAMPS export file.
        /// </param>
        /// <returns>
        /// Flag indicating success or not.
        /// </returns>
        public async Task<bool> DecompressGZIP(IFileInfoEx inputFile)
        {
            App.Current.Services.GetService<IErrorNotifications>().DataLogEntryAdd("Decompressing GRAMPS GZIP file");

            // Check arguments
            if (inputFile == null)
            {
                App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("The input file is null"));
                return false;
            }

            try
            {
                ExtractGZip(inputFile, "data.xml");

                App.Current.Services.GetService<IErrorNotifications>().DataLogEntryReplace("GRAMPS GZIP file decompress complete");
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                ErrorInfo t = new ErrorInfo("Unauthorised Access exception when trying to acess file")
                    {
                        { "Exception Message ", ex.Message },
                    };

                App.Current.Services.GetService<IErrorNotifications>().NotifyError(t);
                return false;
            }
        }

        /// <summary>
        /// This routine is heavily customized to decompress GRAMPS whole of database export file
        /// (i.e. .gramps) files.
        /// </summary>
        /// <returns>
        /// Flag indicating success or not.
        /// </returns>
        public async Task<bool> DecompressTAR()
        {
            await App.Current.Services.GetService<IErrorNotifications>().DataLogEntryAdd("Decompressing GRAMPS TAR files").ConfigureAwait(false);

            // Check arguments
            if (!DataStore.Instance.AD.CurrentInputStreamValid)
            {
                App.Current.Services.GetService<IErrorNotifications>().NotifyAlert("The input file is invalid");
                return false;
            }

            Stream originalFileStream = DataStore.Instance.AD.CurrentInputStream;

            // open the gzip and extract the tar file
            using (Stream stream = new GZipInputStream(originalFileStream))
            {
                using (TarInputStream tarIn = new TarInputStream(stream, System.Text.Encoding.ASCII))
                {
                    // TODO DO NOT AWAIT as causes thread blocking await
                    await ExtractTar(tarIn).ConfigureAwait(false);
                }
            }

            App.Current.Services.GetService<IErrorNotifications>().DataLogEntryReplace("UnTaring of files complete");
            return true;
        }
    }
}