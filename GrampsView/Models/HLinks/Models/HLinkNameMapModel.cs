namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// <para> HLink to namemap model. </para>
    /// <para> TODO Needs XML 1.71 check </para>
    /// </summary>

    public class HLinkNameMapModel : HLinkBase, IHLinkNameMapModel
    {
        private NameMapModel _Deref = new NameMapModel();

        private bool DeRefCached = false;

        public HLinkNameMapModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconNameMaps;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundNameMap");
        }

        /// <summary>
        /// Gets the NameMap model.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        [JsonIgnore]
        public NameMapModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.NameMapDV.GetModelFromHLinkKey(HLinkKey);

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
            await UCNavigateBase(this, "NameMapDetailPage");
            return;
        }

        //protected override IModelBase GetDeRef()
        //{
        //    return this.DeRef;
        //}
    }
}