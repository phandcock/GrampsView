namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Xamarin.Forms;

    public class ViewBase : ContentPage
    {
        protected override void OnAppearing()
        {
            (this.BindingContext as ViewModelBase).InternalOnAppearing();
        }
    }
}