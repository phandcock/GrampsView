namespace GrampsView.Common.CustomClasses
{
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

            BackgroundColor = Color.Transparent;
        }

        public bool DismissFlag
        {
            get
            {
                return (bool)GetValue(DismissFlagProperty);
            }
            set
            {
                SetValue(DismissFlagProperty, value);
            }
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