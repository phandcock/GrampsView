﻿// XML 171 - All fields defined

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Views;

    using System;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// HLink to a Date Model.
    /// </summary>
    [DataContract]
    public class HLinkDateModel : HLinkBase, IHLinkDateModel
    {
        public HLinkDateModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconDate;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");

            HLinkKey.Value = new Guid().ToString();
        }

        [DataMember]
        public new DateObjectModel DeRef
        {
            get;
            set;
        } = new DateObjectModelVal();

        [DataMember]
        public string Title
        {
            get; set;
        }

        public override bool Valid => DeRef.Valid;

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(DateDetailPage));
            return;
        }
    }
}