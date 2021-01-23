// XML 1.71 All done

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkTagModel : HLinkBase, IHLinkTagModel
    {
        /// <summary>
        /// The local image h link.
        /// </summary>
        private HLinkMediaModel localImageHLink = new HLinkMediaModel();

        public HLinkTagModel()
        {
            UCNavigateCommand = new AsyncCommand<HLinkTagModel>(NavPage => UCNavigate(NavPage));
        }

        /// <summary>
        /// Gets the dereferenced Tag ViewModel.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public TagModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.TagDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the image h link key.
        /// </summary>
        /// <value>
        /// The image h link key.
        /// </value>
        [DataMember]
        public HLinkMediaModel HomeImageHLink
        {
            get
            {
                return localImageHLink;
            }

            set
            {
                SetProperty(ref localImageHLink, value);
            }
        }

        public IAsyncCommand<HLinkTagModel> UCNavigateCommand
        {
            get; set;
        }

        public async Task UCNavigate(HLinkTagModel argHLink)
        {
            await UCNavigateBase(argHLink, "TagDetailPage");
            return;
        }
    }
}