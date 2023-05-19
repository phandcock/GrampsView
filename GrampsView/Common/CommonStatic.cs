// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Models.DataModels;

using SharedSharp.Common.Interfaces;

namespace GrampsView.Common
{
    public static class CommonStatic
    {
        public static ISharedSharpCardSizes CardSizes { get; } = Ioc.Default.GetRequiredService<ISharedSharpCardSizes>();

        public static IModelBase CurrentActiveModel { get; set; } = new ModelBase();

        public static ISharedSharpFontSizes FontSizes { get; } = Ioc.Default.GetRequiredService<ISharedSharpFontSizes>();
    }
}