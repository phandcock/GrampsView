//-----------------------------------------------------------------------
//
// Family model interface
//
// <copyright file="IFamilyModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    /// <summary>
    /// interface definitions.
    /// </summary>
    public interface IFamilyModel : IModelBase
    {
        /// <summary>
        /// Gets the family display name sort.
        /// </summary>
        /// <value>The family display name sort.</value>
        string FamilyDisplayNameSort { get; }

        /// <summary>
        /// Gets the get h link Family Model that points to this ViewModel.
        /// </summary>
        /// <value>The get h link.</value>
        HLinkFamilyModel HLink { get; }
    }
}