﻿namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// <para>Repository HLink.</para>
    /// <para>XML 1.71 check done</para>
    /// </summary>
    [DataContract]
    public class HLinkRepositoryModel : HLinkBase, IHLinkRepositoryModel
    {
        private RepositoryModel _Deref = new RepositoryModel();

        private bool DeRefCached = false;

        public HLinkRepositoryModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconRepository;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundRepository");
        }

        /// <summary>
        /// Gets the dereference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public new RepositoryModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.RepositoryDV.GetModelFromHLinkKey(HLinkKey);
                    DeRefCached = true;
                }

                return _Deref;
            }
        }

        /// <summary>
        /// Gets or sets the call no.
        /// </summary>
        /// <value>
        /// The call no.
        /// </value>
        [DataMember]
        public string GCallNo
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the medium.
        /// </summary>
        /// <value>
        /// The medium.
        /// </value>
        [DataMember]
        public string GMedium
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the note reference.
        /// </summary>
        /// <value>
        /// The note reference.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRef
        {
            get;
            set;
        }

        = new HLinkNoteModelCollection();

        /// <summary>
        /// Navigates to a Repository detail page.
        /// </summary>
        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, "RepositoryDetailPage");
            return;
        }
    }
}