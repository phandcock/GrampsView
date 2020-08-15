namespace GrampsView.Droid
{
    using System.Threading.Tasks;

    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Util;

    [Activity(Theme = "@style/SplashTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity

    {
        private static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed()
        {
        }

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)

        {
            base.OnCreate(savedInstanceState, persistentState);

            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task

        protected override void OnResume()

        {
            base.OnResume();

            Task startupWork = new Task(() => { SimulateStartup(); });

            startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        private async void SimulateStartup()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");

            await Task.Delay(1).ConfigureAwait(false); // Simulate a bit of startup work.

            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}