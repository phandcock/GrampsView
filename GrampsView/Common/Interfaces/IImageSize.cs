namespace GrampsView.Common
{
    using Xamarin.Forms;

    public interface IImageResource
    {
        Size GetSize(string fileName);
    }
}