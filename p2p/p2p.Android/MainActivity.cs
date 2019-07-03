using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Ninject;
using AndroidX.Work;
using Android.Content;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Threading;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android;

namespace p2p.Droid
{
    [Activity(Label = "p2p", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {


        protected override void OnCreate(Bundle savedInstanceState)
        {
            var settings = new Ninject.NinjectSettings() { LoadExtensions = false };
            var kernel = new Ninject.StandardKernel(settings, new BaseContractModule());
            App.Container = kernel;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            /*
            PeriodicWorkRequest taxWorkRequest = PeriodicWorkRequest.Builder.From<TokenWorker>(TimeSpan.FromMinutes(20)).Build();
            WorkManager.Instance.Enqueue(taxWorkRequest);
            */

            Intent serviceStart = new Intent(this, typeof(MyMediaPlayer));
            StartService(serviceStart);


        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private async Task CheckAppPermissions()
        {
            if ((int)Build.VERSION.SdkInt < 23)
            {
                return;
            }
            else
            {
                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != Permission.Granted
                    && ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
                {
                    var permissions = new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage };
                    ActivityCompat.RequestPermissions(this, permissions, 1);
                }
            }
        }

    }

    [Service]
    public class MyMediaPlayer : Service
    {

        static readonly string TAG = "X:";
        static readonly int TimerWait = 500000;
        Timer timer;
        DateTime startTime;
        bool isStarted = false;


        public override void OnCreate()
        {
            base.OnCreate();
        }

        //this will not be called, however it is require to override
        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {


            base.OnStartCommand(intent, flags, startId);

            if (isStarted)
            {
                TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
                SaveTextAsync("testWork.txt", TAG + $"This service was already started, it's been running for {runtime:c}.").Wait();
                Console.WriteLine(TAG + $"This service was already started, it's been running for {runtime:c}.");
            }
            else
            {
                RegisterForegroundService();
                startTime = DateTime.UtcNow;
                SaveTextAsync("testWork.txt", $"Starting the service, at {startTime}.").Wait();
                timer = new Timer(HandleTimerCallback, startTime, 0, TimerWait);
                isStarted = true;

            }



            return StartCommandResult.Sticky;
        }



        public override void OnDestroy()
        {
            // Remove the notification from the status bar.
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Cancel(1);

            timer.Dispose();
            timer = null;
            isStarted = false;

            TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
            Console.WriteLine(TAG + $"Simple Service destroyed at {DateTime.UtcNow} after running for {runtime:c}.");

            base.OnDestroy();
        }


        void RegisterForegroundService()
        {

           var notificationBuilder =   new NotificationCompat.Builder(this, "M_CH_ID");

            notificationBuilder.SetAutoCancel(true)
                    .SetContentTitle("Guda")
                    .SetContentText("Audio")
                    //.SetSmallIcon(Resource.Drawable.ic_stat_name)
                    //.SetContentIntent(BuildIntentToShowMainActivity())
                    .SetOngoing(true);
                    //.AddAction(BuildRestartTimerAction())
                    //.AddAction(BuildStopServiceAction())
                   



            var notification = new Notification.Builder(this)
                .SetContentTitle("Guda")
                .SetContentText("Audio")
                //.SetSmallIcon(Resource.Drawable.ic_stat_name)
                //.SetContentIntent(BuildIntentToShowMainActivity())
                .SetOngoing(true)
                //.AddAction(BuildRestartTimerAction())
                //.AddAction(BuildStopServiceAction())
                .Build();


            // Enlist this instance of the service as a foreground service
            //StartForeground(1,notification);
            StartForeground(1, notificationBuilder.Build());
        }

        async void HandleTimerCallback(object state)
        {
            TimeSpan runTime = DateTime.UtcNow.Subtract(startTime);
            await SaveTextAsync("testWork.txt", $"This service has been running for {runTime:c} (since ${state}).");
            Console.WriteLine(TAG + $"This service has been running for {runTime:c} (since ${state}).");
            //var backend = App.Cnt.Get<IBackendSessionManager>();
            //backend.RefreshSession().Wait();

        }


        private async Task SaveTextAsync(string filename, string text)
        {
            string pathToFile = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, filename);
            //string pathToFile = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath, filename);
            using (var fileStream = new FileStream(pathToFile, FileMode.Append, FileAccess.Write, FileShare.None))
            {
                fileStream.Write(Encoding.UTF8.GetBytes(text), 0, Encoding.UTF8.GetByteCount(text));
            }


        }



    }
}