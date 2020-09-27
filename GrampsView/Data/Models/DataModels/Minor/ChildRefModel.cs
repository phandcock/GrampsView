namespace GrampsView.Data.Model
{
    using GrampsView.Data.Collections;

    /// <summary>
    /// GRAMPS Alt element class.
    /// </summary>
    /// TODO Update fields as per Schema
    public class ChildRefModel : ModelBase, IChildRefModel
    {
        /// <summary>
        /// Sets the child ref citation collection reference.
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
        /// Gets or sets the g note collection reference.
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