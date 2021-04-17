// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// HLink to an Address model.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkAdressModel : HLinkBase, IHLinkAddressModel
    {
        private AddressModel _Deref = new AddressModel();

        public HLinkAdressModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconAddress;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundAddress");
        }

        /// <summary>
        /// Gets the address model.
        /// </summary>
        /// <value>
        /// The address model.
        /// </value>
        public AddressModel DeRef
        {
            get
            {
                if (Valid & (!_Deref.Valid))
                {
                    _Deref = DV.AddressDV.GetModelFromHLinkString(HLinkKey);
                }

                return _Deref;
            }
        }

        /// <summary>
        /// Compares to. Bases it on the HLInkKey for want of anything else that makes sense.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public new int CompareTo(object obj)
        {
            HLinkAdressModel arg = obj as HLinkAdressModel;

            // Null objects go first
            if (arg is null)
            {
                return 1;
            }

            // Can only comapre if they are the same type so assume equal
            if (arg.GetType() != typeof(HLinkAdressModel))
            {
                return 0;
            }

            return DeRef.CompareTo(arg.DeRef);
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, "AddressDetailPage");

            return;
        }
    }
}