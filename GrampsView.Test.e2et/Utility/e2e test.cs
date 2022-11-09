using GrampsView.Common;
using GrampsView.Data;
using GrampsView.Data.Repository;

using NUnit.Framework;

using System.Threading.Tasks;

namespace GrampsView.Test.e2e.Utility
{
    public static class e2e_test
    {
        public static void TestDecompressGzip()
        {
            GeneralData.GrampsFile = new FileInfoEx(argFileName: Constants.StorageGRAMPSFileName);

            if (GeneralData.GrampsFile.Valid)
            {
                _ = GeneralData.iocStoreFile.DecompressGZIP(GeneralData.GrampsFile);
            }

            Assert.True(DataStore.Instance.AD.CurrentInputStream != null);
        }

        public static void TestDecompressTar()
        {
            _ = GeneralData.iocStoreFile.DecompressTAR();
        }

        public static async Task TestGrampsLoad()
        {
            GeneralData.iocCommonLogging.DataLogEntryAdd("Later version of Gramps XML data file found. Loading it into the program");

            // Load the new data
            _ = await GeneralData.newManager.TriggerLoadGrampsUnZippedFolderAsync().ConfigureAwait(false);

            Assert.True(DataStore.Instance.AD.CurrentInputStream != null);
        }

        public static async Task TestGrampsUnzip()
        {
            GeneralData.iocCommonLogging.DataLogEntryAdd("Later version of Gramps XML data compressed file found. Loading it into the program");

            //File.Copy(DataStore.Instance.AD.CurrentInputStreamPath, Path.Combine(DataStore.Instance.AD.CurrentDataFolder.Path, Constants.StorageXMLFileName));

            _ = await GeneralData.newManager.TriggerLoadGRAMPSFileAsync(false).ConfigureAwait(false);

            Assert.True(DataStore.Instance.AD.CurrentInputStream != null);
        }
    }
}