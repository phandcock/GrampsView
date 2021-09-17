﻿// XML 171 - All fields defined

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Views;

    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// HLink to a Date Model.
    /// </summary>

    public class HLinkDateModelSpan : HLinkBase, IHLinkDateModel
    {
        public HLinkDateModelSpan()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconDate;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");

            HLinkKey = Common.CustomClasses.HLinkKey.NewAsGUID();
        }

        [JsonIgnore]
        public new DateObjectModelSpan DeRef
        {
            get;
            set;
        } = new DateObjectModelSpan();

        public string Title
        {
            get; set;
        }

        public override bool Valid => DeRef.Valid;

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(DateSpanDetailPage));
            return;
        }

        //protected override IModelBase GetDeRef()
        //{
        //    return this.DeRef;
        //}
    }
}