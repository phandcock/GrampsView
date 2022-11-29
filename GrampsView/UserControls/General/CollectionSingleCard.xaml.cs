using SharedSharp.Common.Interfaces;
using SharedSharp.Messages;

using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace GrampsView.UserControls
{
    public partial class CollectionSingleCard : Border, INotifyPropertyChanged
    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(object), declaringType: typeof(CollectionSingleCard), propertyName: nameof(FsctSource), propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty FsctTemplateProperty
                    = BindableProperty.Create(nameof(FsctTemplate), returnType: typeof(DataTemplate), declaringType: typeof(CollectionSingleCard), propertyChanged: OnItemTemplateChanged);

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionSingleCard"/> class.
        /// </summary>
        public CollectionSingleCard()
        {
            InitializeComponent();

            Ioc.Default.GetService<IMessenger>().Register<SSharpMessageWindowSizeChanged>(this, (r, m) =>
            {
                CardsAcross = Ioc.Default.GetService<ISharedSharpCardSizes>().CardsAcrossColumns;
            });
        }

        public int CardsAcross { get; set; } = 2;

        /// <summary>
        /// Gets or sets the FSCT source.
        /// </summary>
        /// <value>
        /// The Control Item Source.
        /// </value>
        public object FsctSource
        {
            get => GetValue(FsctSourceProperty);
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

        /// <summary>
        /// Gets or sets the number columns to display.
        /// </summary>
        /// <value>
        /// The number columns.
        /// </value>
        public int NumColumns
        {
            get; set;
        } = 3;

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

            CollectionSingleCard? layout = argSource as CollectionSingleCard;
            Contract.Requires(layout != null);

            DataTemplate? iTemplate = newValue as DataTemplate;

            layout.theCollectionView.ItemTemplate = iTemplate;
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
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
        private void CollectionSingleCardGroupedRoot_SizeChanged(object sender, EventArgs e)
        {
            Contract.Requires(sender != null);

            CollectionSingleCard? t = sender as CollectionSingleCard;

            NumColumns = (int)((t.Width / Ioc.Default.GetService<ISharedSharpCardSizes>().CardSmallWidth) + 1);  // +1 for padding
        }
    }
}