// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;

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

        public void SetFirstImage()
        {
            // Set the first image link. Assumes main image is manually set to the first image in
            // Gramps if we need it to be, e.g. Citations.

            FirstHLinkHomeImage = new ItemGlyph();

            if (Count > 0)
            {
                // Step through each mediamodel hlink in the collection
                for (int i = 0; i < Count; i++)
                {
                    switch (this[i].HLinkGlyphItem.ImageType)
                    {
                        case CommonEnums.HLinkGlyphType.Image:
                            {
                                FirstHLinkHomeImage = this[i].HLinkGlyphItem;

                                // Stop after the first match
                                return;
                            }
                        case CommonEnums.HLinkGlyphType.Media:
                            {
                                FirstHLinkHomeImage = this[i].HLinkGlyphItem;
                                FirstHLinkHomeImage.ImageType = CommonEnums.HLinkGlyphType.Image;

                                // Stop after the first match
                                return;
                            }

                        case CommonEnums.HLinkGlyphType.Symbol:
                            break;

                        case CommonEnums.HLinkGlyphType.Unknown:
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        public virtual void SetGlyph()
        {
        }
    }
}