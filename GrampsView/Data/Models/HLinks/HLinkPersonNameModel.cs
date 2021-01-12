// XML 171 - Not in definition so created this for use with BackLink functionality

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;

    using Xamarin.Forms;

    [DataContract]
    public class HLinkPersonNameModel : HLinkBase, IHLinkPersonNameModel
    {
        public HLinkPersonNameModel()
        {
            UCNavigateCommand = new Command<HLinkPersonNameModel>(UCNavigate);
        }

        public PersonNameModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.PersonNameDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return new PersonNameModel();
                }
            }
        }

        /// <summary>
        /// Compares to. Bases it on the HLinkKey for want of anything else that makes sense.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public new int CompareTo(object obj)
        {
            HLinkPersonNameModel arg = obj as HLinkPersonNameModel;

            // Null objects go first
            if (arg is null) { return 1; }

            // Can only comapre if they are the same type so assume equal
            if (arg.GetType() != typeof(HLinkPersonNameModel))
            {
                return 0;
            }

            return DeRef.CompareTo(arg.DeRef);
        }

        public async void UCNavigate(HLinkPersonNameModel argHLink)
        {
            await UCNavigateBase(argHLink, nameof(PersonNameDataView));
        }
    }
}