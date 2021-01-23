// XML 171 - All fields defined

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;
    using GrampsView.Views;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Forms;

    /// <summary>
    /// HLink to the Family Model.
    /// </summary>
    [DataContract]
    public class HLinkFamilyModel : HLinkBase, IHLinkFamilyModel
    {
        public IAsyncCommand<HLinkFamilyModel> UCNavigateCommand { get; set; }

        public HLinkFamilyModel()
        {
            UCNavigateCommand = new AsyncCommand<HLinkFamilyModel>(NavPage =>UCNavigate(NavPage));
        }

        /// <summary>
        /// Gets.
        /// </summary>
        public FamilyModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return new FamilyDataView().GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return new FamilyModel();
                }
            }
        }

        public async Task UCNavigate(HLinkFamilyModel argHLink)
        {
            await UCNavigateBase(argHLink, nameof(FamilyDetailPage));
            return;
            }
    }
}