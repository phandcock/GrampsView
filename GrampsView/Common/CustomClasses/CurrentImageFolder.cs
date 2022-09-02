namespace GrampsView.Common.CustomClasses
{
    using Microsoft.Extensions.DependencyInjection;

    using SharedSharp.Errors;

    using System.IO;

    using Xamarin.Essentials;

    public class CurrentImageFolder
    {
        public CurrentImageFolder()
        {
            try
            {
                string tt = System.IO.Path.Combine(FileSystem.CacheDirectory, Constants.DirectoryCacheBase, Constants.DirectoryImageCache);

                Value = new DirectoryInfo(tt);

                DirectoryInfo t = new DirectoryInfo(System.IO.Path.Combine(FileSystem.CacheDirectory, Constants.DirectoryCacheBase));

                if (!Value.Exists)
                {
                    t.CreateSubdirectory(Constants.DirectoryImageCache);
                }
            }
            catch (System.Exception ex)
            {
                App.Current.Services.GetService<IErrorNotifications>().NotifyException("Exception creating application image cache", ex, null);
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