// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;
    using GrampsView.Views;

    using System.Runtime.Serialization;

    using Xamarin.Forms;

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkPersonModel : HLinkBase, IHLinkPersonModel
    {
        private PersonModel _Deref = new PersonModel();

        public HLinkPersonModel()
        {
            UCNavigateCommand = new Command<HLinkPersonModel>(UCNavigate);
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

        // TODO Why pass HLinkPersonModel to HLinkPersonModel?
        public async void UCNavigate(HLinkPersonModel argHLink)
        {
            await UCNavigateBase(argHLink, nameof(PersonDetailPage));
        }
    }
}