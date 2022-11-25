using System.Text;

using Windows.Storage;
using Windows.Storage.FileProperties;

namespace GrampsView.Common
{
    public partial class ImageResource : IImageResource
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
                        StringBuilder outputText = new();

                        // Get image properties
                        ImageProperties imageProperties = await file.Properties.GetImagePropertiesAsync();

                        Size ttt = new(imageProperties.Width, imageProperties.Height);

                        return ttt;
                    }
                }

                // Handle errors with catch blocks
                catch (FileNotFoundException)
                {
                    //DataStore.Instance.CN.MajorStatusMessage( "UWP Size GetSize(string fileName)");

                    // TODO For example, handle a file not found error
                    return new Size(0, 0);
                }
                catch (Exception)
                {
                    throw;
                }

                return new Size(0, 0);
            }).Result;
        }
    }
}