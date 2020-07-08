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
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkNoteModel : HLinkBase, IHLinkNoteModel
    {
        public INoteModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.NoteDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return new NoteModel();
                }
            }
        }
    }
}