namespace GrampsView.Data
{
    using System;
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

        bool Valid
        {
            get;
        }

        DateTime FileGetDateTimeModified();
    }
}