// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Views;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// <para> HLink to a Person Model </para>
    /// <para> <font color="#333333"> Duh Core Model </font> </para>
    /// <list type="table">
    /// <listheader>
    /// <term> Item </term>
    /// <term> Status </term>
    /// </listheader>
    /// <item>
    /// <description> XML 1.71 check </description>
    /// <description> Not Done </description>
    /// </item>
    /// </list>
    /// <para> <br/> </para>
    /// </summary>
    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkPersonModel : HLinkBase, IHLinkPersonModel
    {
        // NOTE: This cannot default to a PersonModel as there is a recursive relationship with FamilyModel
        private PersonModel _Deref;

        private bool DeRefCached;

        public HLinkPersonModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconPeople;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPerson");
        }

        /// <summary>
        /// Gets the dereference.
        /// </summary>
        /// <value>
        /// The dereference.
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

        /// <summary>
        /// Converts to string. TODO fu. Mainly exists to allow workaround for Xamarin bug https://github.com/xamarin/Xamarin.Forms/issues/11075
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (DeRef != null)
            {
                return DeRef.DefaultTextShort;
            }

            return base.ToString();
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(PersonDetailPage));
            return;
        }

        protected override IModelBase GetDeRef()
        {
            return this.DeRef;
        }
    }
}