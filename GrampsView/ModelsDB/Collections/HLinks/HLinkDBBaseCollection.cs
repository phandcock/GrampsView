// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Models.HLinks;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// GRAMPS $$(Hlink)$$ element class.
    /// </summary>

    public class HLinkDBBaseCollection<T> : DBCardGroupHLink<T>, IHLinkDBCollectionBase<T>
         where T : HLinkDBBase, new()
    {
        // TODO Handle HLink collections properly by handling all their data

        /// <summary>
        /// Gets or sets the first image hlink.
        /// </summary>
        public ItemGlyph FirstHLinkHomeImage { get; set; } = new ItemGlyph();

        public T GetFirst
        {
            get
            {
                if (Count == 0)
                {
                    return new T();
                }

                return this.First();
            }
        }

        public virtual DBCardGroupHLink<T> GetCardGroupBase(string argTitle = "")
        {
            DBCardGroupHLink<T> t = GetCardGroupBase();

            if (!string.IsNullOrEmpty(argTitle))
            {
                t.Title = argTitle;
            };

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
            // TODO Fix this
            // Sort the collection
            //List<T> t = this.OrderBy(x => x.DeRef.ToString()).ToList();

            //Items.Clear();

            //foreach (T item in t)
            //{
            //    Items.Add(item);
            //}
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