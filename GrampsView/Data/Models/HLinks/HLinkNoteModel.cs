// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;

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
            await UCNavigateBase(argHLink, "NoteDetailPage");
        }
    }
}