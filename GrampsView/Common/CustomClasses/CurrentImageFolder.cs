// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Repository;

using SharedSharp.Errors.Interfaces;

namespace GrampsView.Common.CustomClasses
{
    public class CurrentImageFolder
    {
        public CurrentImageFolder()
        {
            try
            {
                string tt = Path.Combine(DataStore.Instance.AD.CurrentDataFolder.FolderAsString, Constants.DirectoryImageCache);

                FolderAsDirInfo = new DirectoryInfo(tt);

                if (!FolderAsDirInfo.Exists)
                {
                    _ = DataStore.Instance.AD.CurrentDataFolder.FolderasDirInfo.CreateSubdirectory(Constants.DirectoryImageCache);
                }
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Exception creating application image cache", ex);
                throw;
            }
        }

        public DirectoryInfo FolderAsDirInfo
        {
            get; set;
        } = null;

        public string FolderAsString => FolderAsDirInfo.FullName;

        public bool Valid => !(FolderAsDirInfo == null) && FolderAsDirInfo.Exists;

        public string GetAbsoluteFilePath(string argFilePath)
        {
            return Path.Combine(FolderAsString, argFilePath);
        }

        public string GetRelativeFilePath(string argFilePath)
        {
            return Path.Combine(Constants.DirectoryImageCache, argFilePath);
        }
    }
}