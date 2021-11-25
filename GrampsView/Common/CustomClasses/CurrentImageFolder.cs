namespace GrampsView.Common.CustomClasses
{
    using Microsoft.Extensions.DependencyInjection;

    using System.IO;

    using Xamarin.Essentials;

    public class CurrentImageFolder
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
                return (!(Value == null) && (Value.Exists));
            }
        }

        public DirectoryInfo Value
        {
            get; set;
        } = null;

        public CurrentImageFolder()
        {
            try
            {
                string tt = System.IO.Path.Combine(FileSystem.CacheDirectory, CommonConstants.DirectoryCacheBase, CommonConstants.DirectoryImageCache);

                Value = new DirectoryInfo(tt);

                DirectoryInfo t = new DirectoryInfo(System.IO.Path.Combine(FileSystem.CacheDirectory, CommonConstants.DirectoryCacheBase));

                if (!Value.Exists)
                {
                    t.CreateSubdirectory(CommonConstants.DirectoryImageCache);
                }
            }
            catch (System.Exception ex)
            {
                App.Current.Services.GetService<IErrorNotifications>().NotifyException("Exception creating application image cache", ex, null);
                throw;
            }
        }
    }
}