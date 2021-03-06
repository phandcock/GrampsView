﻿// TODO Needs XML 1.71 check

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
        // NOTE: This cannot default to a PersonModel as there is a recursive relationship with FamilyModel
        private PersonModel _Deref = null;

        private bool DeRefCached = false;

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
        public new PersonModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.PersonDV.GetModelFromHLinkKey(HLinkKey);
                    DeRefCached = true;
                }

                if (_Deref is null)
                {
                    _Deref = new PersonModel();
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