// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.HLinks;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

namespace GrampsView.UserControls
{
    public class UControlTemplateBase : ContentView
    {

        public void OnTapGestureRecognizerTappedHandler(string ArgTemplateName, TappedEventArgs argEventArgs)
        {
            try
            {
                Ioc.Default.GetRequiredService<ILog>().Variable($"{ArgTemplateName} - OnTapGestureRecognizerTapped", argEventArgs.Parameter.ToString(), Microsoft.Extensions.Logging.LogLevel.Trace);

                Navigation.PushAsync((argEventArgs.Parameter as HLinkBase).NavigationPage());
            }
            catch (Exception ex)
            {
                ErrorInfo t = new ErrorInfo($"{ArgTemplateName}", "OnTapGestureRecognizerTapped")
                {
                    { "Type", argEventArgs.Parameter.GetType().ToString() },
                    { "Arg", argEventArgs.Parameter.ToString() }
                };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, t);
            }
        }
    }
}
