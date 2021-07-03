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

        private bool DeRefCached = false;

        public HLinkNoteModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconNotes;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundNote");
        }

        public new INoteModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.NoteDV.GetModelFromHLinkKey(HLinkKey);
                    DeRefCached = true;
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