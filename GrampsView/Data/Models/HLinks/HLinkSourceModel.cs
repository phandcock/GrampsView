// TODO Needs XML 1.71 check

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkSourceModel : HLinkBase, IHLinkSourceModel
    {
        /// <summary>
        /// Gets the model from the HLink.
        /// </summary>
        /// <value>
        /// The HLink reference.
        /// </value>
        public SourceModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.SourceDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return new SourceModel();
                }
            }
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="argOobj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public int CompareTo(HLinkSourceModel argOobj) => DeRef.CompareTo(argOobj);

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="argObj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public new int CompareTo(object argObj)
        {
            // Null objects go first
            if (argObj is null) { return 1; }

            // Can only comapre if they are the same type so assume equal
            if (argObj.GetType() != typeof(HLinkSourceModel))
            {
                return 0;
            }

            return DeRef.CompareTo((argObj as HLinkSourceModel).DeRef);
        }
    }
}