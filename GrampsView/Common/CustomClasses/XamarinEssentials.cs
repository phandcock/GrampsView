namespace GrampsView.Common
{
    using System;

    using Xamarin.Essentials;

    public class XamarinEssentials : IXamarinEssentials
    {
        public DevicePlatform DevicePlatform
        {
            get
            {
                return Xamarin.Essentials.DeviceInfo.Platform;
            }
        }

        public DisplayInfo DisplayInfo
        {
            get
            {
                return DeviceDisplay.MainDisplayInfo;
            }
        }

        public string FileSystemCacheDirectory
        {
            get
            {
                return FileSystem.CacheDirectory;
            }
        }

        public void MainThreadInvokeOnMainThreadAsync(Action argAction)
        {
            MainThread.InvokeOnMainThreadAsync(argAction);
        }

        public bool PreferencesGet(string argPrefrence, bool argDefault)
        {
            return Preferences.Get(argPrefrence, argDefault);
        }

        public int PreferencesGet(string argPrefrence, int argDefault)
        {
            return Preferences.Get(argPrefrence, argDefault);
        }

        public string PreferencesGet(string argPrefrence, string argDefault)
        {
            return Preferences.Get(argPrefrence, argDefault);
        }

        public void PreferencesRemove(string argPreference)
        {
            Preferences.Remove(argPreference);
        }

        public void PreferencesSet(string argPrefrence, string argDefault)
        {
            Preferences.Set(argPrefrence, argDefault);
        }

        public void PreferencesSet(string argPrefrence, bool argDefault)
        {
            Preferences.Set(argPrefrence, argDefault);
        }

        public void PreferencesSet(string argPrefrence, int argDefault)
        {
            Preferences.Set(argPrefrence, argDefault);
        }
    }
}