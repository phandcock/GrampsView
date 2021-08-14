// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;

    using System.Collections.Generic;
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

        public virtual CardGroup GetCardGroup()
        {
            CardGroup t = new CardGroup();

            foreach (T item in Items)
            {
                t.Add(item);
            }

            t.Title = Title;

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

        /// <summary>
        /// <para> Sets the first image. </para>
        /// <para>
        /// Set the first or last image link. Chooses the first or last image in the collection
        /// <br/> depending on the user settings.
        /// </para>
        /// </summary>
        public void SetFirstImage()
        {
            FirstHLinkHomeImage = new ItemGlyph();

            if (Count > 0)
            {
                if (CommonLocalSettings.UseFirstImageFlag)
                {
                    FirstHLinkHomeImage = GetImage(0);
                }
                else
                {
                    FirstHLinkHomeImage = GetImage(Count - 1);
                }
            }
        }

        public virtual void SetGlyph()
        {
            //// Set the first image link. Assumes main image is manually set to the first image in
            //// Gramps if we need it to be, e.g. Citations.
            SetFirstImage();

            if (CommonLocalSettings.SortHLinkCollections)
            {
                Sort();
            }
        }

        public virtual void Sort()
        {
            // Sort the collection
            List<T> t = this.OrderBy(x => x.DeRef.DefaultText).ToList();

            Items.Clear();

            foreach (T item in t)
            {
                Items.Add(item);
            }
        }

        private ItemGlyph GetImage(int argIndex)
        {
            FirstHLinkHomeImage = new ItemGlyph();

            switch (this[argIndex].HLinkGlyphItem.ImageType)
            {
                case CommonEnums.HLinkGlyphType.Image:
                    {
                        FirstHLinkHomeImage = this[argIndex].HLinkGlyphItem;

                        break;
                    }
                case CommonEnums.HLinkGlyphType.Media:
                    {
                        FirstHLinkHomeImage = this[argIndex].HLinkGlyphItem;
                        FirstHLinkHomeImage.ImageType = CommonEnums.HLinkGlyphType.Image;

                        break;
                    }

                case CommonEnums.HLinkGlyphType.Symbol:
                    break;

                case CommonEnums.HLinkGlyphType.Unknown:
                    break;

                default:
                    break;
            }

            return FirstHLinkHomeImage;
        }
    }
}