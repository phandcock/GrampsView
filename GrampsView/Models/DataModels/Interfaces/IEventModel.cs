// Copyright (c) phandcock.  All rights reserved.

using GrampsView.ModelsDB.HLinks;

using System;
using System.Collections;

namespace GrampsView.Data.Model
{
    public interface IEventModel : IModelBase, IComparable, IComparer
    {
        HLinkEventDBModel HLink
        {
            get;
        }
    }
}