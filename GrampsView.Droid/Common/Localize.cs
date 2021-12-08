using Android.Graphics;

using GrampsView.Common;
using GrampsView.Droid;

using System;
using System.Globalization;
using System.Threading;
using System.Diagnostics.Contracts;

using Xamarin.Forms;

[assembly: Dependency(typeof(Localize))]
[assembly: Dependency(typeof(ImageResource))]

namespace GrampsView.Droid
{
    public class ImageResource : Java.Lang.Object, IImageResource
    {
        public Size GetSize(string fileName)
        {
            var options = new BitmapFactory.Options
            {
                InJustDecodeBounds = true
            };

            //fileName = fileName.Replace('-', '_').Replace(".png", "").Replace(".jpg", "");
            //var resId = Forms.Context.Resources.GetIdentifier(fileName, "drawable", Forms.Context.PackageName);
            //BitmapFactory.DecodeResource(Forms.Context.Resources, resId, options);

            BitmapFactory.DecodeFile(fileName, options);

            Size outArg = new Size((double)options.OutWidth, (double)options.OutHeight);

            options.Dispose();

            return outArg;
        }
    }

    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            string netLanguage; // = "en";
            var androidLocale = Java.Util.Locale.Default;
            netLanguage = AndroidToDotnetLanguage(androidLocale.ToString().Replace("_", "-"));

            // TODO this gets called a lot - try/catch can be expensive so consider caching or something
            CultureInfo ci;

            try
            {
                ci = new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException e1)
            {
                // iOS locale not valid .NET culture (eg. "en-ES" : English in Spain) fallback to
                // first characters, in this case "en"
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                    Console.WriteLine(netLanguage + " failed, trying " + fallback + " (" + e1.Message + ")");
                    ci = new CultureInfo(fallback);
                }
                catch (CultureNotFoundException e2)
                {
                    // iOS language not valid .NET culture, falling back to English
                    Console.WriteLine(netLanguage + " couldn't be set, using 'en' (" + e2.Message + ")");
                    ci = new CultureInfo("en");
                }
            }

            return ci;
        }

        public void SetLocale(CultureInfo ci)
        {
            Contract.Assert(ci != null);

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            Console.WriteLine("CurrentCulture set: " + ci.Name);
        }

        private string AndroidToDotnetLanguage(string androidLanguage)
        {
            Console.WriteLine("Android Language:" + androidLanguage);
            var netLanguage = androidLanguage;

            //certain languages need to be converted to CultureInfo equivalent
            switch (androidLanguage)
            {
                case "in-ID":  // "Indonesian (Indonesia)" has different code in  .NET
                    netLanguage = "id-ID"; // correct code for .NET
                    break;

                case "gsw-CH":  // "Schwiizertüütsch (Swiss German)" not supported .NET culture
                    netLanguage = "de-CH"; // closest supported
                    break;

                    // add more application-specific cases here (if required) ONLY use cultures that
                    // have been tested and known to work
            }

            Console.WriteLine(".NET Language/Locale:" + netLanguage);
            return netLanguage;
        }

        private string ToDotnetFallbackLanguage(PlatformCulture platCulture)
        {
            Console.WriteLine(".NET Fallback Language:" + platCulture.LanguageCode);
            var netLanguage = platCulture.LanguageCode; // use the first part of the identifier (two chars, usually);

            switch (platCulture.LanguageCode)
            {
                case "gsw":
                    netLanguage = "de-CH"; // equivalent to German (Switzerland) for this app
                    break;

                    // add more application-specific cases here (if required) ONLY use cultures that
                    // have been tested and known to work
            }

            Console.WriteLine(".NET Fallback Language/Locale:" + netLanguage + " (application-specific)");
            return netLanguage;
        }
    }
}