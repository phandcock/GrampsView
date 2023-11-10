// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Model;
using GrampsView.ModelsDB.Date;
using GrampsView.Views;

namespace GrampsView.Models.HLinks.Models
{
    /// <summary>
    /// HLink to a Date Model.
    /// </summary>

    public class HLinkDateDBModelStr : HLinkDBBase, IHLinkDateDBModel
    {
        public HLinkDateDBModelStr()
        {
            HLinkGlyphItem.Symbol = Constants.IconDate;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");

            HLinkKey = Common.CustomClasses.HLinkKey.NewAsGUID();
        }

        public DateDBModelStr DeRef
        {
            get;
            set;
        } = new DateDBModelStr();

        public string Title
        {
            get; set;
        } = string.Empty;

        public override bool Valid => DeRef.Valid;

        public override Page NavigationPage()
        {
            return new DateStrDetailPage(this);
        }
    }
}