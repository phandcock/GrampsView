using Microsoft.Extensions.DependencyInjection;

using SharedSharp.Errors.Interfaces;

using System.IO;



namespace GrampsView.Common.CustomClasses
{
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
                    _ = t.CreateSubdirectory(Constants.DirectoryImageCache);
                }
            }
            catch (System.Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Exception creating application image cache", ex, null);
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