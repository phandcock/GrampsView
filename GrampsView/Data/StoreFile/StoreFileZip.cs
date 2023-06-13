// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.Interfaces;
using GrampsView.Data.Repository;
using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Interfaces;

using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System.IO.Compression;

namespace GrampsView.Data.StoreFile
{
    public class StoreFileZip : ObservableObject, IStoreFileZip
    {
        /// <summary>
        /// This routine is heavily customized to decompress GRAMPS whole of database export file
        /// (i.e. .gramps) files.
        /// </summary>
        /// <param name="inputFile">
        /// Input GRAMPS export file.
        /// </param>
        /// <returns>
        /// Flag indicating success or not.
        /// </returns>
        public bool DecompressGZIP(IFileInfoEx inputFile)
        {
            Ioc.Default.GetRequiredService<ILog>().DataLogEntryAdd("Decompressing GRAMPS GZIP file");

            // Check arguments
            if (inputFile == null)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("The input file is null"));
                return false;
            }

            try
            {
                IStoreFileZip t = new StoreFileZip();
                _ = t.ExtractGZip(inputFile, "data.xml");

                Ioc.Default.GetRequiredService<ILog>().DataLogEntryReplace("GRAMPS GZIP file decompress complete");
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                ErrorInfo t = new("Unauthorised Access exception when trying to acess file")
                    {
                        { "Exception Message ", ex.Message },
                    };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(t);
                return false;
            }
        }

        /// <summary>
        /// Extracts the gzip file.
        /// </summary>
        /// <param name="argInputFile">
        /// The input file.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<bool> ExtractGZip(IFileInfoEx argInputFile, string argOutFile)
        {
            if (argInputFile is null)
            {
                throw new ArgumentNullException(nameof(argInputFile));
            }

            FileStream originalFileStream = argInputFile.FInfo.OpenRead();

            byte[] dataBuffer = new byte[4096];

            GZipStream gzipStream = new(originalFileStream, CompressionMode.Decompress);

            FileInfo fsOut = new(Path.Combine(DataStore.Instance.AD.CurrentDataFolder.FolderAsString, argOutFile));

            FileStream fsOut1 = fsOut.Create();

            StreamUtils.Copy(gzipStream, fsOut1, dataBuffer);
            fsOut1.Flush();

            fsOut1.Dispose();
            gzipStream.Dispose();

            return true;
        }

        /// <summary>Extracts the first image from a zip file.</summary>
        /// <param name="argCurrentDataFolder"></param>
        /// <param name="argExistingMediaModel"></param>
        /// <param name="argNewMediaModel"></param>
        public IMediaModel ExtractZipFileFirstImage(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel)
        {
            ICSharpCode.SharpZipLib.Zip.ZipFile? zf = null;
            try
            {
                FileStream fs = File.OpenRead(argExistingMediaModel.CurrentStorageFile.GetAbsoluteFilePath);
                zf = new ICSharpCode.SharpZipLib.Zip.ZipFile(fs);

                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;           // Ignore directories
                    }

                    string entryFileName = zipEntry.Name;

                    // check for image TODO do proper mimetype mapping. See https://github.com/samuelneff/MimeTypeMap
                    if (SharedSharpGeneral.MimeMimeTypeGet(CommonRoutines.MimeFileContentTypeGet(Path.GetExtension(zipEntry.Name))) != "image")
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
                        using (FileStream streamWriter = File.Create(Path.Combine(argCurrentDataFolder.FullName, argNewMediaModel.OriginalFilePath)))
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
                ErrorInfo t = new("Directory not found when trying to create image from ZIP file")
                                 {
                                     { "Original ID", argExistingMediaModel.Id },
                                     { "Original File", argExistingMediaModel.OriginalFilePath },
                                     { "Clipped Id", argNewMediaModel.Id },
                                     { "New path", "pdfimage" }
                                 };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, t);

                return new MediaModel();
            }
            catch (Exception ex)
            {
                ErrorInfo t = new("Exception when trying to create image from ZIP file")
                                 {
                                     { "Original ID", argExistingMediaModel.Id },
                                     { "Original File", argExistingMediaModel.OriginalFilePath },
                                     { "Clipped Id", argNewMediaModel.Id }
                                 };

                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, t);

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