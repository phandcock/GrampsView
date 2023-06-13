// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Repository;

using SharedSharp.Errors.Interfaces;

using System.Diagnostics;

namespace GrampsView.Data.StoreFile
{
    public class StoreFile : ObservableObject, IStoreFile
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
                    foreach (FileInfo item in DataStore.Instance.AD.CurrentDataFolder.FolderasDirInfo.GetFiles())
                    {
                        item.Delete();
                    }

                    foreach (DirectoryInfo item in DataStore.Instance.AD.CurrentDataFolder.FolderasDirInfo.GetDirectories())
                    {
                        Thread.Sleep(100);
                        Debug.WriteLine($"About to delete  directory: {item.FullName}");
                        item.Delete(true);
                    }
                }
                catch (Exception ex)
                {
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("DataStorageInitialiseAsync", ex);
                }
            }

            // Wait for Android. TODO FInd a better answer for why crash if load file twice Dispose error
            await Task.Delay(2000);

            Ioc.Default.GetRequiredService<ILog>().DataLogEntryReplace("");

            return true;
        }
    }
}