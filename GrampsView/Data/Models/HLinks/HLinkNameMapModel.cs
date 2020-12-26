// TODO Needs XML 1.71 check

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.Forms;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkNameMapModel : HLinkBase, IHLinkNameMapModel
    {
        public HLinkNameMapModel()
        {
            UCNavigateCommand = new Command<HLinkNameMapModel>(UCNavigate);
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

        public async void UCNavigate(HLinkNameMapModel argHLink)
        {
            string jason = await Task.Run(() => CommonRoutines.SerialiseObject<HLinkNameMapModel>(argHLink));

            await Shell.Current.GoToAsync(string.Format("MediaDetailPage?BaseParamsHLink={0}", jason));
        }
    }
}