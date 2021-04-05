namespace GrampsView.Common
{
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Newtonsoft.Json;

    using System.Linq;
    using System.Threading.Tasks;

    using Xamarin.Forms;

    public class GrampsViewSearchHandler : SearchHandler
    {
        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            await Task.Delay(1000);

            ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;

            await CommonRoutines.NavigateAsync("PersonDetailPage?BaseParamsHLink=" + JsonConvert.SerializeObject(item));
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
                ItemsSource = DV.PersonDV.SearchShell(newValue).ToList<HLinkPersonModel>();
            }
        }
    }
}