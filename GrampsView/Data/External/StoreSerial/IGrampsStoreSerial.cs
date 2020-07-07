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
    using System.Threading.Tasks;

    /// <summary>
    /// Interface definitions for IGrampsRepository.
    /// </summary>
    public interface IGrampsStoreSerial
    {
        /// <summary>
        /// Des the serialize repository.
        /// </summary>
        void DeSerializeRepository();

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <param name="theObject">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        bool SerializeObject(object theObject);
    }
}