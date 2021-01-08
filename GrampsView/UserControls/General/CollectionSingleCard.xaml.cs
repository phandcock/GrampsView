namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;
    using System.Windows.Input;

    using Xamarin.Forms;

    public partial class CollectionSingleCard : Frame, INotifyPropertyChanged
    {
        public static readonly BindableProperty FsctSourceProperty
              = BindableProperty.Create(returnType: typeof(IEnumerable), declaringType: typeof(CollectionSingleCard), propertyName: nameof(FsctSource), propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty FsctTemplateProperty
                    = BindableProperty.Create(nameof(FsctTemplate), returnType: typeof(DataTemplate), declaringType: typeof(CollectionSingleCard), propertyChanged: OnItemTemplateChanged);

        private int _NumColumns = 10;

        private int _NumItems = 10;

        private int _ucHeight = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionSingleCard"/> class.
        /// </summary>
        public CollectionSingleCard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when we want to trigger a property changed event.
        /// </summary>
        public event PropertyChangedEventHandler MyPropertyChanged;

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

        public ICommand LoadMoreDataCommand => new Command(GetNextPageOfData);

        public int NumColumns
        {
            get
            {
                return _NumColumns;
            }

            set
            {
                if ((_NumColumns != value) && (value > 0) && (value < 20))
                {
                    _NumColumns = value;
                    OnPropertyChanged();
                }
                else
                {
                }
            }
        }

        public int NumItems
        {
            get
            {
                return _NumItems;
            }

            set
            {
                _NumItems = value;
                OnPropertyChanged();
            }
        }

        public int ucHeight
        {
            get
            {
                return _ucHeight;
            }

            set
            {
                _ucHeight = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Called when [items source changed].
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

            layout.theCollectionView.ItemsSource = iSource;

            int counter = 0;

            foreach (var item in iSource)
            {
                counter++;
            }

            layout.NumItems = counter;

            layout.SetUcHeight();
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

            layout.theCollectionView.ItemTemplate = iTemplate;
        }

        public void SetUcHeight()
        {
            int t = (Convert.ToInt32(NumItems / NumColumns) + 1);
            int ucHeight = Convert.ToInt32(t * CardSizes.Current.CardSmallHeight);

            if (ucHeight < 1)
            {
                ucHeight = 1;
            }

            ucHeight = ucHeight + 50;

            this.HeightRequest = ucHeight;
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

        //private void CollectionSingleCardRoot_SizeChanged(object sender, EventArgs e)
        //{
        //    Contract.Requires(sender != null);

        // CollectionSingleCard t = sender as CollectionSingleCard;

        // int tt = (Int32)(t.Width / CardSizes.Current.CardSmallWidth);

        // if (tt < 1) { tt = 1; }

        //    // _NumColumns = tt;
        //}

        private void GetNextPageOfData()
        {
        }

        private void theCollectionView_SizeChanged(object sender, EventArgs e)
        {
            Contract.Requires(sender != null);

            CollectionView t = sender as CollectionView;

            int tt = (Int32)(t.Width / CardSizes.Current.CardSmallWidth);

            if (tt < 1)
            {
                tt = 1;
            }

            NumColumns = tt;
        }
    }
}