namespace GrampsView.Common
{
    using Xamarin.Essentials;

    /// <summary>
    /// Implement interface to allow Unit Tetsing and Mocking
    /// </summary>
    public interface IXamarinEssentials
    {
        DisplayInfo DisplayInfo
        {
            get;
        }

        string FileSystemCacheDirectory
        {
            get;
        }

        string PreferencesGet(string argPrefrence, string argDefault);

        bool PreferencesGet(string argPrefrence, bool argDefault);

        int PreferencesGet(string argPrefrence, int argDefault);

        void PreferencesRemove(string argPreference);

        void PreferencesSet(string argPrefrence, string argDefault);

        void PreferencesSet(string argPrefrence, bool argDefault);

        void PreferencesSet(string argPrefrence, int argDefault);
    }
}