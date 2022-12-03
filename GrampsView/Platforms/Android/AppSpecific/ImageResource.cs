using Android.Graphics;

using GrampsView.Common.Interfaces;

namespace GrampsView.Common
{
    public partial class ImageSize : Java.Lang.Object, IImageSize
    {
        public Size GetSize(string fileName)
        {
            BitmapFactory.Options options = new()
            {
                InJustDecodeBounds = true
            };

            //fileName = fileName.Replace('-', '_').Replace(".png", "").Replace(".jpg", "");
            //var resId = Forms.Context.Resources.GetIdentifier(fileName, "drawable", Forms.Context.PackageName);
            //BitmapFactory.DecodeResource(Forms.Context.Resources, resId, options);

            _ = BitmapFactory.DecodeFile(fileName, options);

            Size outArg = new(options.OutWidth, options.OutHeight);

            options.Dispose();

            return outArg;
        }
    }
}