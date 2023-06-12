// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class PersonLinkCardLarge : LinkLargeCardControlTemplate
    {
        public static readonly BindableProperty FsctShowParentLinkProperty
        = BindableProperty.Create(returnType: typeof(bool), declaringType: typeof(PersonLinkCardLarge), propertyName: nameof(FsctShowParentLink), defaultValue: true);

        public PersonLinkCardLarge()
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