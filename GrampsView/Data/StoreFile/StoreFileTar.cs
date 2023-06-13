// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Common.Interfaces;
using GrampsView.Data.Repository;

using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

namespace GrampsView.Data.StoreFile
{
    /// <summary>
    /// </summary>

    public class StoreFileTar : ObservableObject, IStoreFileTar
    {
        /// <summary>
        /// This routine is heavily customized to decompress GRAMPS whole of database export file
        /// (i.e. .gramps) files.
        /// </summary>
        /// <returns>
        /// Flag indicating success or not.
        /// </returns>
        public async Task<bool> DecompressTAR()
        {
            Ioc.Default.GetRequiredService<ILog>().DataLogEntryAdd("Decompressing GRAMPS TAR files");

            // Check arguments
            if (!DataStore.Instance.AD.CurrentInputStreamValid)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyAlert("The input file is invalid");
                return false;
            }

            // Register encodings
            // See https://stackoverflow.com/questions/50858209/system-notsupportedexception-no-data-is-available-for-encoding-1252
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            Stream originalFileStream = DataStore.Instance.AD.CurrentInputStream;

            // open the gzip and extract the tar file
            using (Stream stream = new GZipInputStream(originalFileStream))
            {
                using TarInputStream tarIn = new(stream, System.Text.Encoding.ASCII);

                StoreFileTar t = new StoreFileTar();

                // TODO DO NOT AWAIT as causes thread blocking await
                await t.ExtractTar(tarIn).ConfigureAwait(false);
            }

            Ioc.Default.GetRequiredService<ILog>().DataLogEntryReplace("UnTaring of files complete");
            return true;
        }

