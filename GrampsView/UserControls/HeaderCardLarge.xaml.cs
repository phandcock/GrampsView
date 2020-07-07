// <copyright file="PersonCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;

    using System.Diagnostics.Contracts;

    using Xamarin.Forms;

    public partial class HeaderCardLarge : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderCardLarge"/> class.
        /// </summary>
        public HeaderCardLarge()
        {
            InitializeComponent();
        }

        public CardListLineCollection HeaderCard { get; set; } = new CardListLineCollection();

        private void HeaderCardLargeRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
            if (this.BindingContext is null)
            {
                return;
            }

            HLinkHeaderModel HeaderData = this.BindingContext as HLinkHeaderModel;

            Contract.Assert(HeaderData != null);

            if (HeaderData.Valid)
            {
                HeaderCard = new CardListLineCollection
                    {
                        new CardListLine("Created using version:", HeaderData.DeRef.GCreatedVersion),
                        new CardListLine("Created on:", HeaderData.DeRef.GCreatedDate),
                        new CardListLine("Researcher Name:", HeaderData.DeRef.GResearcherName),
                        new CardListLine("Researcher State:", HeaderData.DeRef.GResearcherState),
                        new CardListLine("Researcher Country:", HeaderData.DeRef.GResearcherCountry),
                        new CardListLine("Researcher Email:", HeaderData.DeRef.GResearcherEmail),
                        new CardListLine("MediaPath:", HeaderData.DeRef.GMediaPath),
                    };
            }

            HeaderCard.Title = "Header Details";

            this.headerFrame.Content = new ListLineCardLarge
            {
                BindingContext = HeaderCard
            };
        }
    };
}