namespace GrampsView.Common
{
    using Xamarin.Forms;

    public class GrampsViewSearchHandler : SearchHandler
    {
        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            // Note: strings will be URL encoded for navigation (e.g. "Blue Monkey" becomes "Blue%20Monkey"). Therefore, decode at the receiver.
            //await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync($"monkeydetails?name={((Animal)item).Name}");
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
                //ItemsSource = MonkeyData.Monkeys
                //    .Where(monkey => monkey.Name.ToLower().Contains(newValue.ToLower()))
                //    .ToList<Animal>();
            }
        }
    }
}