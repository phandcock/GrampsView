// TODO Needs XML 1.71 check

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Forms;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkNameMapModel : HLinkBase, IHLinkNameMapModel
    {
        public IAsyncCommand<HLinkNameMapModel> UCNavigateCommand { get; set; }

        public HLinkNameMapModel()
        {
            UCNavigateCommand = new AsyncCommand<HLinkNameMapModel>(NavPage =>UCNavigate(NavPage));
        }

        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public NameMapModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return new NameMapDataView().GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task UCNavigate(HLinkNameMapModel argHLink)
        {
            await UCNavigateBase(argHLink, "NameMapDetailPage");
            return;
            }
    }
}