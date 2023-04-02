// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class FamilyGraphCardMediumLink : LinkMediumCardControlTemplate
    {
        public static readonly BindableProperty FsctShowParentLinkProperty
        = BindableProperty.Create(returnType: typeof(bool), declaringType: typeof(FamilyGraphCardMediumLink), propertyName: nameof(FsctShowParentLink), defaultValue: true);

        public FamilyGraphCardMediumLink()
        {
            InitializeComponent();

            FamilyGraphCardMediumLink t = this;
        }

        public bool FsctShowParentLink
        {
            get => (bool)GetValue(FsctShowParentLinkProperty);
            set => SetValue(FsctShowParentLinkProperty, value);
        }
    }
}