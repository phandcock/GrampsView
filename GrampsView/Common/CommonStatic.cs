// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Models.DataModels;

using SharedSharp.Sizes;

namespace GrampsView.Common
{
    public static class CommonStatic
    {
        public static ISharedCardSizes CardSizes { get; } = Ioc.Default.GetRequiredService<ISharedCardSizes>();

        public static IModelBase CurrentActiveModel { get; set; } = new ModelBase();

        public static ISharedFontSizes FontSizes { get; } = Ioc.Default.GetRequiredService<ISharedFontSizes>();
    }
}