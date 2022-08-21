namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    public partial class ParentLinkCardLarge : Grid
    {
        public static readonly BindableProperty FsctShowParentLinkProperty
        = BindableProperty.Create(returnType: typeof(bool), declaringType: typeof(ParentLinkCardLarge), propertyName: nameof(FsctShowParentLink), defaultValue: true);

        public ParentLinkCardLarge()
        {
            InitializeComponent();
        }

        public bool FsctShowParentLink
        {
            get => (bool)GetValue(FsctShowParentLinkProperty);
            set => SetValue(FsctShowParentLinkProperty, value);
        }
    }
}