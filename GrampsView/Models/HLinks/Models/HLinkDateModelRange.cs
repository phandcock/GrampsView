// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels.Date;

namespace GrampsView.Models.HLinks.Models
{
    /// <summary>
    /// HLink to a Date Model.
    /// </summary>

    public class HLinkDateModelRange : HLinkBase, IHLinkDateModel
    {
        public HLinkDateModelRange()
        {
            HLinkGlyphItem.Symbol = Constants.IconDate;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");

            HLinkKey = Common.CustomClasses.HLinkKey.NewAsGUID();
        }

        public DateObjectModelRange DeRef
        {
            get;
            set;
        } = new DateObjectModelRange();

        public string Title
        {
            get; set;
        } = string.Empty;

        public override bool Valid => DeRef.Valid;

        //public override Page NavigationPage()
        //{
        //    return new DateRangeDetailPage(this);
        //}
    }
}