        /// <summary>
        /// Extracts the tared files from the archive.
        /// NOTE: This is not the file last modified date but if the file has been modified then it
        /// should be later than any UnTared file date.
        /// </summary>
        /// <param name="tarIn">
        /// The tar in.
        /// </param>
        /// <returns>
        /// True if the file is UnTARed correctly.
        /// </returns>
        public async Task<bool> ExtractTar(TarInputStream tarIn)
        {
            if (tarIn is null)
            {
                throw new ArgumentNullException(nameof(tarIn));
            }

            TarEntry tarEntry = tarIn.GetNextEntry();

            try
            {
                while (tarEntry != null)
                {
                    if (tarEntry.IsDirectory)
                    {
                        continue;
                    }

                    // Debug.WriteLine("Untaring " + tarEntry.Name);
                    string outFileName = Path.GetFileName(tarEntry.Name);

                    // Converts the unix forward slashes in the filenames to windows backslashes
                    string tarName = tarEntry.Name.Replace('/', Path.DirectorySeparatorChar);

                    // Remove any root e.g. '\' because a PathRooted filename defeats Path.Combine
                    if (Path.IsPathRooted(tarName))
                    {
                        tarName = tarName[Path.GetPathRoot(tarName).Length..];
                    }

                    // Apply further name transformations here as necessary
                    string filename = Path.GetFileName(tarName);

                    string outName = Path.Combine(DataStore.Instance.AD.CurrentDataFolder.FolderAsString, tarName);

                    string relativePath = Path.GetDirectoryName(tarEntry.Name);

                    string directoryName = Path.GetDirectoryName(outName);

                    // Check file modification date if it exists
                    bool okToCopyFlag = true;

                    //// Android uses mimetypes and a type for .gramps files is not in the list TODO
                    //// work how how to add it to the list .gramps are .gz to just rename for now
                    //if (filename == "data.gramps")
                    //{
                    //    filename = Constants.StorageGRAMPSFileName;
                    //}

                    IFileInfoEx newFileName = new FileInfoEx(argFileName: filename, argRelativeFolder: relativePath);

                    // if tarEntry modtime is less than outFile datemodified
                    // NOTE: This is not the file last modified date but if the file has been
                    // modified then it should be later than any UnTared file date
                    if (newFileName.Exists)
                    {
                        //if (filename == "1024x768.png")
                        //{
                        //}

                        if (newFileName.Valid)
                        {
                            // TODO Check this compare date and tiem TODO Add delete existing files
                            // option before extract
                            if (tarEntry.ModTime.CompareTo(newFileName.FInfo.LastAccessTime) < 0)
                            {
                                okToCopyFlag = false;
                            }

                            if (newFileName.FInfo.LastWriteTimeUtc == tarEntry.ModTime)
                            {
                                okToCopyFlag = false;
                            }
                        }
                    }

                    if (okToCopyFlag)
                    {
                        if (filename == Constants.StorageGRAMPSFileName)
                        {
                        }

                        Ioc.Default.GetRequiredService<ILog>().DataLogEntryAdd($"UnTaring file {tarEntry.Name}");

                        using Stream outStr = GrampsView.Data.StoreFolder.StoreFolder.FolderCreateFile(newFileName.FInfo.Directory, filename);
                        try
                        {
                            tarIn.CopyEntryContents(outStr);

                            Thread.Sleep(500);
                        }
                        catch (Exception ex)
                        {
                            Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("UnTar issue", ex);
                        }

                        // TODO check of modification date kept Set the modification date/time. This
                        // approach seems to solve timezone issues.

                        // outFile = await newFolder.TryGetItemAsync(filename);
                        //// StorageFile tt = await newFolder.GetFileAsync(filename);

                        // IDictionary<string, object> ttt = await
                        // tt.Properties.RetrievePropertiesAsync(new List<string> { });

                        // foreach (var item in ttt) { Debug.WriteLine(item); }

                        // BasicProperties outFileProperties = await outFile.GetBasicPropertiesAsync();

                        // var fileProperties = await file.Properties.RetrievePropertiesAsync( new
                        // List { "System.GPS.Latitude", "System.GPS.Longitude" });

                        // var fileProperties = await outFileProperties.RetrievePropertiesAsync(new
                        // List { });

                        // var propertyToSave = new List> { new
                        // KeyValuePair("System.Photo.LensManufacturer", "Pentax") };

                        // await file.Properties.SavePropertiesAsync(propertyToSave);

                        // var changes = new List<KeyValuePair<string, object>>();

                        // DateTime modDate = tarEntry.ModTime;

                        // PropertySet t4 = new PropertySet();

                        // t4.Add("DateModified", modDate);

                        // changes.Add(new KeyValuePair<string, object>("System.ExpandoProperties", t4));

                        // await tt.Properties.SavePropertiesAsync(changes);

                        // changes.Add(new KeyValuePair<string, object>("System.ExpandoProperties", modDate));

                        // await tt.Properties.SavePropertiesAsync(ttt);

                        // DateTime myDt = DateTime.SpecifyKind(tarEntry.ModTime, DateTimeKind.Utc);

                        // t = null;

                        // File.SetLastWriteTime(outName, modDate);

                        // .SetLastWriteTime(t.CreateSafeFileHandle(), modDate);
                    }
                    else
                    {
                        // TODO write to the output log // await
                        // Ioc.Default.GetRequiredService<IErrorNotifications>().DataLogEntryAdd("File "
                        // + tarEntry.Name + " does not need to be unTARed as its modified date is
                        // earlier than the one in the output folder").ConfigureAwait(false);
                    }

                    // Check file ceated successfully
                    bool checkFileExistsFlag = newFileName.Exists;

                    if (!checkFileExistsFlag)
                    {
                        ErrorInfo t = new("Error UnTaring file. File not created.  Perhaps the path is too long?")
                                {
                                    { "New Folder",  newFileName.FInfo.FullName },
                                    { "Filename",  filename },
                                };

                        Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(t);

                        // TODO copy dummy file in its place
                    }

                    // Get the next
                    tarEntry = tarIn.GetNextEntry();
                }
            }
            catch (Exception ex)
            {
                // Handle disk full errors
                const int HR_ERROR_HANDLE_DISK_FULL = unchecked((int)0x80070027);
                const int HR_ERROR_DISK_FULL = unchecked((int)0x80070070);

                if (ex.HResult is HR_ERROR_HANDLE_DISK_FULL
                    or HR_ERROR_DISK_FULL)
                {
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("UnTar Disk Full Exception working on " + tarEntry.Name, ex);

                    // No recovery from this
                }

                // Handle other errors
                if (tarEntry != null)
                {
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("UnTar Exception working on " + tarEntry.Name, ex);

                    // Keep going
                    // throw;
                }
                else
                {
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("UnTar tarEntry null Exception ", ex);
                    // Keep going
                    // throw;
                }
            }

            return true;
        }
    }
}