//-----------------------------------------------------------------------
//
// Handles GRAMPS Alt fields
//
// <copyright file="ParentLinkModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS Alt element class.
    /// </summary>
    [DataContract]
    public class ParentLinkModel : ModelBase, IDetailViewText
    {
        private FamilyModel _Parents = new FamilyModel();

        public ParentLinkModel()
        {
        }

        public FamilyModel Parents
        {
            get
            {
                return _Parents;
            }

            set
            {
                SetProperty(ref _Parents, value);

                // Set HlinkKey to the family model so Valid is true;
                this.HLinkKey = Parents.HLinkKey;
            }
        }
    }
}