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
            base.OnAppearing();

            if (!(BindingContext is null))
            {
                (BindingContext as ViewModelBase).BaseHandleViewAppearingEventInternal();
            }
        }
    }
}