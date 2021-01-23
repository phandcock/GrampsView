// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
    {
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkMediaModel : HLinkBase, IHLinkMediaModel
        {
        private MediaModel _Deref = new MediaModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="HLinkMediaModel"/> class.
        /// </summary>
        public HLinkMediaModel()
            {
            UCNavigateCommand = new AsyncCommand<HLinkMediaModel>(NavPage => UCNavigate(NavPage));
            }

        /// <summary>
        /// Gets the associated media model
        /// </summary>
        /// <value>
        /// The media model. <note type="caution">This can not hold a local copy of the media model
        /// as the Model Base has a hlinkmediamodel in it and this will cause a referene loop</note>
        /// </value>
        public IMediaModel DeRef
            {
            get
                {
                if (Valid && (!_Deref.Valid))
                    {
                    _Deref = DV.MediaDV.GetModelFromHLinkString(HLinkKey);
                    }

                return _Deref;
                }
            }

        /// <summary>
        /// Gets or sets the Attribute.
        /// </summary>
        /// <value>
        /// The Attribute.
        /// </value>
        [DataMember]
        public OCAttributeModelCollection GAttributeRefCollection { get; set; } = new OCAttributeModelCollection();

        /// <summary>
        /// Gets or sets the g citation model collection.
        /// </summary>
        /// <value>
        /// The g citation model collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection { get; set; } = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets or sets the g note model collection.
        /// </summary>
        /// <value>
        /// The g note model collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection { get; set; } = new HLinkNoteModelCollection();

        public IAsyncCommand<HLinkMediaModel> UCNavigateCommand { get; set; }

        /// <summary>
        /// Gets a value indicating whether gets boolean showing if the $$(HLink)$$ is valid. <note
        /// type="note">Can have a HLink or be a pointer to an image. <br/><br/> So, MUST be valid
        /// for both types and MUST be invalid for a default new instance. <br/></note>
        /// </summary>
        /// <value>
        /// Boolean showing if $$(HLink)$$ is valid.
        /// </value>
        public override bool Valid
            {
            get
                {
                return !string.IsNullOrEmpty(HLinkKey);
                }
            }

        public async Task UCNavigate(HLinkMediaModel argHLink)
            {
            await UCNavigateBase(argHLink, "MediaDetailPage");
            return;
            }
        }
    }