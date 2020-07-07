//-----------------------------------------------------------------------
//
// Handles GRAMPS URL fields
//
// <copyright file="URLModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using Prism.Commands;

    using System;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;

    using Xamarin.Essentials;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// GRAMPS URL element class.
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
            OpenURLCommand = new DelegateCommand(OpenURL, CanOpenURL);

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

        public DelegateCommand OpenURLCommand { get; private set; }

        [DataMember]
        public URIType URLType
        {
            get;
            set;
        } = URIType.URL;

        private bool CanOpenURL()
        {
            return true;
        }

        /// <summary>
        /// Opens the URL.
        /// </summary>
        private void OpenURL()
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

                            MapLocation.OpenMapsAsync(mapOptions);
                        }
                        catch (Exception ex)
                        {
                            DataStore.CN.NotifyException("No map application available to open", ex);

                            throw ex;
                        }

                        break;
                    }
                case URIType.URL:
                    {
                        if (GHRef is null)
                        {
                            DataStore.CN.NotifyError("Bad URI for URL Model");
                            break;
                        }

                        if (GHRef.IsWellFormedOriginalString())
                        {
                            Launcher.OpenAsync(GHRef);
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