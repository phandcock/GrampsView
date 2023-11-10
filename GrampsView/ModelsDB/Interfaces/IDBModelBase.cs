// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Collections;
using GrampsView.DBModels;

using System.ComponentModel.DataAnnotations;

namespace GrampsView.Models.DBModels.Interfaces
{
    public interface IDBModelBase
    {
        HLinkBackLinkDBModelCollection BackHLinkReferenceCollection { get; set; }

        [Key]
        string HLinkKeyValue { get; set; }

        void LoadBasics(DBModelBase argBasics);
    }
}