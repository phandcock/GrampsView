namespace GrampsView.Common.CustomClasses
{
    using GrampsView.Data.Repository;

    using System.IO;

    public class CurrentDataFolder
    {
        public CurrentDataFolder()
        {
            try
            {
                //// Help with Unit Testing
                //if (DeviceInfo.Platform == DevicePlatform.Unknown)
                //{
                //    return;
                //}

                string tt = System.IO.Path.Combine(DataStore.Instance.ES.FileSystemCacheDirectory, CommonConstants.DirectoryCacheBase);

                Value = new DirectoryInfo(tt);

                DirectoryInfo t = new DirectoryInfo(DataStore.Instance.ES.FileSystemCacheDirectory);

                if (!Value.Exists)
                {
                    Value = t.CreateSubdirectory(CommonConstants.DirectoryCacheBase);
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
                return !(Value == null) && (Value.Exists);
            }
        }

        public DirectoryInfo Value
        {
            get; set;
        } = null;
    }
}