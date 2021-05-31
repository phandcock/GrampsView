namespace GrampsView.Data.Model
{
    using GrampsView.Common.CustomClasses;

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