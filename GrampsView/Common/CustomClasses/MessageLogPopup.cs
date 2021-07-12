namespace GrampsView.Common.CustomClasses
{
    using GrampsView.Data.Repository;

    using Xamarin.CommunityToolkit.UI.Views;
    using Xamarin.Forms;

    public class MessageLogPopup : Popup
    {
        public static readonly BindableProperty DismissFlagProperty
       = BindableProperty.Create(returnType: typeof(bool), declaringType: typeof(MessageLogPopup), defaultValue: false, propertyName: nameof(DismissFlag), propertyChanged: OnDismissFlagChanged);

        public MessageLogPopup()
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand;

            IsLightDismissEnabled = false;

            VerticalOptions = LayoutOptions.CenterAndExpand;

            Size = new Size(DataStore.Instance.AD.ScreenSize.Width - 100, DataStore.Instance.AD.ScreenSize.Height - 100);
        }

        public bool DismissFlag
        {
            get => (bool)GetValue(DismissFlagProperty);
            set => SetValue(DismissFlagProperty, value);
        }

        private static void OnDismissFlagChanged(BindableObject bindable, object oldValue, object newValue)
        {
            MessageLogPopup sender = bindable as MessageLogPopup;

            bool t = (bool)newValue;

            if (t)
            {
                sender.Dismiss(null);
            }
        }
    }
}