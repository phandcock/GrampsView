//-----------------------------------------------------------------------
//
// Interface for the Event Repository
//
// <copyright file="IFamilyDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using System.Collections.Generic;

    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;

    /// <summary>
    /// Interface for the Family Repository.
    /// </summary>
    public interface IFamilyDataView : IDataViewBase<FamilyModel, HLinkFamilyModel, HLinkFamilyModelCollection>
    {
        new IReadOnlyList<FamilyModel> DataDefaultSort { get; }

        /// <summary>
        /// Gets or sets the family data.
        /// </summary>
        /// <value>
        /// The family data.
        /// </value>
        RepositoryModelDictionary<FamilyModel, HLinkFamilyModel> FamilyData
        {
            get;
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkFamilyModelCollection GetAllAsHLink();

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="hlinkFamily">
        /// The hlink family.
        /// </param>
        /// <returns>
        /// </returns>
        HLinkPersonModelCollection GetChildren(HLinkFamilyModel hlinkFamily);

        /// <summary>
        /// Gets the current partner.
        /// </summary>
        /// <param name="hlinkFamily">
        /// The hlink family.
        /// </param>
        /// <returns>
        /// </returns>
        HLinkPersonModel GetCurrentPartner(HLinkFamilyModel hlinkFamily);

        /// <summary>
        /// Gets the current spouses.
        /// </summary>
        /// <param name="hlinkFamily">
        /// The hlink family.
        /// </param>
        /// <returns>
        /// </returns>
        HLinkPersonModelCollection GetCurrentSpouses(HLinkFamilyModel hlinkFamily);

        /// <summary>
        /// Gets the father.
        /// </summary>
        /// <param name="arg">
        /// The argument.
        /// </param>
        /// <returns>
        /// </returns>
        IPersonModel GetFather(string arg);

        /// <summary>
        /// Gets the mother.
        /// </summary>
        /// <param name="arg">
        /// The argument.
        /// </param>
        /// <returns>
        /// </returns>
        IPersonModel GetMother(string arg);

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        new HLinkFamilyModelCollection HLinkCollectionSort(HLinkFamilyModelCollection collectionArg);
    }
}