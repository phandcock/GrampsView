namespace GrampsView.Data
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using ICSharpCode.SharpZipLib.Core;
    using ICSharpCode.SharpZipLib.GZip;
    using ICSharpCode.SharpZipLib.Zip;

    using System;
    using System.IO;

    /// <summary>
    /// </summary>
    /// <seealso cref="GrampsView.Common.CommonBindableBase"/>
    /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.IStoreFile"/>
    public partial class StoreFile : CommonBindableBase, IStoreFile
    {
        /// <summary>
        /// Extracts the gzip file.
        /// </summary>
        /// <param name="argInputFile">
        /// The input file.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool ExtractGZip(FileInfoEx argInputFile)
        {
            if (argInputFile is null)
            {
                throw new ArgumentNullException(nameof(argInputFile));
            }

            FileStream originalFileStream = argInputFile.FInfo.OpenRead();

            byte[] dataBuffer = new byte[4096];

            GZipInputStream gzipStream = new GZipInputStream(originalFileStream);

            FileInfo fsOut = new FileInfo(Path.Combine(DataStore.Instance.AD.CurrentDataFolder.Path, "data.xml"));

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
        public static MediaModel ExtractZipFileFirstImage(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, MediaModel argNewMediaModel)
        {
            ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(argExistingMediaModel.MediaStorageFilePath);
                zf = new ZipFile(fs);

                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;           // Ignore directories
                    }

                    string entryFileName = zipEntry.Name;

                    // check for image TODO do proper mimetype mapping. See https://github.com/samuelneff/MimeTypeMap
                    if (CommonRoutines.MimeMimeTypeGet(CommonRoutines.MimeFileContentTypeGet(Path.GetExtension(zipEntry.Name))) != "image")
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
            catch (System.IO.DirectoryNotFoundException ex)
            {
                ErrorInfo t = new ErrorInfo("Directory not found when trying to create image from PDF file")
                                 {
                                     { "Original ID", argExistingMediaModel.Id },
                                     { "Original File", argExistingMediaModel.MediaStorageFilePath },
                                     { "Clipped Id", argExistingMediaModel.DeRef.Id },
                                     { "New path", "pdfimage" }
                                 };

                DataStore.Instance.CN.NotifyException("PDF to Image", ex, t);

                return new MediaModel();
            }
            catch (System.Exception ex)
            {
                ErrorInfo t = new ErrorInfo("Exception when trying to create image form PDF file")
                                 {
                                     { "Original ID", argExistingMediaModel.Id },
                                     { "Original File", argExistingMediaModel.MediaStorageFilePath },
                                     { "Clipped Id", argExistingMediaModel.DeRef.Id }
                                 };

                DataStore.Instance.CN.NotifyException("PDF to Image", ex, t);

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

        ///// <summary>
        ///// Extracts the zip file.
        ///// </summary>
        ///// <param name="archiveFilenameIn">
        ///// The archive filename in.
        ///// </param>
        ///// <param name="outFolder">
        ///// The out folder.
        ///// </param>
        //public static void ExtractZipFile(string archiveFilenameIn, string outFolder)
        //{
        //    ZipFile zf = null;
        //    try
        //    {
        //        FileStream fs = File.OpenRead(archiveFilenameIn);
        //        zf = new ZipFile(fs);

        // foreach (ZipEntry zipEntry in zf) { if (!zipEntry.IsFile) { continue; // Ignore
        // directories }

        // string entryFileName = zipEntry.Name;

        // // to remove the folder from the entry:- entryFileName = //
        // Path.GetFileName(entryFileName); Optionally match entrynames against a // selection list
        // here to skip as desired. The unpacked length is available in // the zipEntry.Size
        // property. byte[] buffer = new byte[4096]; // 4K is optimum Stream zipStream = zf.GetInputStream(zipEntry);

        // // Manipulate the output filename here as desired. string fullZipToPath =
        // Path.Combine(outFolder, entryFileName); string directoryName =
        // Path.GetDirectoryName(fullZipToPath); if (directoryName.Length > 0) {
        // Directory.CreateDirectory(directoryName); }

        // // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer // the
        // full size of the file, but does not waste memory. The "using" will close // the stream
        // even if an exception occurs. using (FileStream streamWriter = File.Create(fullZipToPath))
        // { StreamUtils.Copy(zipStream, streamWriter, buffer); } }

        //        fs.Close();
        //    }
        //    finally
        //    {
        //        if (zf != null)
        //        {
        //            zf.IsStreamOwner = true; // Makes close also shut the underlying stream
        //            zf.Close(); // Ensure we release resources
        //        }
        //    }
        //}
    }
}