//-----------------------------------------------------------------------
//
// Extended Splash Screen
//
// <copyright file="ExtendedSplashScreen.xaml.cs" company="MeMySelfandI">
//     GPL Copyright
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Views
{
    using Windows.ApplicationModel.Activation;
    using Windows.UI.Xaml;

    using Xamarin.Forms;

    /// <summary>
    /// A very simple Extended Splash Screen page.
    /// </summary>
    public sealed partial class ExtendedSplashScreen : ContentPage
    {
        #region Fields

        /// <summary>
        /// The extended splash screen
        /// </summary>
        private readonly SplashScreen splashScreen;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedSplashScreen" /> class.
        /// </summary>
        /// <param name="splashScreen">
        /// Existing splash screen
        /// </param>
        public ExtendedSplashScreen(SplashScreen splashScreen)
        {
            // store arguments
            this.splashScreen = splashScreen;

            // this.InitializeComponent();

            // this.SizeChanged += this.ExtendedSplashScreen_SizeChanged;
            // this.splashImage.ImageOpened += this.SplashImage_ImageOpened;
        }

        #endregion Constructors

        /// <summary>
        /// Whenever the size of the application change, the image position and size need to be recalculated.
        /// </summary>
        /// <param name="sender">
        /// Calling page
        /// </param>
        /// <param name="e">
        /// Size Changed Arguments
        /// </param>
        private void ExtendedSplashScreen_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Resize();
        }

        /// <summary>
        /// This method is used to position and resizing the splash screen image correctly.
        /// </summary>
        private void Resize()
        {
            if (splashScreen == null)
            {
                return;
            }

            // The splash image's not always perfectly centered. Therefore we need to set our image's
            // position to match the original one to obtain a clean transition between both splash screens.

            //splashImage.Height = splashScreen.ImageLocation.Height;
            //splashImage.Width = splashScreen.ImageLocation.Width;

            //splashImage.SetValue(Canvas.TopProperty, splashScreen.ImageLocation.Top);
            //splashImage.SetValue(Canvas.LeftProperty, splashScreen.ImageLocation.Left);

            //progressRing.SetValue(Canvas.TopProperty, splashScreen.ImageLocation.Top + splashScreen.ImageLocation.Height + 50);
            //progressRing.SetValue(Canvas.LeftProperty, splashScreen.ImageLocation.Left + (splashScreen.ImageLocation.Width / 2) - (progressRing.Width / 2));
        }

        /// <summary>
        /// Resize and activate the window when the embedded image is opened The application's window
        /// should not become activate until the extended splash screen is ready to be shown in order
        /// to prevent flickering when switching between the real splash screen and this one. In
        /// order to do this we need to be sure that the image was opened so we subscribed to this
        /// event and activate the window in it.
        /// </summary>
        /// <param name="sender">
        /// Calling page
        /// </param>
        /// <param name="e">
        /// Routed Event Arguments
        /// </param>
        private void SplashImage_ImageOpened(object sender, RoutedEventArgs e)
        {
            Resize();
            Window.Current.Activate();
        }
    }
}