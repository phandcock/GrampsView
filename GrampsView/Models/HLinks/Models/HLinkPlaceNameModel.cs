// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Models.DataModels.Minor;

    /// TODO Update fields as per Schema

    public class HLinkPlaceNameModel : HLinkBase, IHLinkPlaceNameModel
    {
        public HLinkPlaceNameModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconPlace;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPlace");
        }

        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>

        public PlaceNameModel DeRef
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