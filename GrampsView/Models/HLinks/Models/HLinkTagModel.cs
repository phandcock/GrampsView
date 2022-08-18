﻿namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// HLink to a Tag Model
    /// <list type="table">
    /// <listheader>
    /// <term> Item </term>
    /// <term> Status </term>
    /// </listheader>
    /// <item>
    /// <description> XML 1.71 check </description>
    /// <description> Done </description>
    /// </item>
    /// </list>
    /// <para> <br/> </para>
    /// </summary>

    public class HLinkTagModel : HLinkBase, IHLinkTagModel
    {
        private TagModel _Deref = new TagModel();

        private bool DeRefCached = false;

        public HLinkTagModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconTag;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
        }

        /// <summary>
        /// Gets the dereferenced Tag ViewModel.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        [JsonIgnore]
        public TagModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.TagDV.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
                }

                return _Deref;
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, "TagDetailPage");
            return;
        }

        //protected override IModelBase GetDeRef()
        //{
        //    return this.DeRef;
        //}
    }
}