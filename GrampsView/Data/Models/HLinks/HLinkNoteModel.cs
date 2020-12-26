// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.Forms;

    [DataContract]
    public class HLinkNoteModel : HLinkBase, IHLinkNoteModel
    {
        private NoteModel _Deref = new NoteModel();

        public HLinkNoteModel()
        {
            UCNavigateCommand = new Command<HLinkNoteModel>(UCNavigate);
        }

        public INoteModel DeRef
        {
            get
            {
                if (Valid && (!_Deref.Valid))
                {
                    _Deref = DV.NoteDV.GetModelFromHLinkString(HLinkKey);
                }

                return _Deref;
            }
        }

        public async void UCNavigate(HLinkNoteModel argHLink)
        {
            string jason = await Task.Run(() => CommonRoutines.SerialiseObject<HLinkNoteModel>(argHLink));

            await Shell.Current.GoToAsync(string.Format("NoteDetailPage?BaseParamsHLink={0}", jason));
        }
    }
}