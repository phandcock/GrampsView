namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    public partial class FamilyGraphCardMediumLink : LinkMediumCardControlTemplate
    {
        public static readonly BindableProperty FsctShowParentLinkProperty
        = BindableProperty.Create(returnType: typeof(bool), declaringType: typeof(FamilyGraphCardMediumLink), propertyName: nameof(FsctShowParentLink), defaultValue: true);

        public FamilyGraphCardMediumLink()
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