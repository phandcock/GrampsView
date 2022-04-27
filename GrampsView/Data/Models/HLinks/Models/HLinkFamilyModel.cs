// XML 171 - All fields defined

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Views;

    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// HLink to the Family Model.
    /// </summary>

    public class HLinkFamilyModel : HLinkBase, IHLinkFamilyModel
    {
        // NOTE: This cannot default to a FamilyModel as there is a recursive relationship with PersonModel
        private FamilyModel _Deref = null;

        private bool DeRefCached = false;

        public HLinkFamilyModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconFamilies;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundFamily");
        }

        /// <summary>
        /// Gets.
        /// </summary>
        [JsonIgnore]
        public FamilyModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.FamilyDV.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
                }

                if (_Deref is null)
                {
                    _Deref = new FamilyModel();
                }

                return _Deref;
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(FamilyDetailPage));
            return;
        }

        //protected override IModelBase GetDeRef()
        //{
        //    return this.DeRef;
        //}
    }
}