namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Views;

    using System.Threading.Tasks;

    /// <summary>
    /// <para> HLink to an Attribute model. </para>
    /// <list type="table">
    /// <listheader>
    /// <term> Item </term>
    /// <term> Status </term>
    /// </listheader>
    /// <item>
    /// <description> XML 1.71 check </description>
    /// <description> Not in XML 1.71 so use the base HLink. </description>
    /// </item>
    /// </list>
    /// </summary>

    public class HLinkFamilyGraphModel : HLinkBase, IHLinkParentLinkModel
    {
        public HLinkFamilyGraphModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconAttribute;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundAttribute");
        }

        /// <summary>
        /// Gets the dereference.
        /// </summary>
        /// <value>
        /// The dereference.
        /// </value>

        public PersonModel DeRef
        {
            get;

            set;
        } = new PersonModel();

        public Group<object> Families
        {
            get
            {
                Group<object> returnValue = new Group<object>();
                foreach (HLinkFamilyModel currentFamily in DeRef.GParentInRefCollection)
                {
                    currentFamily.DisplayAs = CommonEnums.DisplayFormat.LargeCard;
                    returnValue.Add(currentFamily);
                }

                return returnValue;
            }
        }

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

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this.DeRef.GChildOf, nameof(FamilyDetailPage));
            return;
        }
    }
}