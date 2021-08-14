namespace GrampsView.Test.e2e.Utility
{
    using GrampsView.Common;
    using GrampsView.Data;
    using GrampsView.Data.Repository;

    using NUnit.Framework;

    using System.Threading.Tasks;

    public static class e2e_test
    {
        public static void TestDecompressGzip()
        {
            GeneralData.GrampsFile = new FileInfoEx(argFileName: CommonConstants.StorageGRAMPSFileName);

            if (GeneralData.GrampsFile.Valid)
            {
                GeneralData.iocStoreFile.DecompressGZIP(GeneralData.GrampsFile);
            }

            Assert.True(DataStore.Instance.AD.CurrentInputStream != null);
        }

        public static void TestDecompressTar()
        {
            GeneralData.iocStoreFile.DecompressTAR();
        }

        public static async Task TestGrampsLoad()
        {
            await GeneralData.iocCommonNotifications.DataLogEntryAdd("Later version of Gramps XML data file found. Loading it into the program").ConfigureAwait(false);

            // Load the new data
            await GeneralData.newManager.TriggerLoadGrampsUnZippedFolderAsync().ConfigureAwait(false);

            Assert.True(DataStore.Instance.AD.CurrentInputStream != null);
        }

        public static async Task TestGrampsUnzip()
        {
            await GeneralData.iocCommonNotifications.DataLogEntryAdd("Later version of Gramps XML data compressed file found. Loading it into the program").ConfigureAwait(false);

            //File.Copy(DataStore.Instance.AD.CurrentInputStreamPath, Path.Combine(DataStore.Instance.AD.CurrentDataFolder.Path, CommonConstants.StorageXMLFileName));

            await GeneralData.newManager.TriggerLoadGRAMPSFileAsync(false).ConfigureAwait(false);

            Assert.True(DataStore.Instance.AD.CurrentInputStream != null);
        }
    }
}