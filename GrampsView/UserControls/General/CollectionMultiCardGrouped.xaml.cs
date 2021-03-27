namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;

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
            get; set;
        } = 3;

        private void CollectionMultiCardGroupedRoot_SizeChanged(object sender, EventArgs e)
        {
            Contract.Requires(sender != null);

            CollectionMultiCardGrouped t = sender as CollectionMultiCardGrouped;

            NumColumns = (Int32)(t.Width / CardSizes.Current.CardSmallWidth + 1);  // +1 for padding
        }
    }
}