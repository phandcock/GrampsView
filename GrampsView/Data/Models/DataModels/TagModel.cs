// <copyright file="TagModel.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
////<define name = "tag-content" >
////  <ref name="table-object" />
////  <attribute name = "name" >
////    < text />
////  </ attribute >
////  < attribute name="color">
////    <text />
////  </attribute>
////  <attribute name = "priority" >
////    < data type="integer" />
////  </attribute>
////</define>

/// TODO Update fields as per Schema

namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    using Xamarin.Forms;

    /// <summary>
    /// data model for an event.
    /// </summary>
    [DataContract]
    public sealed class TagModel : ModelBase, ITagModel, IComparable, IComparer
    {
        /// <summary>
        /// The color.
        /// </summary>
        private Color _GColor = Color.White;

        /// <summary>
        /// The name.
        /// </summary>
        private string _GName = string.Empty;

        /// <summary>
        /// The priority.
        /// </summary>
        private int _GPriority = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagModel"/> class.
        /// </summary>
        public TagModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconTag;
            HomeImageHLink.HomeSymbolColour = HomeImageHLink.HomeSymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        [DataMember]
        public Color GColor
        {
            get
            {
                return _GColor;
            }

            set
            {
                SetProperty(ref _GColor, value);
            }
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string GName
        {
            get
            {
                return _GName;
            }

            set
            {
                SetProperty(ref _GName, value);
            }
        }

        /// <summary>
        /// Gets or sets the Priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        [DataMember]
        public int GPriority
        {
            get
            {
                return _GPriority;
            }

            set
            {
                SetProperty(ref _GPriority, value);
            }
        }

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkTagModel HLink
        {
            get
            {
                HLinkTagModel t = new HLinkTagModel
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
            TagModel firstEvent = (TagModel)a;
            TagModel secondEvent = (TagModel)b;

            // compare on Priority first
            int testFlag = string.Compare(firstEvent.GName, secondEvent.GName, StringComparison.CurrentCulture);

            return testFlag;
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
            TagModel secondEvent = (TagModel)obj;

            // compare on Name first
            int testFlag = string.Compare(GName, secondEvent.GName, StringComparison.CurrentCulture);

            return testFlag;
        }
    }
}