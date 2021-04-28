namespace GrampsView.Droid
{
    using Android;
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;

    using Android.Util;

    using AndroidX.AppCompat.App;
    using AndroidX.Core.App;
    using AndroidX.Core.Content;

    using System.Threading.Tasks;

    [Activity(
                Theme = "@style/SplashTheme.Splash",
                MainLauncher = true,
                Label = "GrampsView",
                Icon = "@mipmap/icon",
                RoundIcon = "@mipmap/ic_launcher_round",
                ConfigurationChanges = /*ConfigChanges.ScreenSize |*/
                                        ConfigChanges.UiMode |
                                        //ConfigChanges.Orientation |
                                        ConfigChanges.ScreenLayout |
                                        ConfigChanges.SmallestScreenSize,
                ScreenOrientation = ScreenOrientation.User,
                NoHistory = true,
                ResizeableActivity = true
            )]
    public class SplashActivity : AppCompatActivity
    {
        private static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed()
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Log.Debug(TAG, "SplashActivity.OnCreate");

            // Get read/write permisions
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.WriteExternalStorage }, 0);
            }

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage }, 0);
            }
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