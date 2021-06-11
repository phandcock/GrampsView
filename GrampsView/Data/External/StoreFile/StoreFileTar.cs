namespace GrampsView.Data
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;

    using ICSharpCode.SharpZipLib.Tar;

    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// </summary>
    /// <seealso cref="GrampsView.Common.CommonBindableBase"/>
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.IStoreFile"/>
    public partial class StoreFile : CommonBindableBase, IStoreFile
    {
        /// <summary>
        /// Extracts the tar by entry.
        /// </summary>
        /// <param name="tarFileName">
        /// Name of the tar file.
        /// </param>
        /// <param name="targetDir">
        /// The target dir.
        /// </param>
        /// <param name="asciiTranslate">
        /// if set to <c>true</c> [ASCII translate].
        /// </param>
        public static void ExtractTarByEntry(string tarFileName, string targetDir, bool asciiTranslate)
        {
            using (FileStream fsIn = new FileStream(tarFileName, FileMode.Open, FileAccess.Read))
            {
                using (TarInputStream tarIn = new TarInputStream(fsIn, System.Text.Encoding.ASCII))
                {
                    TarEntry tarEntry;
                    while ((tarEntry = tarIn.GetNextEntry()) != null)
                    {
                        if (tarEntry.IsDirectory)
                        {
                            continue;
                        }

                        // Converts the unix forward slashes in the filenames to windows backslashes
                        string name = tarEntry.Name.Replace('/', Path.DirectorySeparatorChar);

                        // Remove any root e.g. '\' because a PathRooted filename defeats Path.Combine
                        if (Path.IsPathRooted(name))
                        {
                            name = name.Substring(Path.GetPathRoot(name).Length);
                        }

                        // Apply further name transformations here as necessary
                        string outName = Path.Combine(targetDir, name);

                        string directoryName = Path.GetDirectoryName(outName);
                        Directory.CreateDirectory(directoryName);       // Does nothing if directory exists

                        using (FileStream outStr = new FileStream(outName, FileMode.Create))
                        {
                            if (asciiTranslate)
                            {
                                CopyWithAsciiTranslate(tarIn, outStr);
                            }
                            else
                            {
                                tarIn.CopyEntryContents(outStr);
                            }
                        }

                        // outStr.Close(); Set the modification date/time. This approach seems to
                        // solve timezone issues.
                        DateTime myDt = DateTime.SpecifyKind(tarEntry.ModTime, DateTimeKind.Utc);
                        File.SetLastWriteTime(outName, myDt);
                    }

                    // tarIn.Close();
                }
            }
        }

        /// <summary>
        /// Extracts the tar archive.
        /// </summary>
        /// <param name="dataFolder">
        /// The data folder.
        /// </param>
        /// <param name="tarIn">
        /// The tar in.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task ExtractTarArchive(TarInputStream tarIn)
        {
            await ExtractTarIfNotModified(tarIn, false).ConfigureAwait(false);
        }

        /// <summary>
        /// Extracts the tared files from the archive.
        /// NOTE: This is not the file last modified date but if the file has been modified then it
        /// should be later than any UnTared file date.
        /// </summary>
        /// <param name="dataFolder">
        /// The data folder.
        /// </param>
        /// <param name="tarIn">
        /// The tar in.
        /// </param>
        /// <param name="asciiTranslate">
        /// if set to <c>true</c> [ASCII translate].
        /// </param>
        /// <returns>
        /// True if the file is UnTARed correctly.
        /// </returns>
        public async Task ExtractTarIfNotModified(TarInputStream tarIn, bool asciiTranslate)
        {
            if (tarIn is null)
            {
                throw new ArgumentNullException(nameof(tarIn));
            }

            TarEntry tarEntry = null;

            //FileInfo outFile = null;

            try
            {
                while ((tarEntry = tarIn.GetNextEntry()) != null)
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
                        tarName = tarName.Substring(Path.GetPathRoot(tarName).Length);
                    }

                    // Apply further name transformations here as necessary
                    string filename = Path.GetFileName(tarName);

                    string outName = Path.Combine(DataStore.Instance.AD.CurrentDataFolder.Path, tarName);

                    string relativePath = Path.GetDirectoryName(tarEntry.Name);

                    string directoryName = Path.GetDirectoryName(outName);

                    DirectoryInfo newFolder = null;

                    if (relativePath.Length > 0)
                    {
                        newFolder = Directory.CreateDirectory(Path.Combine(DataStore.Instance.AD.CurrentDataFolder.Path, relativePath));
                    }
                    else
                    {
                        newFolder = DataStore.Instance.AD.CurrentDataFolder.Value;
                    }

                    // Check if the folder was created successfully.
                    if (newFolder == null)
                    {
                        // Skip this file.
                        continue;
                    }

                    // Check file modification date if it exists
                    bool okToCopyFlag = true;

                    //// Android uses mimetypes and a type for .gramps files is not in the list TODO
                    //// work how how to add it to the list .gramps are .gz to just rename for now
                    //if (filename == "data.gramps")
                    //{
                    //    filename = CommonConstants.StorageGRAMPSFileName;
                    //}

                    // if tarEntry modtime is less than outFile datemodified
                    // NOTE: This is not the file last modified date but if the file has been
                    // modified then it should be later than any UnTared file date
                    if (await StoreFolder.FolderFileExistsAsync(newFolder, filename).ConfigureAwait(false))
                    {
                        FileInfoEx newFileName = StoreFolder.FolderGetFile(filename, newFolder);

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
                        if (filename == CommonConstants.StorageGRAMPSFileName)
                        {
                        }

                        await DataStore.Instance.CN.DataLogEntryReplace($"UnTaring file {tarEntry.Name}");

                        Stream outStr = await StoreFolder.FolderCreateFileAsync(newFolder, filename).ConfigureAwait(false);

                        if (asciiTranslate)
                        {
                            CopyWithAsciiTranslate(tarIn, outStr);
                        }
                        else
                        {
                            tarIn.CopyEntryContents(outStr);
                        }

                        outStr.Flush();
                        outStr.Dispose();

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
                        // DataStore.Instance.CN.DataLogEntryAdd("File "
                        // + tarEntry.Name + " does not need to be unTARed as its modified date is
                        // earlier than the one in the output folder").ConfigureAwait(false);
                    }

                    // Check file ceated successfully
                    bool checkFileExistsFlag = await StoreFolder.FolderFileExistsAsync(newFolder, filename).ConfigureAwait(false);

                    if (!checkFileExistsFlag)
                    {
                        ErrorInfo t = new ErrorInfo("Error UnTaring file. File not created.  Perhaps the path is too long?")
                                {
                                    { "New Folder",  newFolder.FullName },
                                    { "Filename",  filename },
                                };

                        DataStore.Instance.CN.NotifyError(t);

                        // TODO copy dummy file in its place
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle disk full errors
                const int HR_ERROR_HANDLE_DISK_FULL = unchecked((int)0x80070027);
                const int HR_ERROR_DISK_FULL = unchecked((int)0x80070070);

                if (ex.HResult == HR_ERROR_HANDLE_DISK_FULL
                    || ex.HResult == HR_ERROR_DISK_FULL)
                {
                    DataStore.Instance.CN.NotifyException("UnTar Disk Full Exception working on " + tarEntry.Name, ex);
                }

                // Handle other errors
                if (tarEntry != null)
                {
                    DataStore.Instance.CN.NotifyException("UnTar Exception working on " + tarEntry.Name, ex);
                    throw;
                }
                else
                {
                    DataStore.Instance.CN.NotifyException("UnTar tarEntry null Exception ", ex);
                    throw;
                }
            }
        }

        /// <summary>
        /// Copies the with ASCII translate.
        /// </summary>
        /// <param name="tarIn">
        /// The tar in.
        /// </param>
        /// <param name="outStream">
        /// The out stream.
        /// </param>
        private static void CopyWithAsciiTranslate(TarInputStream tarIn, Stream outStream)
        {
            byte[] buffer = new byte[4096];
            bool isAscii = true;
            bool cr = false;

            int numRead = tarIn.Read(buffer, 0, buffer.Length);
            int maxCheck = Math.Min(200, numRead);
            for (int i = 0; i < maxCheck; i++)
            {
                byte b = buffer[i];
                if (b < 8 || (b > 13 && b < 32) || b == 255)
                {
                    isAscii = false;
                    break;
                }
            }

            while (numRead > 0)
            {
                if (isAscii)
                {
                    // Convert LF without CR to CRLF. Handle CRLF split over buffers.
                    for (int i = 0; i < numRead; i++)
                    {
                        byte b = buffer[i]; // assuming plain Ascii and not UTF-16
                        if (b == 10 && !cr) // LF without CR
                        {
                            outStream.WriteByte(13);
                        }

                        cr = b == 13;

                        outStream.WriteByte(b);
                    }
                }
                else
                {
                    outStream.Write(buffer, 0, numRead);
                }

                numRead = tarIn.Read(buffer, 0, buffer.Length);
            }
        }
    }
}