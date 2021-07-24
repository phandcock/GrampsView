namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// HLink to an Address model. Not in XML Schema so use the standard hlink
    /// </summary>
    [DataContract]
    public class HLinkAdressModel : HLinkBase, IHLinkAddressModel
    {
        public HLinkAdressModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconAddress;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundAddress");
        }

        /// <summary>
        /// Gets and sets the address model.
        /// </summary>
        /// <value>
        /// The address model.
        /// </value>
        public new AddressModel DeRef

        {
            get;

            set;
        } = new AddressModel();

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