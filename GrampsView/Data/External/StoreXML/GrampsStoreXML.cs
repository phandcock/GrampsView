namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;

    public partial class GrampsStoreXML : CommonBindableBase, IGrampsStoreXML
    {
        /// <summary>
        /// The default XML namespace.
        /// </summary>
        private static XNamespace ns;

        /// <summary>
        /// IOC local copy of GramsView Logging routines.
        /// </summary>
        private readonly ICommonLogging localGrampsCommonLogging;

        /// <summary>
        /// The Gramps XML document.
        /// </summary>
        private XDocument localGrampsXMLdoc;

        /// <summary>
        /// Initializes a new instance of the <see cref="GrampsStoreXML"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The gramps view common logging.
        /// </param>
        /// <param name="iocGrampsStorePostLoad">
        /// The ioc gramps store post load.
        /// </param>
        public GrampsStoreXML(ICommonLogging iocCommonLogging)
        {
            localGrampsCommonLogging = iocCommonLogging;

            ns = CommonConstants.GrampsXMLNameSpace;
        }

        /// <summary>
        /// Sets the private object.
        /// </summary>
        /// <param name="thePriv">
        /// The priv.
        /// </param>
        /// <returns>
        /// True or False depending on if the object is private.
        /// </returns>
        public static bool SetPrivateObject(string thePriv)
        {
            switch (thePriv)
            {
                case "1":
                    {
                        return true;
                    }

                case "0":
                default:
                    {
                        return false;
                    }
            }
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
                FileInfoEx inputFile = StoreFolder.FolderGetFile(DataStore.Instance.AD.CurrentDataFolder, CommonConstants.StorageXMLFileName);

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
                DataStore.Instance.CN.NotifyException("trying to load Gramps data only", ex);
                throw;
            }

            return true;
        }

        /// <summary>
        /// Loads the repositories asynchronous.
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

            await LoadBookMarksAsync().ConfigureAwait(false);
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

            return true;
        }
    }
}