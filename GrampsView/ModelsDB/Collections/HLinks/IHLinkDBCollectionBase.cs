// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common.CustomClasses;

namespace GrampsView.Data.Model
{
    public interface IHLinkDBCollectionBase<T>
    {
        int Count
        {
            get;
        }

        ItemGlyph FirstHLinkHomeImage
        {
            get; set;
        }

        void SetGlyph();
    }
}