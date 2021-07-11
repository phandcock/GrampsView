namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Xamarin.Forms;

    public class ViewBase : ContentPage
    {
        private double height;

        private double width;

        public ViewBase()
        {
        }

        protected override void OnAppearing()
        {
            (this.BindingContext as ViewModelBase).BaseHandleAppearingEventInternal();
        }

        protected override void OnDisappearing()
        {
            (this.BindingContext as ViewModelBase).BaseHandleDisAppearingEventInternal();
        }
    }
}