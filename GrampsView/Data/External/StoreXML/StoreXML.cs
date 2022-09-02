namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common;

    using Microsoft.Extensions.DependencyInjection;

    using SharedSharp.Errors;
    using SharedSharp.Logging;

    using System;
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

            ns = Constants.GrampsXMLNameSpace;
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
                IFileInfoEx inputFile = new FileInfoEx(argFileName: Constants.StorageXMLFileName);

                App.Current.Services.GetService<IErrorNotifications>().DataLogEntryAdd("Loading existing local copy of the GRAMPS data");
                {
                    XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();

                    if (Xamarin.Essentials.DeviceInfo.Platform == Xamarin.Essentials.DevicePlatform.Android)
                    {
                        xmlReaderSettings.DtdProcessing = DtdProcessing.Ignore;
                    }
                    else
                    {
                        xmlReaderSettings.DtdProcessing = DtdProcessing.Parse;
                    }

                    // TODO Handle no network connection
                    XmlReader xmlReader = XmlReader.Create(inputFile.FInfo.OpenRead(), xmlReaderSettings);

                    try
                    {
                        localGrampsXMLdoc = XDocument.Load(xmlReader);
                    }
                    catch (System.IO.DirectoryNotFoundException ex)
                    {
                        App.Current.Services.GetService<IErrorNotifications>().NotifyException("Can not load the Gramps XML file. Error in basic XML load", ex);

                        return false;
                    }
                    catch (Exception ex)
                    {
                        App.Current.Services.GetService<IErrorNotifications>().NotifyException("Can not load the Gramps XML file. Error in basic XML load", ex);

                        return false;
                    }

                    if (!(localGrampsXMLdoc.DocumentType is null))
                    {
                        int compareFlag = string.Compare(localGrampsXMLdoc.DocumentType.PublicId, Constants.GrampsXMLPublicId, StringComparison.CurrentCulture);
                        if (compareFlag < 0)
                        {
                            ErrorInfo t = new ErrorInfo("DataStorageLoadXML", "The program can only load files with a Gramps XML version equal or greater.")
                                {
                                    { "Minimum Version", Constants.GrampsXMLPublicId },
                                    { "Found Version", localGrampsXMLdoc.DocumentType.PublicId },
                        };

                            App.Current.Services.GetService<IErrorNotifications>().NotifyError(t);
                            return false;
                        }
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
                App.Current.Services.GetService<IErrorNotifications>().NotifyException("Trying to load Gramps data only", ex);
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