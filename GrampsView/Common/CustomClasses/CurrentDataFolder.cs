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
                DirectoryInfo BaseDir = new(FileSystem.Current.AppDataDirectory);

                string tt = System.IO.Path.Combine(BaseDir.FullName, Constants.DirectoryCacheBase);

                Value = new DirectoryInfo(tt);

                if (!Value.Exists)
                {
                    Value = BaseDir.CreateSubdirectory(Constants.DirectoryCacheBase);
                }

                Debug.WriteLine("CurrentDataFolder Path:" + Value.FullName);
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Exception creating application cache", ex);
                throw;
            }
        }

        public string Path => Value.FullName;

        public bool Valid => !(Value == null) && Value.Exists;

        public DirectoryInfo Value
        {
            get; set;
        }
            = new(FileSystem.Current.AppDataDirectory);
    }
}