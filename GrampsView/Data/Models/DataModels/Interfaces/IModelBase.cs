namespace GrampsView.Data.Model
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Collections;

    using System;
    using System.ComponentModel;

    /// <summary>
    /// Public interfaces for the Event elements.
    /// </summary>
    public interface IModelBase : IComparable<ModelBase>, INotifyPropertyChanged
    {
        HLinkBackLinkModelCollection BackHLinkReferenceCollection
        {
            get;
        }

        DateTime Change
        {
            get; set;
        }

        /// <summary>
        /// Gets the default text.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        string DefaultText
        {
            get;
        }

        string DefaultTextShort
        {
            get;
        }

        string DefaultTextSort
        {
            get;
        }

        /// <summary>
        /// Gets or sets the model handle.
        /// </summary>
        /// <value>
        /// The handle.
        /// </value>
        string Handle
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the h link key.
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