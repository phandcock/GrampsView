// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Models.DataModels.Date;
using GrampsView.Models.HLinks;
using GrampsView.Views;

using System.Threading.Tasks;

namespace GrampsView.Data.Model
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
        }

        public override bool Valid => DeRef.Valid;

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, new DateRangeDetailPage());
            return;
        }


    }
}