using GrampsView.UserControls;
using GrampsView.UserControls.iOS.Renderers;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEditor), typeof(BorderlessEditorRenderer))]

namespace GrampsView.UserControls.iOS.Renderers
{
    public class BorderlessEditorRenderer : EditorRenderer

    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Control.Layer.BorderWidth = 0;
        }
    }
}