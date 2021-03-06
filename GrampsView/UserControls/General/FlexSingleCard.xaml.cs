﻿namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;

    using Xamarin.Forms;

    public partial class FlexSingleCard : Frame, INotifyPropertyChanged

    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(IEnumerable), declaringType: typeof(FlexSingleCard), propertyName: nameof(FsctSource), propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty FsctTemplateProperty
                    = BindableProperty.Create(nameof(FsctTemplate), returnType: typeof(DataTemplate), declaringType: typeof(FlexSingleCard));

        /// <summary>
        /// Initializes a new instance of the <see cref="FlexSingleCard"/> class.
        /// </summary>
        public FlexSingleCard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the Fsct source.
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
        /// Gets or sets the Fsct template.
        /// </summary>
        /// <value>
        /// The Control Item Template.
        /// </value>
        public DataTemplate FsctTemplate
        {
            get => (DataTemplate)GetValue(FsctTemplateProperty);
            set => SetValue(FsctTemplateProperty, value);
        }

        public static void OnItemsSourceChanged(BindableObject argSource, object oldValue, object newValue)
        {
            Contract.Assert(argSource != null);

            FlexSingleCard thisCard = argSource as FlexSingleCard;

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

            // Set Justification to Center if only one column
            if (CardSizes.Current.CardsAcrossColumns == 1)
            {
                thisCard.theCollectionView.JustifyContent = FlexJustify.Center;
            }
            else
            {
                thisCard.theCollectionView.JustifyContent = FlexJustify.Start;
            }
        }
    }
}