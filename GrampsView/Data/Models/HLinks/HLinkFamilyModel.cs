// XML 171 - All fields defined

// TODO fix Deref caching

/// <summary>
/// </summary>

/// TODO Update fields as per Schema
namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.Forms;

    /// <summary>
    /// HLink to the Family Model.
    /// </summary>
    [DataContract]
    public class HLinkFamilyModel : HLinkBase, IHLinkFamilyModel
    {
        public HLinkFamilyModel()
        {
            UCNavigateCommand = new Command<HLinkFamilyModel>(UCNavigate);
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

        public async void UCNavigate(HLinkFamilyModel argHLink)
        {
            string jason = await Task.Run(() => CommonRoutines.SerialiseObject<HLinkFamilyModel>(argHLink));

            await Shell.Current.GoToAsync(string.Format("FamilyDetailPage?BaseParamsHLink={0}", jason));
        }
    }
}