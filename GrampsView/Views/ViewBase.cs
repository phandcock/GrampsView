namespace GrampsView.Views
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;
    using GrampsView.ViewModels;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class ViewBase : ContentPage
    {
        private double height = 0;

        private double width = 0;

        protected override void OnAppearing()
        {
            (this.BindingContext as ViewModelBase).BaseHandleAppearingEventInternal();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;

                //reconfigure layout
                if (width > height)
                {
                    DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Landscape;
                }
                else
                {
                    DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Portrait;
                }

                CardSizes.Current.ReCalculateCardWidths();
            }
        }
    }
}