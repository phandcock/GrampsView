namespace GrampsView.Droid.Common
{
    using Android.Media;

    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;

    using System.IO;
    using System.Threading.Tasks;

    using Stream = System.IO.Stream;

    internal class PlatformSpecific : IPlatformSpecific
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        public async Task ActivityTimeLineAdd(PersonModel argPersonModel)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            // No Timeline functionality so ignore this
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        public async Task ActivityTimeLineAdd(FamilyModel argFamilyModel)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            // No Timeline functionality so ignore this
        }

        public Stream GenerateThumbImageFromVideo(string filePath, long millisecond)
        {
            MediaMetadataRetriever retriever = new MediaMetadataRetriever();
            retriever.SetDataSource(filePath);
            Android.Graphics.Bitmap bitmap = retriever.GetFrameAtTime(millisecond);
            if (bitmap != null)
            {
                MemoryStream stream = new MemoryStream();
                bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 80, stream);
                return stream;
            }
            return null;
        }
    }
}