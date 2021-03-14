using GrampsView.Common.CustomClasses;

namespace GrampsView.Data.Model
{
    public interface IHLinkCollectionBase<T>
    {
        int Count
        {
            get;
        }

        ItemGlyph FirstHLinkHomeImage
        {
            get; set;
        }

        void SetGlyph();
    }
}