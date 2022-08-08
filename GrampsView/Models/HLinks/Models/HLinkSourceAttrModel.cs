// TODO Needs XML 1.71 check

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkSourceAttrModel : HLinkBase, IHLinkSourceAttrModel
    {
        protected override IModelBase GetDeRef()
        {
            return this.DeRef;
        }
    }
}