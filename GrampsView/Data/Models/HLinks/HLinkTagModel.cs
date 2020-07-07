// <copyright file="HLinkTagModel.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// ALL DONE
////<define name = "tagref-content" >
////  < attribute name="hlink">
////    <data type = "IDREF" />
////  </ attribute >
////</ define >

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    using GrampsView.Data.DataView;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkTagModel : HLinkBase, IHLinkTagModel
    {
        /// <summary>
        /// The local image h link.
        /// </summary>
        private HLinkMediaModel localImageHLink = new HLinkMediaModel();

        /// <summary>
        /// Gets the dereferenced Tag ViewModel.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public TagModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.TagDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the image h link key.
        /// </summary>
        /// <value>
        /// The image h link key.
        /// </value>
        [DataMember]
        public HLinkMediaModel HomeImageHLink
        {
            get
            {
                return localImageHLink;
            }

            set
            {
                SetProperty(ref localImageHLink, value);
            }
        }
    }
}