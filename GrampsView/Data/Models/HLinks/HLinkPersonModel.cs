// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Views;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkPersonModel : HLinkBase, IHLinkPersonModel
    {
        private PersonModel _Deref = new PersonModel();

        public HLinkPersonModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconPeople;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPerson");
        }

        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public PersonModel DeRef
        {
            get
            {
                if (Valid & (!_Deref.Valid))
                {
                    _Deref = DV.PersonDV.GetModelFromHLinkString(HLinkKey);
                }

                return _Deref;
            }
        }

        // TODO Why pass HLinkPersonModel to HLinkPersonModel?
        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(PersonDetailPage));
            return;
        }
    }
}