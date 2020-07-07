using GrampsView.Common.CustomClasses;

using Prism;
using Prism.Ioc;

namespace GrampsView.UWP
{
    public sealed partial class MainPage
    {
        //public CommonTileUpdate tileUpdater;

        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new GrampsView.App(new UWPInit()));
        }
    }

    public class UWPInit : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IPlatformSpecific, PlatformSpecific>();
        }
    }
}