// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Data.Collections;

    /// <summary>
    /// Child Reference model
    /// </summary>
    public class ChildRefModel : ModelBase, IChildRefModel
    {
        /// <summary>
        /// Citation collection reference.
        /// </summary>
        /// <value>
        /// The citation collection reference.
        /// </value>
        public HLinkCitationModelCollection GCitationCollectionReference
        {
            get;
        }

        = new HLinkCitationModelCollection();

        public string GGFRel
        {
            get;
            set;
        }

        = string.Empty;

        public string GGMRel
        {
            get;
            set;
        }

        = string.Empty;

        public HLinkPersonModel GHLink
        {
            get;
            set;
        }

            = new HLinkPersonModel();

        /// <summary>
        /// Gets the note collection reference.
        /// </summary>
        /// <value>
        /// The g note collection reference.
        /// </value>
        public HLinkNoteModelCollection GNoteCollectionReference
        {
            get;
        }

        = new HLinkNoteModelCollection();

        public bool GPriv
        {
            get;
            set;
        }
    }
}