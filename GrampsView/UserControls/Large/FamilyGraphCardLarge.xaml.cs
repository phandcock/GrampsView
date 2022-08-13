namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    public partial class FamilyGraphCardLarge : Grid
    {
        public static readonly BindableProperty FsctShowParentLinkProperty
        = BindableProperty.Create(returnType: typeof(bool), declaringType: typeof(FamilyGraphCardLarge), propertyName: nameof(FsctShowParentLink), defaultValue: true);

        public FamilyGraphCardLarge()
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