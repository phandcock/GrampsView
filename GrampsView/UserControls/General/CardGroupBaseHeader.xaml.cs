namespace GrampsView.UserControls
{
    using System.ComponentModel;

    using Xamarin.Forms;

    public partial class CardGroupBaseHeader : Frame, INotifyPropertyChanged
    {
        public static readonly BindableProperty FsctTemplateProperty
                    = BindableProperty.Create(nameof(FsctTemplate), returnType: typeof(DataTemplate), declaringType: typeof(CardGroupBaseHeader));

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionSingleCard"/> class.
        /// </summary>
        public CardGroupBaseHeader()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when we want to trigger a property changed event.
        /// </summary>
        public event PropertyChangedEventHandler MyPropertyChanged;

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

        /// <summary>
        /// Method that is called when a bound property is changed.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the bound property that changed.
        /// </param>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected new void OnPropertyChanged(string propertyName)
        {
            MyPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CardGroupBaseHeaderRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
        }
    }
}