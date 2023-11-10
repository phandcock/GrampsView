// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels.Date;

namespace GrampsView.Models.HLinks.Models
{
    /// <summary>
    /// HLink to a Date Model.
    /// </summary>

    public class HLinkDateModelSpan : HLinkBase, IHLinkDateModel
    {
        public HLinkDateModelSpan()
        {
            HLinkGlyphItem.Symbol = Constants.IconDate;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");

            HLinkKey = Common.CustomClasses.HLinkKey.NewAsGUID();
        }

        public DateObjectModelSpan DeRef
        {
            get;
            set;
        } = new DateObjectModelSpan();

        public string Title
        {
            get; set;
        } = string.Empty;

        public override bool Valid => DeRef.Valid;

        //public override Page NavigationPage()
        //{
        //    return new DateSpanDetailPage(this);
        //}
    }
}