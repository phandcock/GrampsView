﻿namespace GrampsView.Common.CustomClasses
{
    using GrampsView.Data.Repository;

    using System.IO;

    using Xamarin.Essentials;

    public class CurrentDataFolder
    {
        public CurrentDataFolder()
        {
            try
            {
                // Help with Unit Testing
                if (DeviceInfo.Platform == DevicePlatform.Unknown)
                    return;

                string tt = System.IO.Path.Combine(FileSystem.CacheDirectory, CommonConstants.DirectoryCacheBase);

                Value = new DirectoryInfo(tt);

                if (!Value.Exists)
                {
                    DirectoryInfo t = new DirectoryInfo(FileSystem.CacheDirectory);
                    t.CreateSubdirectory(CommonConstants.DirectoryCacheBase);
                }
            }
            catch (System.Exception ex)
            {
                DataStore.Instance.CN.NotifyException("Exception creating application cache", ex, null);
                throw;
            }
        }

        public string Path
        {
            get
            {
                return Value.FullName;
            }
        }

        public bool Valid
        {
            get
            {
                return (!(Value == null) && (Value.Exists));
            }
        }

        public DirectoryInfo Value
        {
            get; set;
        } = null;
    }
}