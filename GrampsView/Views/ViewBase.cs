namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Xamarin.Forms;

    public class ViewBase : ContentPage
    {
        public ViewBase()
        {
        }

        protected override void OnAppearing()
        {
            if (!(this.BindingContext is null))
            {
                (this.BindingContext as ViewModelBase).BaseHandleAppearingEventInternal();
            }
        }

        protected override void OnDisappearing()
        {
            if (!(this.BindingContext is null))
            {
                (this.BindingContext as ViewModelBase).BaseHandleDisAppearingEventInternal();
            }
        }
    }
}