//-----------------------------------------------------------------------
//
// Various note models
//
// <copyright file="HLinkSourceModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

////<define name = "sourceref-content" >
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
    public class HLinkSourceModel : HLinkBase, IHLinkSourceModel
    {
        /// <summary>
        /// Gets the model from the HLink.
        /// </summary>
        /// <value>
        /// The HLink reference.
        /// </value>
        public SourceModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.SourceDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return new SourceModel();
                }
            }
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="argOobj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public int CompareTo(HLinkSourceModel argOobj) => DeRef.CompareTo(argOobj);

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="argObj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public new int CompareTo(object argObj)
        {
            // Null objects go first
            if (argObj is null) { return 1; }

            // Can only comapre if they are the same type so assume equal
            if (argObj.GetType() != typeof(HLinkSourceModel))
            {
                return 0;
            }

            return DeRef.CompareTo((argObj as HLinkSourceModel).DeRef);
        }
    }
}