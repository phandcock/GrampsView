using GrampsView.Common;
using GrampsView.Data.Repository;

using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;
using SharedSharp.Logging.Interfaces;

using System.Diagnostics;

namespace GrampsView.Data
{
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
            Ioc.Default.GetRequiredService<ILog>().DataLogEntryAdd("Deleting existing datastorage");
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
                    _ = DataStore.Instance.AD.CurrentDataFolder.Value.CreateSubdirectory(Constants.DirectoryImageCache);

                    // TODO    await DataStore.Instance.FFIL.InvalidateCacheAsync(CacheType.All).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("DataStorageInitialiseAsync", ex);
                    throw;
                }
            }

            // Wait for Android. TODO FInd a better answer for why crash if load file twice Dispose error
            await Task.Delay(2000);

            Ioc.Default.GetRequiredService<ILog>().DataLogEntryReplace("");

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
        public bool DecompressGZIP(IFileInfoEx inputFile)
        {
            Ioc.Default.GetRequiredService<ILog>().DataLogEntryAdd("Decompressing GRAMPS GZIP file");

            // Check arguments
            if (inputFile == null)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("The input file is null"));
                return false;
            }

            try
            {
                _ = ExtractGZip(inputFile, "data.xml");

                Ioc.Default.GetRequiredService<ILog>().DataLogEntryReplace("GRAMPS GZIP file decompress complete");
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                ErrorInfo t = new("Unauthorised Access exception when trying to acess file")
                    {
                        { "Exception Message ", ex.Message },
                    };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(t);
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
            Ioc.Default.GetRequiredService<ILog>().DataLogEntryAdd("Decompressing GRAMPS TAR files");

            // Check arguments
            if (!DataStore.Instance.AD.CurrentInputStreamValid)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyAlert("The input file is invalid");
                return false;
            }

            Stream originalFileStream = DataStore.Instance.AD.CurrentInputStream;

            // open the gzip and extract the tar file
            using (Stream stream = new GZipInputStream(originalFileStream))
            {
                using TarInputStream tarIn = new(stream, System.Text.Encoding.ASCII);
                // TODO DO NOT AWAIT as causes thread blocking await
                await ExtractTar(tarIn).ConfigureAwait(false);
            }

            Ioc.Default.GetRequiredService<ILog>().DataLogEntryReplace("UnTaring of files complete");
            return true;
        }
    }
}