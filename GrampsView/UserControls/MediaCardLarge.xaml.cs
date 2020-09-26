// <copyright file="MediaCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;

    using Xamarin.Forms;

    /// <summary>
    /// Media Control Large code behind.
    /// </summary>
    public partial class MediaCardLarge : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaCardLarge"/> class.
        /// </summary>
        public MediaCardLarge()
        {
            InitializeComponent();
        }

        private void MediaCardLargeRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
            MediaCardLarge thisObject = sender as MediaCardLarge;

            HLinkMediaModel thisMedia;

            if (thisObject.BindingContext is null)
            {
                (sender as MediaCardLarge).BindingContext = new HLinkMediaModel();
            }
            else
            {
                thisMedia = thisObject.BindingContext as HLinkMediaModel;

                if ((thisObject != null) && (thisMedia != null))
                {
                    thisObject.AnchorImage.BindingContext = thisMedia.DeRef.HomeImageHLink;
                }
            }
        }
    }
}