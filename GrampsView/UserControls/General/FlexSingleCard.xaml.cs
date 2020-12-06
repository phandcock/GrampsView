namespace GrampsView.UserControls
{
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;

    using Xamarin.Forms;

    public partial class FlexSingleCard : Frame, INotifyPropertyChanged

    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(IEnumerable), declaringType: typeof(FlexSingleCard), propertyName: nameof(FsctSource), propertyChanged: OnItemsSourceChanged);

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
        }
    }
}