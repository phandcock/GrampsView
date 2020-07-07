//-----------------------------------------------------------------------
//
// The data model defined by this file serves to hold Repository data from the GRAMPS data file
//
// <copyright file="RepositoryModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Gramps XML 1.71 all configured
/// </summary>

namespace GrampsView.Data.Model
{
    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Data.Collections;

    /// <summary>
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.ModelBase"/>
    /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.ViewModel.IRepositoryModel"/>
    /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="System.IComparable"/>
    /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="System.Collections.IComparer"/>
    [DataContract]
    public sealed class RepositoryModel : ModelBase, IRepositoryModel, IComparable, IComparer
    {
        /// <summary>
        /// The local r name.
        /// </summary>
        private string localRName;

        /// <summary>
        /// The local type.
        /// </summary>
        private string localType;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryModel"/> class.
        /// </summary>
        public RepositoryModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconRepository;
            HomeImageHLink.HomeSymbolColour = HomeImageHLink.HomeSymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundRepository");
        }

        /// <summary>
        /// Gets or sets address collection.
        /// </summary>
        [DataMember]
        public HLinkOCAddressModelCollection GAddress { get; set; }

        = new HLinkOCAddressModelCollection();

        /// <summary>
        /// Gets the get default text for this Model.
        /// </summary>
        /// <value>
        /// The default text.
        /// </value>
        public override string GetDefaultText
        {
            get
            {
                return GRName;
            }
        }

        /// <summary>
        /// Gets or sets the g note reference collection.
        /// </summary>
        /// <value>
        /// The g note reference collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection { get; set; }
            = new HLinkNoteModelCollection();

        /// <summary>
        /// Gets or sets the Repository Name.
        /// </summary>
        /// <value>
        /// The name of the Repository.
        /// </value>
        [DataMember]
        public string GRName
        {
            get
            {
                return localRName;
            }

            set
            {
                SetProperty(ref localRName, value);
            }
        }

        /// <summary>
        /// Gets or sets the g tag reference collection.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRefCollection { get; set; } = new HLinkTagModelCollection();

        /// <summary>
        /// Gets or sets the repository type.
        /// </summary>
        /// <value>
        /// The type of the repository.
        /// </value>
        [DataMember]
        public string GType
        {
            get
            {
                return localType;
            }

            set
            {
                SetProperty(ref localType, value);
            }
        }

        /// <summary>
        /// Gets or sets uRL collection.
        /// </summary>
        [DataMember]
        public OCURLModelCollection GURL { get; set; }

        = new OCURLModelCollection();

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkRepositoryModel HLink
        {
            get
            {
                HLinkRepositoryModel t = new HLinkRepositoryModel
                {
                    HLinkKey = HLinkKey,
                };
                return t;
            }
        }

        /// <summary>
        /// Compares two objects.
        /// </summary>
        /// <param name="a">
        /// object A.
        /// </param>
        /// <param name="b">
        /// object B.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        int IComparer.Compare(object a, object b)
        {
            // TODO Add compare TagModel firstEvent = (TagModel)a; TagModel secondEvent = (TagModel)b;

            //// compare on Priority first
            // int testFlag = string.Compare(firstEvent.Name, secondEvent.Name, StringComparison.CurrentCulture);

            // return testFlag;
            return 0;
        }

        /// <summary>
        /// Implement IComparable CompareTo method.
        /// </summary>
        /// <param name="obj">
        /// The object to compare.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        int IComparable.CompareTo(object obj)
        {
            // TODO Add compare to TagModel secondEvent = (TagModel)obj;

            //// compare on Name first
            // int testFlag = string.Compare(Name, secondEvent.Name, StringComparison.CurrentCulture);

            // return testFlag;
            return 0;
        }
    }
}