// Copyright (c) phandcock.  All rights reserved.

using Microsoft.Maui.Layouts;

namespace GrampsView.Common.CustomClasses
{
    public class HorizontalWrapLayout : StackLayout
    {
        public HorizontalWrapLayout()
        {
        }

        protected override ILayoutManager CreateLayoutManager()
        {
            return new HorizontalWrapLayoutManager(this);
        }
    }
}