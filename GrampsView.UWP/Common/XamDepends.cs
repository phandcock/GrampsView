using GrampsView.Common;
using GrampsView.UWP.Common;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageResource))]

namespace GrampsView.UWP.Common
{
    public class ImageResource : IImageResource
    {
        public Size GetSize(string fileName)
        {
            return Task.Run(async () =>

            {
                try
                {
                    StorageFile file = await StorageFile.GetFileFromPathAsync(fileName);

                    if (file != null)
                    {
                        StringBuilder outputText = new StringBuilder();

                        // Get image properties
                        ImageProperties imageProperties = await file.Properties.GetImagePropertiesAsync();

                        Size ttt = new Size(imageProperties.Width, imageProperties.Height);

                        return ttt;
                    }
                }

                // Handle errors with catch blocks
                catch (FileNotFoundException)
                {
                    //DataStore.CN.MajorStatusMessage( "UWP Size GetSize(string fileName)");

                    // TODO For example, handle a file not found error
                    return new Size(0, 0);
                }

                return new Size(0, 0);
            }).Result;
        }
    }
}