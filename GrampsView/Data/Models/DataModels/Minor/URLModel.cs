// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
    {
    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using System;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Essentials;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// GRAMPS URL element class. TODO Update fields as per Schema
    /// </summary>
    public class URLModel : ModelBase, IURLModel
        {
        //// "url-content"
        //// "priv"
        //// "type"
        //// "href"
        //// "description"

        /// <summary>
        /// Initializes a new instance of the <see cref="URLModel"/> class.
        /// </summary>
        public URLModel()
            {
            OpenURLCommand = new AsyncCommand(() => OpenURL());

            HomeImageHLink.HomeSymbol = CommonConstants.IconURL;
            }

        /// <summary>
        /// Gets the default text.
        /// </summary>
        /// <value>
        /// The default text.
        /// </value>
        public string DefaultText
            {
            get
                {
                string returnVal = string.Empty;

                if (!string.IsNullOrEmpty(GType))
                    {
                    returnVal = GType + ":";
                    }

                return returnVal + GDescription;
                }
            }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The g description.
        /// </value>
        [DataMember]
        public string GDescription
            {
            get;
            set;
            }

        /// <summary>
        /// Gets or sets the hlink reference.
        /// </summary>
        /// <value>
        /// The gh reference.
        /// </value>
        [DataMember]
        public Uri GHRef
            {
            get;
            set;
            }

        [DataMember]
        public string GType
            {
            get;
            set;
            }

        public Placemark MapLocation { get; set; } = new Placemark();

        public IAsyncCommand OpenURLCommand { get; private set; }

        [DataMember]
        public URIType URLType
            {
            get;
            set;
            } = URIType.URL;

        /// <summary>
        /// Opens the URL.
        /// </summary>
        private async Task OpenURL()
            {
            switch (URLType)
                {
                case URIType.Map:
                        {
                        try
                            {
                            MapLaunchOptions mapOptions = new MapLaunchOptions
                                {
                                Name = DefaultText,
                                };

                            await MapLocation.OpenMapsAsync(mapOptions);
                            }
                        catch (Exception ex)
                            {
                            DataStore.Instance.CN.NotifyException("No map application available to open", ex);

                            throw;
                            }

                        break;
                        }
                case URIType.URL:
                        {
                        if (GHRef is null)
                            {
                            DataStore.Instance.CN.NotifyError("Bad URI for URL Model");
                            break;
                            }

                        if (GHRef.IsWellFormedOriginalString())
                            {
                            await Launcher.OpenAsync(GHRef);
                            }
                        break;
                        }
                default:
                        {
                        Contract.Assert(false, "Bad URI Type");
                        break;
                        }
                }
            }
        }
    }