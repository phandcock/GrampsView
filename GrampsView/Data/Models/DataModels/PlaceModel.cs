//-----------------------------------------------------------------------
//
// The data model defined by this file serves to hold Event data from the GRAMPS data file
//
// <copyright file="PlaceModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
///
/// </summary>
///
/// Done.
///
/// - SecondaryColor-object
/// -- type
/// -- ptitle
/// -- placeref
/// -- objref
/// -- noteref
/// -- pname
///
///
////   < optional >
////     < element name="coord">
////       <attribute name = "long" >
////         < text />
////       </ attribute >
////       < attribute name="lat">
////         <text />
////       </attribute>
////     </element>
////   </optional>

////   <zeroOrMore>
////     <element name = "location" >
////       < optional >
////         < attribute name="street">
////           <text />
////         </attribute>
////       </optional>
////       <optional>
////         <attribute name = "locality" >
////           < text />
////         </ attribute >
////       </ optional >
////       < optional >
////         < attribute name="city">
////           <text />
////         </attribute>
////       </optional>
////       <optional>
////         <attribute name = "parish" >
////           < text />
////         </ attribute >
////       </ optional >
////       < optional >
////         < attribute name="county">
////           <text />
////         </attribute>
////       </optional>
////       <optional>
////         <attribute name = "state" >
////           < text />
////         </ attribute >
////       </ optional >
////       < optional >
////         < attribute name="country">
////           <text />
////         </attribute>
////       </optional>
////       <optional>
////         <attribute name = "postal" >
////           < text />
////         </ attribute >
////       </ optional >
////       < optional >
////         < attribute name="phone">
////           <text />
////         </attribute>
////       </optional>
////     </element>
////   </zeroOrMore>
////
////     <element name = "objref" > Done
////       <ref name="objref-content" />
////     </element>
////
////   <zeroOrMore>
////     <element name = "url" >
////       <ref name="url-content" />
////     </element>
////   </zeroOrMore>
////

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
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.ViewModel.IPlaceModel"/>
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="System.IComparable"/>
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="System.Collections.IComparer"/>
    [DataContract]
    public sealed class PlaceModel : ModelBase, IPlaceModel, IComparable, IComparer
    {
        /// <summary>
        /// The local g code field.
        /// </summary>
        private string _GCodeField;

        private string _GNameField = string.Empty;

        private HLinkPlaceModelCollection _PlaceChildCollection = new HLinkPlaceModelCollection();

        /// <summary>
        /// The local media collection.
        /// </summary>
        private HLinkMediaModelCollection localMediaCollection = new HLinkMediaModelCollection();

        /// <summary>
        /// The local note reference.
        /// </summary>
        private HLinkNoteModelCollection localNoteReference = new HLinkNoteModelCollection();

        /// <summary>
        /// The local place reference.
        /// </summary>
        private HLinkPlaceModelCollection localPlaceReference = new HLinkPlaceModelCollection();

        /// <summary>
        /// The local place type field.
        /// </summary>
        private string localPlaceTypeField;

        /// <summary>
        /// The local p title field.
        /// </summary>
        private string localPTitleField;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceModel"/> class.
        /// </summary>
        public PlaceModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconPlace;
            HomeImageHLink.HomeSymbolColour = HomeImageHLink.HomeSymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPlace");

            PlaceChildCollection.Title = "Enclosed Places";
            GPlaceRefCollection.Title = "Enclosing Place";
        }

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection { get; set; }

        /// <summary>
        /// Gets or sets the gp code.
        /// </summary>
        /// <value>
        /// The gp code.
        /// </value>
        [DataMember]
        public string GCode
        {
            get
            {
                return _GCodeField;
            }

            set
            {
                SetProperty(ref _GCodeField, value);
            }
        }

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
                if (!string.IsNullOrEmpty(GPTitle))
                {
                    return GPTitle;
                }

                return GName;
            }
        }

        /// <summary>
        /// Gets or sets the g media reference collection.
        /// </summary>
        /// <value>
        /// The g media reference collection.
        /// </value>
        [DataMember]
        public HLinkMediaModelCollection GMediaRefCollection
        {
            get
            {
                return localMediaCollection;
            }

            set
            {
                SetProperty(ref localMediaCollection, value);
            }
        }

        [DataMember]
        public string GName
        {
            get
            {
                return _GNameField;
            }

            set
            {
                SetProperty(ref _GNameField, value);
            }
        }

        /// <summary>
        /// Gets or sets the g note reference collection.
        /// </summary>
        /// <value>
        /// The g note reference collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection
        {
            get
            {
                return localNoteReference;
            }

            set
            {
                SetProperty(ref localNoteReference, value);
            }
        }

        /// <summary>
        /// Gets or sets the g place reference collection.
        /// </summary>
        /// <value>
        /// The g place reference collection.
        /// </value>
        [DataMember]
        public HLinkPlaceModelCollection GPlaceRefCollection
        {
            get
            {
                return localPlaceReference;
            }

            set
            {
                SetProperty(ref localPlaceReference, value);
            }
        }

        /// <summary>
        /// Gets or sets the place title.
        /// </summary>
        /// <value>
        /// The gp title.
        /// </value>
        [DataMember]
        public string GPTitle
        {
            get
            {
                return localPTitleField;
            }

            set
            {
                SetProperty(ref localPTitleField, value);
            }
        }

        /// <summary>
        /// Gets or sets the g tag reference collection.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRefCollection
        {
            get
            ;

            set
          ;
        }

        // <attribute name = "type" > < text /> </ attribute >
        [DataMember]
        public string GType
        {
            get
            {
                return localPlaceTypeField;
            }

            set
            {
                SetProperty(ref localPlaceTypeField, value);
            }
        }

        /// <summary>
        /// Gets or sets uRL model collection.
        /// </summary>
        [DataMember]
        public OCURLModelCollection GURLCollection { get; set; }

        = new OCURLModelCollection();

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkPlaceModel HLink
        {
            get
            {
                HLinkPlaceModel t = new HLinkPlaceModel
                {
                    HLinkKey = HLinkKey,
                };
                return t;
            }
        }

        [DataMember]
        public HLinkPlaceModelCollection PlaceChildCollection
        {
            get
            {
                return _PlaceChildCollection;
            }

            set
            {
                SetProperty(ref _PlaceChildCollection, value);
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
            // TagModel firstEvent = (TagModel)a; TagModel secondEvent = (TagModel)b;

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
            // TagModel secondEvent = (TagModel)obj;

            //// compare on Name first
            // int testFlag = string.Compare(Name, secondEvent.Name, StringComparison.CurrentCulture);

            // return testFlag;
            return 0;
        }
    }
}