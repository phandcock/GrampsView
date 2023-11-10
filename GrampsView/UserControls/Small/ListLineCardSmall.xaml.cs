// Copyright (c) phandcock.  All rights reserved.

using SharedSharp.Common.Interfaces;
using SharedSharp.Sizes;

namespace GrampsView.UserControls
{


    public partial class ListLineCardSmall : Grid
    {
        public ListLineCardSmall()
        {
            InitializeComponent();

            CardSmallHeight = Ioc.Default.GetRequiredService<ISharedCardSizes>().CardSmallHeight;
        }

        public double CardSmallHeight { get; set; } = 100;
    }
}