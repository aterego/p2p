using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using p2p.Droid;
using p2p.Models;
using p2p.Services;
using p2p.Views;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace p2p.Droid
{
    public class LoginPageRenderer : PageRenderer
    {
        Context ctx;
        //public static string sessionToken;
        //public static string username;
        public static IBackendSessionManager backendSessionManager;
        public LoginPageRenderer(Context context) : base(context)
        {
            
            this.ctx = context;
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            var loginPage = (LoginPage)e.NewElement;
            if (loginPage != null)
            {
                loginPage.OnStartRefresh += StartRefresh;
            }
        }

        private void StartRefresh(object sender, SessionEventArgs ea)
        {
            try
            {
                var args = new Bundle();
                //sessionToken = ea.SessionToken;
                //username = ea.Username;
                backendSessionManager = ea.BackendSessionManager;
                Intent serviceStart = new Intent(ctx, typeof(RefreshSession));
                ctx.StartService(serviceStart);


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
            }
        }
    }


    [Service]
    public class RefreshSession : Service
    {
        
        static readonly string TAG = "X:";
        static readonly int TimerWait = 25000;
        Timer timer;
        DateTime startTime;
        bool isStarted = false;


        public override void OnCreate()
        {
            //Console.WriteLine(LoginPageRenderer.sessionToken);
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
                //SaveTextAsync("testWork0.txt", TAG + $"This service was already started, it's been running for {runtime:c}.\n").Wait();
                Console.WriteLine(TAG + $"This service was already started, it's been running for {runtime:c}.");
            }
            else
            {
                RegisterForegroundService();
                startTime = DateTime.UtcNow;
                //SaveTextAsync("testWork0.txt", $"Starting the service, at {startTime}.\n").Wait();
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

            var notificationBuilder = new NotificationCompat.Builder(this, "M_CH_ID");

            notificationBuilder.SetAutoCancel(true)
                    .SetContentTitle("Guda")
                    .SetContentText("Audio")
                    //.SetSmallIcon(Resource.Drawable.ic_stat_name)
                    //.SetContentIntent(BuildIntentToShowMainActivity())
                    .SetOngoing(true);
            //.AddAction(BuildRestartTimerAction())
            //.AddAction(BuildStopServiceAction())



            /*
            var notification = new Notification.Builder(this)
                .SetContentTitle("Guda")
                .SetContentText("Audio")
                //.SetSmallIcon(Resource.Drawable.ic_stat_name)
                //.SetContentIntent(BuildIntentToShowMainActivity())
                .SetOngoing(true)
                //.AddAction(BuildRestartTimerAction())
                //.AddAction(BuildStopServiceAction())
                .Build();
            */


            // Enlist this instance of the service as a foreground service
            //StartForeground(1,notification);
            StartForeground(1, notificationBuilder.Build());
        }

        async void HandleTimerCallback(object state)
        {
            TimeSpan runTime = DateTime.UtcNow.Subtract(startTime);
            //await SaveTextAsync("testWork0.txt", $"This service has been running for {runTime:c} (since ${state}).\n");
            Console.WriteLine(TAG + $"This service has been running for {runTime:c} (since ${state}).");
            await LoginPageRenderer.backendSessionManager.Refresh();
            //var backend = App.Container.GetService<IBackendSessionManager>();
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