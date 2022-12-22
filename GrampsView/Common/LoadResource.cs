using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System.Diagnostics;

namespace GrampsView.Common
{
    public static partial class CommonRoutines
    {
        public static async Task<string> LoadResource(string argResourceName)
        {
            string returnValue = string.Empty;

            try
            {
                // Load Resource
                using Stream stream = await FileSystem.Current.OpenAppPackageFileAsync(argResourceName);
                if (stream is null)
                {
                    Debug.WriteLine($"LoadResource - Stream is Null");
                }
                else
                {


                    //  Debug.WriteLine($"LoadResource - Stream Length {stream.Length}");
                    using StreamReader reader = new(stream);
                    returnValue = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex,
                                           new ErrorInfo("Error trying to load resource")
                                                   {
                                                            { "File Name", argResourceName },
                                                   }
                                           );
            }

            return returnValue;
        }
    }
}