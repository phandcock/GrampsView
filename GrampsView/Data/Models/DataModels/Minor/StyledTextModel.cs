//-----------------------------------------------------------------------
//
// Handles GRAMPS Address fields
//
// <copyright file="AddressModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System;

    // TODO finish this
    /// <summary> <define name = "styledtext" >
    //  < element name = "text" >
    //    < text />
    //  </ element >
    //  < zeroOrMore >
    //    < element name = "style" >
    //      < attribute name = "name" >
    //        < choice >
    //          < value > bold </ value >
    //          < value > italic </ value >
    //          < value > underline </ value >
    //          < value > fontface </ value >
    //          < value > fontsize </ value >
    //          < value > fontcolor </ value >
    //          < value > highlight </ value >
    //          < value > superscript </ value >
    //          < value > link </ value >
    //        </ choice >
    //      </ attribute >
    //      < optional >
    //        < attribute name = "value" >
    //          < text />
    //        </ attribute >
    //      </ optional >
    //      < oneOrMore >
    //        < element name = "range" >
    //          < attribute name = "start" >
    //            < data type = "int" />
    //          </ attribute >
    //          < attribute name = "end" >
    //            < data type = "int" />
    //          </ attribute >
    //        </ element >
    //      </ oneOrMore >
    //    </ element >
    //  </ zeroOrMore >
    //</ define >
    /// </summary>
    public class StyledTextModel : ModelBase, IStyledTextModel, IComparable<StyledTextModel>, IEquatable<StyledTextModel>
    {
        public StyledTextModel()
        {
        }

        public int CompareTo(StyledTextModel other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return GetDefaultText.CompareTo(other.GetDefaultText);
        }

        public bool Equals(StyledTextModel other)
        {
            if (other is null)
            {
                return false;
            }

            if (GetDefaultText == other.GetDefaultText)
            {
                return true;
            }

            return false;
        }
    }
}