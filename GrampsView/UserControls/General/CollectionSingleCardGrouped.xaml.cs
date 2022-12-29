using PropertyChanged;

using SharedSharp.Common.Interfaces;
using SharedSharp.Messages;

using System.Diagnostics.Contracts;

namespace GrampsView.UserControls
{
    public partial class CollectionSingleCardGrouped : Border
    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(object), declaringType: typeof(CollectionSingleCardGrouped), propertyName: nameof(FsctSource));

        public static readonly BindableProperty FsctTemplateProperty
                    = BindableProperty.Create(nameof(FsctTemplate), returnType: typeof(DataTemplate), declaringType: typeof(CollectionSingleCardGrouped), propertyChanged: OnItemTemplateChanged);

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionSingleCardGrouped"/> class.
        /// </summary>
        public CollectionSingleCardGrouped()
        {
            InitializeComponent();

            Ioc.Default.GetRequiredService<IMessenger>().Register<SSharpMessageWindowSizeChanged>(this, (r, m) =>
            {
                NumColumns = Ioc.Default.GetRequiredService<ISharedSharpCardSizes>().CardsAcrossColumns;
                //Debug.WriteLine(NumColumns);
            });
        }

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
        [SuppressPropertyChangedWarnings]
        public static void OnItemTemplateChanged(BindableObject argSource, object oldValue, object newValue)
        {
            Contract.Requires(argSource != null);
            Contract.Requires(newValue != null);

            CollectionSingleCardGrouped? layout = argSource as CollectionSingleCardGrouped;
            Contract.Requires(layout != null);

            DataTemplate? iTemplate = newValue as DataTemplate;

            layout.theCollectionView.ItemTemplate = iTemplate;
        }




    }
}