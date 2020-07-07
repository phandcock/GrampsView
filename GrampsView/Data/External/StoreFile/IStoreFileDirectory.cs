namespace GrampsView.Data.External.StoreFile
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    public interface IStoreFileDataFolderDirectory
    {
        SafeHandle DataFolderDirectoryFileOpen(string filePath);

        string DataFolderDirectoryGet();

        Task<bool> DataFolderDirectoryLoad();

        Task<bool> DataFolderDirectoryPick();

        Task<bool> DataFolderDirectorySave();

        void DataFolderDirectorySet(string dataStoreSetting);

        Task<string> DataFolderFileExists(string fileToFind);
    }
}