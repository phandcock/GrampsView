namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System.ComponentModel;

    using Xamarin.Forms;

    public partial class CollectionMultiCardGrouped : Frame, INotifyPropertyChanged
    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(CardGroup), declaringType: typeof(CollectionMultiCardGrouped), propertyName: nameof(FsctSource)); //, propertyChanged: OnItemsSourceChanged);

        public CollectionMultiCardGrouped()
        {
            InitializeComponent();
        }

        public CardGroup FsctSource
        {
            get
            {
                return (CardGroup)GetValue(FsctSourceProperty);
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