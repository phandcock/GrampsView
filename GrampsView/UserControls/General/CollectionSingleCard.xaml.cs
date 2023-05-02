// Copyright (c) phandcock.  All rights reserved.

using System.Collections;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace GrampsView.UserControls
{
    public partial class CollectionSingleCard : Border, INotifyPropertyChanged
    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(IEnumerable), declaringType: typeof(CollectionSingleCard), propertyName: nameof(FsctSource), propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty FsctTemplateProperty
                    = BindableProperty.Create(nameof(FsctTemplate), returnType: typeof(DataTemplate), declaringType: typeof(CollectionSingleCard));

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionSingleCard"/> class.
        /// </summary>
        public CollectionSingleCard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the FSCT source.
        /// </summary>
        /// <value>
        /// The Control Item Source.
        /// </value>
        public IEnumerable FsctSource
        {
            get => (IEnumerable)GetValue(FsctSourceProperty);
            set => SetValue(FsctSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the FSCT template.
        /// </summary>
        /// <value>
        /// The Control Item Template.
        /// </value>
        public DataTemplate FsctTemplate
        {
            get => (DataTemplate)GetValue(FsctTemplateProperty);
            set => SetValue(FsctTemplateProperty, value);
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Contract.Assert(bindable != null);

            CollectionSingleCard? thisCard = bindable as CollectionSingleCard;

            if (newValue is null)
            {
                thisCard.IsVisible = false;
                return;
            }

            // TODO cleanup this code when we work out how
            IEnumerator counter = thisCard.FsctSource.GetEnumerator();

            if (counter.MoveNext())
            {
                // We have some data
                thisCard.IsVisible = true;
            }
            else
            {
                thisCard.IsVisible = false;
            }
        }
    }
}