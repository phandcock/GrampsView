namespace GrampsView.UserControls
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;

    using Xamarin.Forms;

    public partial class FlexSingleCard : Frame, INotifyPropertyChanged
    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(IEnumerable), declaringType: typeof(CollectionSingleCard), propertyName: nameof(FsctSource), propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty FsctTemplateProperty
                    = BindableProperty.Create(nameof(FsctTemplate), returnType: typeof(DataTemplate), declaringType: typeof(CollectionSingleCard), propertyChanged: OnItemTemplateChanged);

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionSingleCard"/> class.
        /// </summary>
        public FlexSingleCard()
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
            get { return (IEnumerable)GetValue(FsctSourceProperty); }
            set { SetValue(FsctSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the FSCT template.
        /// </summary>
        /// <value>
        /// The Control Item Template.
        /// </value>
        public DataTemplate FsctTemplate
        {
            get { return (DataTemplate)GetValue(FsctTemplateProperty); }
            set { SetValue(FsctTemplateProperty, value); }
        }

        public static void OnItemsSourceChanged(BindableObject argSource, object oldValue, object newValue)
        {
            Contract.Assert(argSource != null);

            if (newValue is null)
            {
                return;
            }

            CollectionSingleCard layout = argSource as CollectionSingleCard;
            Contract.Requires(layout != null);

            IEnumerable iSource = newValue as IEnumerable;

            // layout.theCollectionView.ItemsSource = iSource;

            int Counter = 0;

            foreach (var item in iSource)
            {
                Counter++;
            }

            // layout.theCollectionView.NumRows = Counter;

            // layout.theCollectionView.SetUcHeight();
        }

        /// <summary>
        /// Called when [item template changed].
        /// </summary>
        /// <param name="argSource">
        /// The argument source.
        /// </param>
        /// <param name="oldValue">
        /// The old value.
        /// </param>
        /// <param name="newValue">
        /// The new value.
        /// </param>
        public static void OnItemTemplateChanged(BindableObject argSource, object oldValue, object newValue)
        {
            Contract.Requires(argSource != null);
            Contract.Requires(newValue != null);

            CollectionSingleCard layout = argSource as CollectionSingleCard;
            Contract.Requires(layout != null);

            DataTemplate iTemplate = newValue as DataTemplate;

            // layout.theCollectionView.ItemTemplate = iTemplate;
        }

        /// <summary>
        /// Handles the SizeChanged event of the CollectionSingleCardRoot control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void CollectionSingleCardRoot_SizeChanged(object sender, EventArgs e)
        {
            Contract.Requires(sender != null);

            CollectionSingleCard t = sender as CollectionSingleCard;

            //t.theCollectionView.NumColumns = (Int32)(t.Width / CardSizes.Current.CardSmallWidth);  // +1 for padding

            //t.theCollectionView.setUcHeight();
        }
    }
}