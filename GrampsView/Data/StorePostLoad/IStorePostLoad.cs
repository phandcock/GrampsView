// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common.CustomClasses;
using GrampsView.Models.DataModels;

using System.Threading.Tasks;

namespace GrampsView.Data.StorePostLoad
{
    /// <summary>
    /// Interface definitions for IExternal Storage.
    /// </summary>
    public interface IStorePostLoad
    {
        ItemGlyph GetThumbImageFromZip(MediaModel argMediaModel);

        //Task LoadSerialUiItems();
    }
}