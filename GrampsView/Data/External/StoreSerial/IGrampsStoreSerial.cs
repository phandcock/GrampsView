//-----------------------------------------------------------------------
//
// Interface defintion for IGrampsRepository.cs
//
// <copyright file="IGrampsStoreSerial.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.External.StoreSerial
{
    using GrampsView.Data.Repository;

    using System.Threading.Tasks;

    /// <summary>
    /// Interface definitions for IGrampsRepository.
    /// </summary>
    public interface IGrampsStoreSerial
    {
        /// <summary>
        /// Des the serialize repository.
        /// </summary>
        Task DeSerializeRepository();

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <param name="theObject">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        Task SerializeObject(DataInstance theObject);
    }
}