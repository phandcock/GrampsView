// TODO Needs XML 1.71 check

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;

    /// <summary>
    /// HLink to the Header model.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkHeaderModel : HLinkBase, IHLinkHeaderModel
    {
        /// <summary>
        /// Gets the dereference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public HeaderModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return new HeaderDataView().GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}