// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkNoteModel : HLinkBase, IHLinkNoteModel
    {
        public INoteModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.NoteDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return new NoteModel();
                }
            }
        }
    }
}