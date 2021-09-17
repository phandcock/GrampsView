namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// <para> Repository HLink. </para>
    /// <para> XML 1.71 check done </para>
    /// </summary>

    public class HLinkRepositoryModel : HLinkBase, IHLinkRepositoryModel
    {
        private RepositoryModel _Deref = new RepositoryModel();

        private bool DeRefCached = false;

        public HLinkRepositoryModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconRepository;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundRepository");
        }

        /// <summary>
        /// Gets the dereference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        [JsonIgnore]
        public new RepositoryModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.RepositoryDV.GetModelFromHLinkKey(HLinkKey);
                    DeRefCached = true;
                }

                return _Deref;
            }
        }

        /// <summary>
        /// Navigates to a Repository detail page.
        /// </summary>
        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, "RepositoryDetailPage");
            return;
        }

        //protected override IModelBase GetDeRef()
        //{
        //    return this.DeRef;
        //}
    }
}