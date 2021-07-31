namespace GrampsView.Data
{
    using System.IO;

    public interface IFileInfoEx
    {
        bool Exists
        {
            get;
        }

        FileInfo FInfo
        {
            get;

            set;
        }

        string RelativeDirectory
        {
            get; set;
        }

        bool Valid
        {
            get;
        }

        bool ModifiedComparedToSettings();

        void SaveLastWriteToSettings();
    }
}