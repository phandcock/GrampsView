// TODO Needs XML 1.71 check

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

        public async void UCNavigate(HLinkPersonModel argHLink)
        {
            string jason = await Task.Run(() => CommonRoutines.SerialiseObject<HLinkPersonModel>(argHLink));

            await Shell.Current.GoToAsync(string.Format("PersonDetailPage?BaseParamsHLink={0}", jason));
        }
    }
}