// Copyright (c) phandcock.  All rights reserved.

using SharedSharp.Errors.Interfaces;

using System.Diagnostics;

namespace GrampsView.Common.CustomClasses
{
    public class CurrentDataFolder
    {
        public CurrentDataFolder()
        {
            try
            {
                DirectoryInfo BaseDir = new(FileSystem.AppDataDirectory);

                string tt = System.IO.Path.Combine(BaseDir.FullName, Constants.DirectoryCacheBase);

                FolderasDirInfo = new DirectoryInfo(tt);

                if (!FolderasDirInfo.Exists)
                {
                    FolderasDirInfo = BaseDir.CreateSubdirectory(Constants.DirectoryCacheBase);
                }

                Debug.WriteLine("CurrentDataFolder Path:" + FolderasDirInfo.FullName);
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Exception creating application cache", ex);
            }
        }

        public DirectoryInfo FolderasDirInfo
        {
            get; set;
        }
            = new(FileSystem.AppDataDirectory);

        public string FolderAsString => FolderasDirInfo.FullName;

        public bool Valid => !(FolderasDirInfo == null) && FolderasDirInfo.Exists;

        public string GetAbsoluteFilePath(string argFilePath)
        {
            return Path.Combine(FolderAsString, argFilePath);
        }

        public string GetRelativeFilePath(string argFilePath)
        {
            return Path.Combine(Constants.DirectoryCacheBase, argFilePath);
        }
    }
}