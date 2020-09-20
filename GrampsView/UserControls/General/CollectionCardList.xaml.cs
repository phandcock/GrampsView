namespace GrampsView.UserControls
{
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;

    using Xamarin.Forms;

    public partial class CollectionCardList : Frame, INotifyPropertyChanged
    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(IEnumerable), declaringType: typeof(CollectionCardList), propertyName: nameof(FsctSource), propertyChanged: OnItemsSourceChanged);

        public CollectionCardList()
        {
            InitializeComponent();
        }

        //public event PropertyChangedEventHandler MyPropertyChanged;

        public IEnumerable FsctSource
        {
            get { return (IEnumerable)GetValue(FsctSourceProperty); }
            set { SetValue(FsctSourceProperty, value); }
        }

        public static void OnItemsSourceChanged(BindableObject argSource, object oldValue, object newValue)
        {
            // Xamarin sets to null as the parent page is destroyed
            if (newValue is null)
            {
                return;
            }

            var layout = argSource as CollectionCardList;

            Contract.Assert(layout != null);

            IEnumerable newVal = newValue as IEnumerable;

            Contract.Assert(newVal != null, "CollectionCardList source should not be null");

            layout.theCollectionView.ItemsSource = newVal;
        }
    }
}