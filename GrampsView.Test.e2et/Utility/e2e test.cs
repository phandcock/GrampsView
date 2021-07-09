namespace GrampsView.Test.e2e.Utility
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data;
    using GrampsView.Data.Repository;

    using NUnit.Framework;

    public static class e2e_test
    {
        public static void doTest()
        {
            GeneralData.iocStoreFile.DecompressTAR();

            FileInfoEx GrampsFile = StoreFolder.FolderGetFile(CommonConstants.StorageGRAMPSFileName);

            if (GrampsFile.Valid)
            {
                GeneralData.iocStoreFile.DecompressGZIP(GrampsFile);
            }

            Assert.True(DataStore.Instance.AD.CurrentInputStream != null);
        }
    }
}