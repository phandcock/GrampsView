// Copyright (c) phandcock.  All rights reserved.

using GrampsView.ModelsDB.HLinks.Models;

namespace GrampsView.Models.DBModels.Interfaces
{
    public interface ICitationDBModel : IDBModelBase
    {

        HLinkCitationDBModel HLink { get; }
    }
}