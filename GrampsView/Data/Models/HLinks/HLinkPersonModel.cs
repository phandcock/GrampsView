// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;
    using GrampsView.Views;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkPersonModel : HLinkBase, IHLinkPersonModel
    {
        private PersonModel _Deref = new PersonModel();

        public HLinkPersonModel()
        {
            UCNavigateCommand = new AsyncCommand<HLinkPersonModel>(NavPage => UCNavigate(NavPage));
        }

        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public PersonModel DeRef
        {
            get
            {
                if (Valid & (!_Deref.Valid))
                {
                    _Deref = DV.PersonDV.GetModelFromHLinkString(HLinkKey);
                }

                return _Deref;
            }
        }

        public IAsyncCommand<HLinkPersonModel> UCNavigateCommand
        {
            get;
        }

        // TODO Why pass HLinkPersonModel to HLinkPersonModel?
        public async Task UCNavigate(HLinkPersonModel argHLink)
        {
            await UCNavigateBase(argHLink, nameof(PersonDetailPage));
            return;
        }
    }
}