//-----------------------------------------------------------------------
//
// Various note models
//
// <copyright file="HLinkNoteModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

////<define name = "noteref-content" >
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
    public class HLinkNoteModel : HLinkBase, IHLinkNoteModel
    {
        public NoteModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.NoteDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}