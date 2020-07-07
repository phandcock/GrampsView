namespace GrampsView.Common
{
    using GrampsView.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class LabelExtended : Label
    {
        public LabelExtended()
        {
            this.IsEnabled = true;

            var tapGestureRecognizer = new TapGestureRecognizer();

            tapGestureRecognizer.Tapped += async (s, e) =>
           {
               LabelExtended thisControl = s as LabelExtended;

               // handle the tap
               string theText = thisControl.Text is null ? thisControl.FormattedText.ToString() : thisControl.Text;

               await Clipboard.SetTextAsync(theText).ConfigureAwait(false);

               await DataStore.CN.MajorStatusAdd("Text copied").ConfigureAwait(false);
           };

            this.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}