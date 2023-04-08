// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Models.DataModels.Date;
using GrampsView.Models.HLinks;
using GrampsView.Views;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// HLink to a Date Model.
    /// </summary>

    public class HLinkDateModelVal : HLinkBase, IHLinkDateModel
    {
        public HLinkDateModelVal()
        {
            HLinkGlyphItem.Symbol = Constants.IconDate;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");

            HLinkKey = Common.CustomClasses.HLinkKey.NewAsGUID();
        }

        public DateObjectModelVal DeRef
        {
            get;
            set;
        } = new DateObjectModelVal();

        public string Title
        {
            get; set;
        }

        public override bool Valid => DeRef.Valid;

        public override async Task UCNavigate()
        {
            await UCNavigateDetail(new DateValDetailPage(this));
            return;
        }
    }
}