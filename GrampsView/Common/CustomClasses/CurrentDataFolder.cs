using Microsoft.Extensions.DependencyInjection;

using SharedSharp.Errors.Interfaces;

using System.Diagnostics;
using System.IO;

using Xamarin.Essentials.Interfaces;

namespace GrampsView.Common.CustomClasses
{
    public class CurrentDataFolder
    {
        public CurrentDataFolder()
        {
            try
            {
                string tt = System.IO.Path.Combine(App.Current.Services.GetService<IFileSystem>().CacheDirectory, Constants.DirectoryCacheBase);

                Value = new DirectoryInfo(tt);

                DirectoryInfo t = new DirectoryInfo(App.Current.Services.GetService<IFileSystem>().CacheDirectory);

                if (!Value.Exists)
                {
                    Value = t.CreateSubdirectory(Constants.DirectoryCacheBase);
                }

                Debug.WriteLine("CurrentDataFolder Path:" + Value.FullName);
            }
            catch (System.Exception ex)
            {
                App.Current.Services.GetService<IErrorNotifications>().NotifyException("Exception creating application cache", ex, null);
                throw;
            }
        }

        public string Path => Value.FullName;

        public bool Valid => !(Value == null) && Value.Exists;

        public DirectoryInfo Value
        {
            get; set;
        } = null;
    }
}