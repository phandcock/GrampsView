namespace GrampsView.Data
{
    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using ICSharpCode.SharpZipLib.Core;
    using ICSharpCode.SharpZipLib.Zip;

    using Microsoft.Extensions.DependencyInjection;

    using SharedSharp.Errors;

    using SharedSharpNu.Interfaces;

    using System;
    using System.IO;
    using System.IO.Compression;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// </summary>
    /// <seealso cref="Common.ObservableObject"/>
    /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="IStoreFile"/>
    public partial class StoreFile : ObservableObject, IStoreFile
    {
        /// <summary>
        /// Extracts the gzip file.
        /// </summary>
        /// <param name="argInputFile">
        /// The input file.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool ExtractGZip(IFileInfoEx argInputFile, string argOutFile)
        {
            if (argInputFile is null)
            {
                throw new ArgumentNullException(nameof(argInputFile));
            }

            FileStream originalFileStream = argInputFile.FInfo.OpenRead();

            byte[] dataBuffer = new byte[4096];

            GZipStream gzipStream = new GZipStream(originalFileStream, CompressionMode.Decompress);

            FileInfo fsOut = new FileInfo(Path.Combine(DataStore.Instance.AD.CurrentDataFolder.Path, argOutFile));

            FileStream fsOut1 = fsOut.Create();

            StreamUtils.Copy(gzipStream, fsOut1, dataBuffer);
            fsOut1.Flush();

            fsOut1.Dispose();
            gzipStream.Dispose();

            return true;
        }

        /// <summary>
        /// Extracts the first image from a zip file.
        /// </summary>
        /// <param name="archiveFilenameIn">
        /// The archive filename in.
        /// </param>
        /// <param name="outFolder">
        /// The out folder.
        /// </param>
        public static IMediaModel ExtractZipFileFirstImage(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel)
        {
            ICSharpCode.SharpZipLib.Zip.ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(argExistingMediaModel.MediaStorageFilePath);
                zf = new ICSharpCode.SharpZipLib.Zip.ZipFile(fs);

                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;           // Ignore directories
                    }

                    string entryFileName = zipEntry.Name;

                    // check for image TODO do proper mimetype mapping. See https://github.com/samuelneff/MimeTypeMap
                    if (SharedSharp.Common.SharedSharpGeneral.MimeMimeTypeGet(CommonRoutines.MimeFileContentTypeGet(Path.GetExtension(zipEntry.Name))) != "image")
                    {
                        continue;
                    }
                    else
                    {
                        // set extension
                        argNewMediaModel.OriginalFilePath = Path.ChangeExtension(argNewMediaModel.OriginalFilePath, Path.GetExtension(zipEntry.Name));

                        // Unzip the file
                        byte[] buffer = new byte[4096];     // 4K is optimum
                        Stream zipStream = zf.GetInputStream(zipEntry);

                        // Unzip file in buffered chunks. This is just as fast as unpacking to a
                        // buffer the full size of the file, but does not waste memory. The "using"
                        // will close the stream even if an exception occurs.
                        using (FileStream streamWriter = File.Create(System.IO.Path.Combine(argCurrentDataFolder.FullName, argNewMediaModel.OriginalFilePath)))
                        {
                            StreamUtils.Copy(zipStream, streamWriter, buffer);
                        }

                        // exit early
                        return argNewMediaModel;
                    }
                }

                fs.Close();

                // Exit
                return new MediaModel();
            }
            catch (DirectoryNotFoundException ex)
            {
                ErrorInfo t = new ErrorInfo("Directory not found when trying to create image from ZIP file")
                                 {
                                     { "Original ID", argExistingMediaModel.Id },
                                     { "Original File", argExistingMediaModel.MediaStorageFilePath },
                                     { "Clipped Id", argNewMediaModel.Id },
                                     { "New path", "pdfimage" }
                                 };

                App.Current.Services.GetService<IErrorNotifications>().NotifyException("PDF to Image", ex, t);

                return new MediaModel();
            }
            catch (Exception ex)
            {
                ErrorInfo t = new ErrorInfo("Exception when trying to create image from ZIP file")
                                 {
                                     { "Original ID", argExistingMediaModel.Id },
                                     { "Original File", argExistingMediaModel.MediaStorageFilePath },
                                     { "Clipped Id", argNewMediaModel.Id }
                                 };

                App.Current.Services.GetService<IErrorNotifications>().NotifyException("PDF to Image", ex, t);

                return new MediaModel();
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources
                }
            }
        }
    }
}