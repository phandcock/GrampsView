// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System.Runtime.Serialization;

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkPlaceNameModel : HLinkBase, IHLinkPlaceNameModel
    {
        public HLinkPlaceNameModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconPlace;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPlace");
        }

        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        [DataMember]
        public new PlaceNameModel DeRef
        {
            get;

            set;
        } = new PlaceNameModel();

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
    }
}