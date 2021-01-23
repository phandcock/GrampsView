// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;
    using GrampsView.Views;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    [DataContract]
    public class HLinkNoteModel : HLinkBase, IHLinkNoteModel
    {
        private NoteModel _Deref = new NoteModel();

        public HLinkNoteModel()
        {
            UCNavigateCommand = new AsyncCommand<HLinkNoteModel>(NavPage => UCNavigate(NavPage));
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

        public IAsyncCommand<HLinkNoteModel> UCNavigateCommand
        {
            get; set;
        }

        public async Task UCNavigate(HLinkNoteModel argHLink)
        {
            await UCNavigateBase(argHLink, nameof(NoteDetailPage));
            return;
        }
    }
}