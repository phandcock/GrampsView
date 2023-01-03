using GrampsView.Common.CustomClasses;
using GrampsView.Data.Collections;
using GrampsView.Models.DataModels;

using System;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Public interfaces for the Event elements.
    /// </summary>
    public interface IModelBase
    {
        HLinkBackLinkModelCollection BackHLinkReferenceCollection
        {
            get;
        }

        DateTime Change
        {
            get; set;
        }

        string DefaultTextShort
        {
            get;
        }

        /// <summary>
        /// Gets or sets the handle link key.
        /// </summary>
        /// <value>
        /// The h link key.
        /// </value>
        HLinkKey HLinkKey
        {
            get; set;
        }

        string Id
        {
            get; set;
        }

        ItemGlyph ModelItemGlyph
        {
            get;

            set;
        }

        bool Priv
        {
            get; set;
        }

        bool Valid
        {
            get;
        }

        void LoadBasics(ModelBase argBasics);
    }
}