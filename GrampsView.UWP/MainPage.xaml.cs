namespace GrampsView.UWP
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.Messaging;

    using SharedSharp.Messages;

    using Windows.UI.Xaml;

    using Size = Xamarin.Forms.Size;

    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += OnMainPageLoaded;

            LoadApplication(new GrampsView.App());
        }

        private void OnMainPageLoaded(object sender, RoutedEventArgs e)
        {
            this.Frame.SizeChanged += (o, args) =>
            {
                Size t = new Size(args.NewSize.Width, args.NewSize.Height);

                ((GrampsView.App)Xamarin.Forms.Application.Current).Services.GetService<IMessenger>().Send(new SSharpMessageWindowSizeChanged(t));
            };
        }
    }
}