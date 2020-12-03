namespace GrampsView.UserControls
{
    using System.Collections;
    using System.ComponentModel;

    using Xamarin.Forms;

    public partial class FlexSingleCard : Frame, INotifyPropertyChanged
    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(IEnumerable), declaringType: typeof(CollectionSingleCard), propertyName: nameof(FsctSource)); //, propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty FsctTemplateProperty
                    = BindableProperty.Create(nameof(FsctTemplate), returnType: typeof(DataTemplate), declaringType: typeof(CollectionSingleCard)); //, propertyChanged: OnItemTemplateChanged);

        /// <summary>
        /// Initializes a new instance of the <see cref="FlexSingleCard"/> class.
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
    }
}