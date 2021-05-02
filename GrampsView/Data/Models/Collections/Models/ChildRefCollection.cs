/// <summary>
/// XML 1.71 check not required as not part of Gramps model
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Data.Model;

    using System.Runtime.Serialization;

    /// <summary>
    /// ChildRefmodel collection
    /// </summary>

    [CollectionDataContract]
    [KnownType(typeof(HLinkBaseCollection<HLinkChildRefModel>))]
    public class ChildRefCollectionCollection : HLinkBaseCollection<HLinkChildRefModel>
    {
        public ChildRefCollectionCollection()
        {
            Title = "Child Reference Collection";
        }

        public override void SetGlyph()
        {
            foreach (HLinkChildRefModel argHLink in this)
            {
                HLinkPersonModel t = argHLink.GetHLinkPerson;

                if (t.Valid)
                {
                    argHLink.HLinkGlyphItem = t.HLinkGlyphItem;
                }
            }
        }
    }
}