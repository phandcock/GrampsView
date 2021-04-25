namespace GrampsView.UserControls
{
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;

    using Xamarin.Forms;

    public partial class CollectionSingleCard : Frame, INotifyPropertyChanged
    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(IEnumerable), declaringType: typeof(CollectionSingleCard), propertyName: nameof(FsctSource)); //, propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty FsctTemplateProperty
                    = BindableProperty.Create(nameof(FsctTemplate), returnType: typeof(DataTemplate), declaringType: typeof(CollectionSingleCard), propertyChanged: OnItemTemplateChanged);

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
            get
            {
                return (IEnumerable)GetValue(FsctSourceProperty);
            }
            set
            {
                SetValue(FsctSourceProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the FSCT template.
        /// </summary>
        /// <value>
        /// The Control Item Template.
        /// </value>
        public DataTemplate FsctTemplate
        {
            get
            {
                return (DataTemplate)GetValue(FsctTemplateProperty);
            }
            set
            {
                SetValue(FsctTemplateProperty, value);
            }
        }

        ///// <summary>
        ///// Called when [items source changed].
        ///// </summary>
        ///// <param name="argSource">
        ///// The argument source.
        ///// </param>
        ///// <param name="oldValue">
        ///// The old value.
        ///// </param>
        ///// <param name="newValue">
        ///// The new value.
        ///// </param>
        //public static void OnItemsSourceChanged(BindableObject argSource, object oldValue, object newValue)
        //{
        //    //Contract.Assert(argSource != null);

        // //if (newValue is null) //{ // return; //}

        // //CollectionSingleCard layout = argSource as CollectionSingleCard;
        // //Contract.Requires(layout != null);

        // //IEnumerable iSource = newValue as IEnumerable;

        //    //layout.theCollectionView.ItemsSource = iSource;
        //}

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

            layout.theCollectionView.ItemTemplate = iTemplate;
        }
    }
}