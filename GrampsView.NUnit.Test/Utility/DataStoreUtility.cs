namespace GrampsView.NUnit.Test.Utility
{
    using GrampsView.Data.Repository;

    using System.IO;

    public static class DataStoreUtility
    {
        public static string DataStorePath = Path.Combine(Path.GetTempPath(), "UnitTestDataStore");

        public static void DataStoreSetup()
        {
            // Delete if it exists
            if (Directory.Exists(DataStorePath))
            {
                Directory.Delete(DataStorePath);
            }

            Directory.CreateDirectory(DataStorePath);

            DataStore.Instance.AD.CurrentDataFolder = new DirectoryInfo(DataStorePath);
        }
    }
}