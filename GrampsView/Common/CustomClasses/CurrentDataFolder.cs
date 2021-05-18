using System.IO;

using Xamarin.Essentials;

namespace GrampsView.Common.CustomClasses
{
    public class CurrentDataFolder
    {
        public CurrentDataFolder()
        {
            // Help with Unit Testing
            if (DeviceInfo.Platform == DevicePlatform.Unknown)
                return;

            Value = new DirectoryInfo(FileSystem.CacheDirectory);
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