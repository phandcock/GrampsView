namespace GrampsView.Data.DataView
{
    using GrampsView.Data.Model;
    using GrampsView.Models.Collections.HLinks;
    using GrampsView.Models.DataModels;
    using GrampsView.Models.DBModels;

    using Microsoft.EntityFrameworkCore;

    using System.Collections.Generic;

    /// <summary>
    /// Interface for the Family Repository.
    /// </summary>
    public interface IFamilyDataLayer : IDataLayerBase<FamilyModel, HLinkFamilyModel, HLinkFamilyModelCollection>
    {
        new IReadOnlyList<FamilyModel> DataAsDefaultSort
        {
            get;
        }

        // TODO add this to the general Interface
        DbSet<FamilyDBModel> FamilyAccess { get; }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkFamilyModelCollection GetAllAsHLink();

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