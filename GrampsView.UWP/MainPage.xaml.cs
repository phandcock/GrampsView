namespace GrampsView.UWP
{
    using GrampsView.Common.CustomClasses;

    using Prism;
    using Prism.Ioc;

    public sealed partial class MainPage
    {
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