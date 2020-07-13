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
    using System.Runtime.Serialization;

    //// gramps XML 1.71
    ////
    /// value
    /// lang
    /// date-content
    /// </summary>
    public class PlaceNameModel : ModelBase, IPlaceNameModel, IComparable<PlaceNameModel>, IEquatable<PlaceNameModel>
    {
        public PlaceNameModel()
        {
        }

        [DataMember]
        public DateObjectModel GDate { get; set; } = new DateObjectModel();

        [DataMember]
        public string GLang { get; set; }

        [DataMember]
        public string GValue { get; set; }

        public int CompareTo(PlaceNameModel other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return GetDefaultText.CompareTo(other.GetDefaultText);
        }

        public bool Equals(PlaceNameModel other)
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