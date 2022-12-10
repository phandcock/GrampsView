using GrampsView.Models.DataModels.Interfaces;
using GrampsView.ViewModels.Media;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

namespace GrampsView.Views
{
    public partial class MediaDetailPage : ViewBasePage
    {
        public MediaDetailPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = Ioc.Default.GetRequiredService<MediaDetailViewModel>();
        }

        private MediaDetailViewModel _viewModel { get; set; }

        private void daMediaElement_MediaFailed(object sender, EventArgs e)
        {
            // TODO
            // (sender as MediaElement).Source = null;

            ErrorInfo argDetail = new()
            {
                    { "Type", "Media Element" },
                    { "e", e.ToString() },
                };

            argDetail.ErrorArea = "Error displaying Media Element";

            Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(argDetail);

            // TODO Handle when can not play video better
        }

        private void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Border? theFrame = sender as Border;
            IMediaModel theModel = (theFrame.BindingContext as MediaDetailViewModel).CurrentMediaObject;

            OpenFileRequest t = new(theModel.GDescription, new ReadOnlyFile(theModel.MediaStorageFilePath));
            _ = Launcher.OpenAsync(t);
        }
    }
}