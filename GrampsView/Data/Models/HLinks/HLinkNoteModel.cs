// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Views;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    [DataContract]
    public class HLinkNoteModel : HLinkBase, IHLinkNoteModel
    {
        private NoteModel _Deref = new NoteModel();

        public HLinkNoteModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconNotes;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundNote");
        }

        public INoteModel DeRef
        {
            get
            {
                if (Valid && (!_Deref.Valid))
                {
                    _Deref = DV.NoteDV.GetModelFromHLinkKey(HLinkKey);
                }

                return _Deref;
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(NoteDetailPage));
            return;
        }
    }
}