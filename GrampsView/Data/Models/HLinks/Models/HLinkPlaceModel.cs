namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// <para> HLink to a Place Model <font color="#333333"> </font> </para>
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

    public class HLinkPlaceModel : HLinkBase, IHLinkPlaceModel
    {
        private PlaceModel _Deref = new PlaceModel();

        private bool DeRefCached = false;

        public HLinkPlaceModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconPlace;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPlace");
        }

        /// <summary>
        /// Gets or sets the optional place date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateObjectModel Date { get; set; } = new DateObjectModelVal();

        [JsonIgnore]
        public new PlaceModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.PlaceDV.GetModelFromHLinkKey(HLinkKey);
                    DeRefCached = true;
                }

                return _Deref;
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, "PlaceDetailPage");
            return;
        }

        // protected override IModelBase GetDeRef() { return DeRef; }
    }
}