// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common.CustomClasses;

namespace GrampsView.Views;

public partial class BaseNavigation : FlyoutPage
{
    public BaseNavigation()
    {
        InitializeComponent();

        flyoutPage.collectionView.SelectionChanged += OnSelectionChanged;


    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        FlyoutPageItem? item = e.CurrentSelection.FirstOrDefault() as FlyoutPageItem;
        if (item != null)
        {
            Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
            IsPresented = false;
        }
    }
}