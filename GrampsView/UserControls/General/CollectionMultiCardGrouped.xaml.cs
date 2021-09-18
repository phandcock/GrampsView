namespace GrampsView.UserControls
{
    using System.ComponentModel;

    using Xamarin.Forms;

    public partial class CollectionMultiCardGrouped : Frame, INotifyPropertyChanged
    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(object), declaringType: typeof(CollectionMultiCardGrouped), propertyName: nameof(FsctSource)); //, propertyChanged: OnItemsSourceChanged);

        public CollectionMultiCardGrouped()
        {
            InitializeComponent();
        }

        public object FsctSource
        {
            get
            {
                return (object)GetValue(FsctSourceProperty);
            }
            set
            {
                SetValue(FsctSourceProperty, value);
            }
        }

        public int NumColumns
        {
            get
            {
                return Common.CardSizes.Current.CardsAcrossColumns;
            }
        }
    }
}