namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    internal class FlexPlus : FlexLayout
    {
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            FlexPlus tt = this;

            tt.HeightRequest = 300;

            SizeRequest t = base.OnMeasure(widthConstraint, heightConstraint);

            return t;
        }
    }
}