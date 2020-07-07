using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

using Xamarin.Forms;

namespace GrampsView.UWP.Common
{
    //internal class XamarinUtil
    //{
    //    public async Task<Image> ConvertBitmapToXamarinImage(WriteableBitmap bitmap)
    //    {
    //        byte[] pixels;

    //        using (var stream = bitmap.PixelBuffer.AsStream())
    //        {
    //            pixels = new byte[(uint)stream.Length];
    //            await stream.ReadAsync(pixels, 0, pixels.Length);
    //        }

    //        var raStream = new InMemoryRandomAccessStream();

    //        // Encode pixels into stream
    //        var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, raStream);
    //        encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied, (uint)bitmap.PixelWidth, (uint)bitmap.PixelHeight, 96, 96, pixels);
    //        await encoder.FlushAsync();

    //        return new Image() { Source = ImageSource.FromStream(() => raStream.AsStreamForRead()) };
    //    }
    //}
}