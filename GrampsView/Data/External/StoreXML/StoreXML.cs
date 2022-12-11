using GrampsView.Common;
using GrampsView.Data.External.StoreXML;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System.Xml;
using System.Xml.Linq;

namespace GrampsView.Data.ExternalStorage
{
    public partial class StoreXML : ObservableObject, IStoreXML
    {
        /// <summary>
        /// The default XML namespace.
        /// </summary>
        private static XNamespace ns = Constants.GrampsXMLNameSpace;

        /// <summary>
        /// local copy of GramsView Logging routines.
        /// </summary>
        private readonly ILog MyLog;

        private readonly IErrorNotifications MyNotifications;

        /// <summary>
        /// The Gramps XML document.
        /// </summary>
        private XDocument LocalGrampsXMLdoc = new();

        /// <summary>Initializes a new instance of the <see cref="StoreXML" /> class.</summary>
        /// <param name="iocLogging"></param>
        /// <param name="iocNotifications"></param>
        public StoreXML(ILog iocLogging, IErrorNotifications iocNotifications)
        {
            MyLog = iocLogging;

            MyNotifications = iocNotifications;


        }

        /// <summary>
        /// Loads the Gramps XML data.
        /// </summary>
        /// <returns>
        /// Flag if Gramps Data Only loaded successfully.
        /// </returns>
        public Task<bool> DataStorageLoadXML()
        {
            try
            {
                IFileInfoEx inputFile = new FileInfoEx(argFileName: Constants.StorageXMLFileName);

                Ioc.Default.GetRequiredService<ILog>().DataLogEntryAdd("Loading existing local copy of the GRAMPS data");
                {
                    XmlReaderSettings xmlReaderSettings = new()
                    {
                        DtdProcessing = DeviceInfo.Platform == DevicePlatform.Android
                        ? DtdProcessing.Ignore
                        : DtdProcessing.Parse
                    };

                    // TODO Handle no network connection
                    XmlReader xmlReader = XmlReader.Create(inputFile.FInfo.OpenRead(), xmlReaderSettings);

                    try
                    {
                        LocalGrampsXMLdoc = XDocument.Load(xmlReader);
                    }
                    catch (System.IO.DirectoryNotFoundException ex)
                    {
                        Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, new SharedSharp.Errors.ErrorInfo("Can not load the Gramps XML file. Error in basic XML load"));

                        return Task.FromResult(false);
                    }
                    catch (Exception ex)
                    {
                        Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex, new ErrorInfo("Can not load the Gramps XML file. Error in basic XML load"));

                        return Task.FromResult(false);
                    }

                    if (LocalGrampsXMLdoc.DocumentType is not null)
                    {
                        int compareFlag = string.Compare(LocalGrampsXMLdoc.DocumentType.PublicId, Constants.GrampsXMLPublicId, StringComparison.CurrentCulture);
                        if (compareFlag < 0)
                        {
                            ErrorInfo t = new("DataStorageLoadXML", "The program can only load files with a Gramps XML version equal or greater.")
                                {
                                    { "Minimum Version", Constants.GrampsXMLPublicId },
                                    { "Found Version", LocalGrampsXMLdoc.DocumentType.PublicId },
                        };

                            Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(t);
                            return Task.FromResult(false);
                        }
                    }

                    System.Collections.Generic.Dictionary<string, XNamespace> nameSpaceList = LocalGrampsXMLdoc.Root.Attributes().Where(
                        a => a.IsNamespaceDeclaration).GroupBy(
                            a => a.Name.Namespace == XNamespace.None ? string.Empty : a.Name.LocalName,
                            a => XNamespace.Get(a.Value)).ToDictionary(
                                                                       g => g.Key,
                                                                       g => g.FirstOrDefault());

                    ns = nameSpaceList.FirstOrDefault().Value.NamespaceName;

                    xmlReader.Dispose();
                }
            }
            catch (Exception ex)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Trying to load Gramps data only", ex);
                throw;
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Load the repositories .
        /// </summary>
        /// <returns>
        /// true or false if the routine is successful.
        /// </returns>
        public async Task<bool> LoadXMLDataAsync()
        {
            // load media because we need it for hlink cropping later
            _ = await LoadMediaObjectsAsync().ConfigureAwait(false);

            // load the rest of the data in dependency order
            await LoadSourcesAsync().ConfigureAwait(false);

            await LoadCitationsAsync().ConfigureAwait(false);
            await LoadEventsAsync().ConfigureAwait(false);

            await LoadHeaderDataAsync().ConfigureAwait(false);
            await LoadNameMapsAsync().ConfigureAwait(false);
            await LoadNotesAsync().ConfigureAwait(false);

            await LoadPlacesAsync().ConfigureAwait(false);
            await LoadRepositoriesAsync().ConfigureAwait(false);

            await LoadTagsAsync().ConfigureAwait(false);

            _ = await LoadFamiliesAsync().ConfigureAwait(false);

            // People last because they rely on pretty much everything else
            await LoadPeopleDataAsync().ConfigureAwait(false);

            // Apart from BookMarks
            await LoadBookMarksAsync().ConfigureAwait(false);

            return true;
        }
    }
}