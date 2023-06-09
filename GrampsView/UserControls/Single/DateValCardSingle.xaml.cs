// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.HLinks;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

namespace GrampsView.UserControls
{
    public partial class DateValCardSingle : SmallCardControlTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateValCardSingle"/> class.
        /// </summary>
        public DateValCardSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            try
            {
                Ioc.Default.GetRequiredService<ILog>().Variable("DateValCardSingle-OnTapGestureRecognizerTapped", args.Parameter.ToString(), Microsoft.Extensions.Logging.LogLevel.Trace);

                Navigation.PushAsync((args.Parameter as HLinkBase).NavigationPage());
            }
            catch (Exception ex)
            {
                ErrorInfo t = new ErrorInfo("DateValCardSingle", "OnTapGestureRecognizerTapped")
                {
                    { "Type", args.Parameter.GetType().ToString() },
                    { "Arg", args.Parameter.ToString() }
                };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, t);
            }
        }
    }
}