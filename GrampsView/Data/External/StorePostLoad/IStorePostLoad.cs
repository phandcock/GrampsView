//-----------------------------------------------------------------------
//
// Interface definition for External Storage Providers
//
// <copyright file="IGrampsStorePostLoad.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.ExternalStorageNS
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface definitions for IExternal Storage.
    /// </summary>
    public interface IStorePostLoad
    {
        /// <summary>
        /// Loads the serial UI items.
        /// </summary>
        /// <returns>
        /// </returns>
        Task LoadSerialUiItems();
    }
}