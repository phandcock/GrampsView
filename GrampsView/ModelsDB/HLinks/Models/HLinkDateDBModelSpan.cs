// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels.Date;
using GrampsView.Views;

namespace GrampsView.Models.HLinks.Models
{
    /// <summary>
    /// HLink to a Date Model.
    /// </summary>

    public class HLinkDateDBModelSpan : HLinkDBBase, IHLinkDateDBModel
    {
        public HLinkDateDBModelSpan()
        {
            HLinkGlyphItem.Symbol = Constants.IconDate;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");

            HLinkKey = Common.CustomClasses.HLinkKey.NewAsGUID();
        }

        public DateDBModelSpan DeRef
        {
            get;
            set;
        } = new DateDBModelSpan();

        public string Title
        {
            get; set;
        } = string.Empty;

        public override bool Valid => DeRef.Valid;

        public override Page NavigationPage()
        {
            return new DateSpanDetailPage(this);
        }
    }
}