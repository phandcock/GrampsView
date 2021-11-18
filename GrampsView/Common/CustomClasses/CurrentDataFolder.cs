﻿namespace GrampsView.Common.CustomClasses
{
    using GrampsView.Data.Repository;

    using Microsoft.Extensions.DependencyInjection;

    using System.Diagnostics;
    using System.IO;

    public class CurrentDataFolder
    {
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
                return !(Value == null) && (Value.Exists);
            }
        }

        public DirectoryInfo Value
        {
            get; set;
        } = null;

        public CurrentDataFolder()
        {
            try
            {
                string tt = System.IO.Path.Combine(DataStore.Instance.ES.FileSystemCacheDirectory, CommonConstants.DirectoryCacheBase);

                Value = new DirectoryInfo(tt);

                DirectoryInfo t = new DirectoryInfo(DataStore.Instance.ES.FileSystemCacheDirectory);

                if (!Value.Exists)
                {
                    Value = t.CreateSubdirectory(CommonConstants.DirectoryCacheBase);
                }

                Debug.WriteLine("CurrentDataFolder Path:" + Value.FullName);
            }
            catch (System.Exception ex)
            {
                App.Current.Services.GetService<IErrorNotifications>().NotifyException("Exception creating application cache", ex, null);
                throw;
            }
        }
    }
}