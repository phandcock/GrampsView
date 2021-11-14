namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;

    using SharedSharp.Errors;
    using SharedSharp.Logging;

    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;

    using Xamarin.CommunityToolkit.ObjectModel;

    public partial class StoreXML : ObservableObject, IStoreXML
    {
        /// <summary>
        /// The default XML namespace.
        /// </summary>
        private static XNamespace ns;

        /// <summary>
        /// local copy of GramsView Logging routines.
        /// </summary>
        private readonly ISharedLogging _iocCommonLogging;

        private readonly IErrorNotifications _iocCommonNotifications;

        /// <summary>
        /// The Gramps XML document.
        /// </summary>
        private XDocument localGrampsXMLdoc;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreXML"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common Logging.
        /// </param>
        /// <param name="iocCommonNotifications">
        /// Common Notifications
        /// </param>
        public StoreXML(ISharedLogging iocCommonLogging, IErrorNotifications iocCommonNotifications)
        {
            _iocCommonLogging = iocCommonLogging;

            _iocCommonNotifications = iocCommonNotifications;

            ns = CommonConstants.GrampsXMLNameSpace;
        }

        /// <summary>
        /// Loads the Gramps XML data.
        /// </summary>
        /// <param name="dataFolder">
        /// StorageFolder to load.
        /// </param>
        /// <returns>
        /// Flag if Gramps Data Only loaded successfully.
        /// </returns>
        public async Task<bool> DataStorageLoadXML()
        {
            try
            {
                IFileInfoEx inputFile = new FileInfoEx(argFileName: CommonConstants.StorageXMLFileName);

                await DataStore.Instance.CN.DataLogEntryAdd("Loading existing local copy of the GRAMPS data").ConfigureAwait(false);
                {
                    // TODO Handle no network connection
                    Stream xmlReader = inputFile.FInfo.OpenRead();

                    XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
                    {
                        DtdProcessing = DtdProcessing.Parse
                    };

                    try
                    {
                        localGrampsXMLdoc = XDocument.Load(xmlReader);
                    }
                    catch (Exception ex)
                    {
                        DataStore.Instance.CN.NotifyException("Can not load the Gramps XML file. Error in basic XML load", ex);

                        throw;
                    }

                    int compareFlag = string.Compare(localGrampsXMLdoc.DocumentType.PublicId, CommonConstants.GrampsXMLPublicId, StringComparison.CurrentCulture);
                    if (compareFlag < 0)
                    {
                        ErrorInfo t = new ErrorInfo("DataStorageLoadXML", "The program can only load files with a Gramps XML version equal or greater.")
                                {
                                    { "Minimum Version", CommonConstants.GrampsXMLPublicId },
                                    { "Found Version", localGrampsXMLdoc.DocumentType.PublicId },
                        };

                        DataStore.Instance.CN.NotifyError(t);
                        return false;
                    }

                    var nameSpaceList = localGrampsXMLdoc.Root.Attributes().Where(
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
                DataStore.Instance.CN.NotifyException("Trying to load Gramps data only", ex);
                throw;
            }

            return true;
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
            await LoadMediaObjectsAsync().ConfigureAwait(false);

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

            await LoadFamiliesAsync().ConfigureAwait(false);

            // People last because they rely on pretty much everything else
            await LoadPeopleDataAsync().ConfigureAwait(false);

            // Apart from BookMarks
            await LoadBookMarksAsync().ConfigureAwait(false);

            return true;
        }
    }
}