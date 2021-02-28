// TODO Needs XML 1.71 check

// TODO fix Deref caching

/// <summary>
/// </summary>
/// "hlink" Done "priv" Done "callno" Done "medium" Done; "noteref" Done
namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkRepositoryModel : HLinkBase, IHLinkRepositoryModel
    {
        /// <summary>
        /// The local call no.
        /// </summary>
        private string _CallNo;

        /// <summary>
        /// The local medium.
        /// </summary>
        private string _Medium;

        public HLinkRepositoryModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconRepository;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundRepository");
        }

        /// <summary>
        /// The local image h link.
        /// </summary>
        //private HLinkMediaModel localImageHLink = new HLinkMediaModel();
        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public RepositoryModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.RepositoryDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the call no.
        /// </summary>
        /// <value>
        /// The call no.
        /// </value>
        [DataMember]
        public string GCallNo
        {
            get
            {
                return _CallNo;
            }

            set
            {
                SetProperty(ref _CallNo, value);
            }
        }

        /// <summary>
        /// Gets or sets the medium.
        /// </summary>
        /// <value>
        /// The medium.
        /// </value>
        [DataMember]
        public string GMedium
        {
            get
            {
                return _Medium;
            }

            set
            {
                SetProperty(ref _Medium, value);
            }
        }

        /// <summary>
        /// Gets or sets the g note reference.
        /// </summary>
        /// <value>
        /// The g note reference.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRef
        {
            get;
            set;
        }

        = new HLinkNoteModelCollection();

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, "RepositoryDetailPage");
            return;
        }
    }
}