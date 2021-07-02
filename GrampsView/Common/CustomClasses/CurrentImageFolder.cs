namespace GrampsView.Common.CustomClasses
{
    using GrampsView.Data.Repository;

    using System.IO;

    using Xamarin.Essentials;

    public class CurrentImageFolder
    {
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
                DataStore.Instance.CN.NotifyException("Exception creating application image cache", ex, null);
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