// XML 171 - Not in definition so created this for use with BackLink functionality

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Models.DataModels.Minor;
using GrampsView.Models.HLinks;
using GrampsView.Views;

using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GrampsView.Data.Model
{
    public class HLinkPersonNameModel : HLinkBase, IHLinkPersonNameModel
    {
        private PersonNameModel _Deref = new();

        private bool DeRefCached = false;

        public HLinkPersonNameModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconPersonName;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundSource");
        }

        [JsonIgnore]
        public PersonNameModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.PersonNameDV.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
                }

                return _Deref;
            }
        }

        /// <summary>
        /// Compares to. Bases it on the HLinkKey for want of anything else that makes sense.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public new int CompareTo(object obj)
        {
            // Null objects go first
            if (obj is not HLinkPersonNameModel arg)
            {
                return 1;
            }

            // Can only comapre if they are the same type so assume equal
            return arg.GetType() != typeof(HLinkPersonNameModel) ? 0 : DeRef.CompareTo(arg.DeRef);
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(PersonNameDetailPage));
        }

        //protected override IModelBase GetDeRef()
        //{
        //    return this.DeRef;
        //}
    }
}