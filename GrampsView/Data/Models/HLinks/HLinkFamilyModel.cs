// XML 171 - All fields defined

// TODO fix Deref caching

/// <summary>
/// </summary>

/// TODO Update fields as per Schema
namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;

    /// <summary>
    /// HLink to the Family Model.
    /// </summary>
    [DataContract]
    public class HLinkFamilyModel : HLinkBase, IHLinkFamilyModel
    {
        /// <summary>
        /// Gets.
        /// </summary>
        public FamilyModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return new FamilyDataView().GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return new FamilyModel();
                }
            }
        }
    }
}