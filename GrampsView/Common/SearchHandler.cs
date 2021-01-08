namespace GrampsView.Common
{
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Newtonsoft.Json;

    using Xamarin.Forms;

    public class GrampsViewSearchHandler : SearchHandler
    {
        public GrampsViewSearchHandler()
        {
            this.ItemTemplate = new DataTemplate(() =>
           {
               Grid grid = new Grid { Padding = 10 };
               grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.15, GridUnitType.Star) });
               grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.85, GridUnitType.Star) });

               Label nameLabel = new Label { FontAttributes = FontAttributes.Bold };
               nameLabel.SetBinding(Label.TextProperty, "HLinkKey");

               grid.Children.Add(nameLabel);
               return grid;
           });
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            //HLinkPersonModel t = (item as HLinkPersonModel);

            //t.UCNavigate(t);

            // Note: strings will be URL encoded for navigation (e.g. "Blue Monkey" becomes
            // "Blue%20Monkey"). Therefore, decode at the receiver.
            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync($"PersonDetailPage?BaseParamsHLink=" + JsonConvert.SerializeObject(item));
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = DV.PersonDV.Search(newValue).ToObservableCollection<HLinkPersonModel>();
            }
        }
    }
}