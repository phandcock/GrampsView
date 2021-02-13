namespace GrampsView.iOS.Common
{
    using AVFoundation;

    using Foundation;

    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;

    using System.IO;
    using System.Threading.Tasks;

    using UIKit;

    internal class PlatformSpecific : IPlatformSpecific
    {
        //public PlatformSpecific(IEventAggregator iocEventAggregator)
        //{
        //}

        public async Task ActivityTimeLineAdd(PersonModel argPersonModel)
        {
            // No Timeline functionality so ignore this

            return;
        }

        public async Task ActivityTimeLineAdd(FamilyModel argFamilyModel)
        {
            // No Timeline functionality so ignore this
        }

        public Stream GenerateThumbImageFromVideo(string filePath, long millisecond)
        {
            var asset = AVAsset.FromUrl(NSUrl.FromFilename(filePath));
            var imageGenerator = AVAssetImageGenerator.FromAsset(asset);
            imageGenerator.AppliesPreferredTrackTransform = true;

            var actualTime = asset.Duration;

            CoreMedia.CMTime cmTime = new CoreMedia.CMTime(millisecond, 1000000);

            NSError error;

            var cgImage = imageGenerator.CopyCGImageAtTime(cmTime, out actualTime, out error);
            return new UIImage(cgImage).AsJPEG().AsStream();
        }
    }
}