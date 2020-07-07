using GrampsView.UserControls;
using GrampsView.UserControls.iOS.Renderers;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]

namespace GrampsView.UserControls.iOS.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer

    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Control.Layer.BorderWidth = 0;
        }
    }
}