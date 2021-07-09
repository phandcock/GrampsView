namespace GrampsView.Views
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;
    using GrampsView.ViewModels;

    using Xamarin.Essentials;
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

        /// <summary>
        /// Indicates that the <see cref="T:Xamarin.Forms.Page"/> has been assigned a size.
        /// </summary>
        /// <param name="width">
        /// The width allocated to the <see cref="T:Xamarin.Forms.Page"/>.
        /// </param>
        /// <param name="height">
        /// The height allocated to the <see cref="T:Xamarin.Forms.Page"/>.
        /// </param>
        /// <remarks>
        /// Height and Width only set when created or rotated in xamarin
        /// </remarks>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called

            var t = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo;

            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;

                //reconfigure layout
                if (width > height)
                {
                    DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Landscape;
                    CardSizes.Current.ReCalculateCardWidths(width, height);
                }
                else
                {
                    DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Portrait;
                    CardSizes.Current.ReCalculateCardWidths(height, width);
                }
            }

            // TODO Handle UWP windows resize
        }
    }
}