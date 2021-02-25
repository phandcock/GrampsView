// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS $$(Hlink)$$ element class.
    /// </summary>
    [CollectionDataContract]
    public class HLinkBaseCollection<T> : CardGroupBase<T>, IHLinkCollectionBase<T>
         where T : HLinkBase, new()
    {
        // TODO Handle HLink collections properly by handling all their data

        public virtual CardGroup CardGroupAsProperty
        {
            get
            {
                return GetCardGroup();
            }
        }

        /// <summary>
        /// Gets or sets the first image hlink.
        /// </summary>
        public ItemGlyph FirstHLinkHomeImage { get; set; } = new ItemGlyph();

        public T GetFirst
        {
            get
            {
                return this.FirstOrDefault();
            }
        }

        public virtual CardGroup GetCardGroup(string argTitle = "")
        {
            CardGroup t = GetCardGroup();

            if (!string.IsNullOrEmpty(argTitle))
            {
                t.Title = argTitle;
            };

            return t;
        }

        public virtual CardGroup GetCardGroup()
        {
            CardGroup t = new CardGroup();

            foreach (T item in Items)
            {
                t.Add(item);
            }

            return t;
        }

        public virtual CardGroupBase<T> GetCardGroupBase(string argTitle = "")
        {
            CardGroupBase<T> t = GetCardGroupBase();

            if (!string.IsNullOrEmpty(argTitle))
            {
                t.Title = argTitle;
            };

            return t;
        }

        public virtual CardGroupBase<T> GetCardGroupBase()
        {
            CardGroupBase<T> t = new CardGroupBase<T>();

            foreach (T item in Items)
            {
                t.Add(item);
            }

            return t;
        }

        public virtual void SetGlyph()
        {
        }
    }
}