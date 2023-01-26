// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.Common.Interfaces
{
    public interface IFileInfoEx
    {
        bool Exists
        {
            get;
        }

        FileInfo FInfo
        {
            get;
        }

        string GetAbsoluteFilePath { get; }

        bool Valid
        {
            get;
        }

        DateTime FileGetDateTimeModified();
    }
}