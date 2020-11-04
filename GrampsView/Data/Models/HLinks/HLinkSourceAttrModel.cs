// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkSourceAttrModel : HLinkBase, IHLinkSourceAttrModel
    {
    }
}