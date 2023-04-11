// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Views;

namespace GrampsView.UserControls
{
    public partial class TagCardSingle : SingleCardControlTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagCardSingle"/> class.
        /// </summary>
        public TagCardSingle()
        {
            InitializeComponent();
        }

        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Navigation.PushAsync(new TagDetailPage(args.Parameter as HLinkTagModel));
        }
    }
}