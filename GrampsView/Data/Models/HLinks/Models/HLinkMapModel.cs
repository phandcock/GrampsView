namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary> HLink to a Map

    /// <para> <br/> </para> </summary>

    [DataContract]
    public class HLinkMapModel : HLinkBase, IHLinkMapModel
    {
        public HLinkMapModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconMap;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
        }

        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        [DataMember]
        public new MapModel DeRef
        {
            get;

            set;
        } = new MapModel();

        public override bool Valid
        {
            get
            {
                if (!HLinkGlyphItem.Valid)
                {
                }

                return HLinkGlyphItem.Valid;
            }
        }

        /// <summary>
        /// No detail page to navigate to, just open the URL externally.
        /// </summary>
        public override async Task UCNavigate()
        {
            await DeRef.OpenMap();
            return;
        }

        protected override IModelBase GetDeRef()
        {
            return this.DeRef;
        }
    }
}