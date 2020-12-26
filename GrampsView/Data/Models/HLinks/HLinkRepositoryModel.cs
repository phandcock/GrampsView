// TODO Needs XML 1.71 check

// TODO fix Deref caching

/// <summary>
/// </summary>
/// "hlink" Done "priv" Done "callno" Done "medium" Done; "noteref" Done
namespace GrampsView.Data.Model
{
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Windows.Input;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkRepositoryModel : HLinkBase, IHLinkRepositoryModel
    {
        /// <summary>
        /// The local call no.
        /// </summary>
        private string localCallNo;

        /// <summary>
        /// The local medium.
        /// </summary>
        private string localMedium;

        /// <summary>
        /// The local image h link.
        /// </summary>
        //private HLinkMediaModel localImageHLink = new HLinkMediaModel();
        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public RepositoryModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.RepositoryDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return null;
                }
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
            get
            {
                return localCallNo;
            }

            set
            {
                SetProperty(ref localCallNo, value);
            }
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
            get
            {
                return localMedium;
            }

            set
            {
                SetProperty(ref localMedium, value);
            }
        }

        /// <summary>
        /// Gets or sets the g note reference.
        /// </summary>
        /// <value>
        /// The g note reference.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRef
        {
            get;
            set;
        }

        = new HLinkNoteModelCollection();

        public ICommand UCNavigateCommand { get; private set; }
        ///// <summary>
        ///// Gets or sets the image h link key.
        ///// </summary>
        ///// <value>
        ///// The image h link key.
        ///// </value>
        //[DataMember]
        //public HLinkMediaModel HomeImageHLink
        //{
        //    get
        //    {
        //        return localImageHLink;
        //    }

        //    set
        //    {
        //        SetProperty(ref localImageHLink, value);
        //    }
        //}
    }
}