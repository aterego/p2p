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
        public StandardKernel Kernel { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            var settings = new Ninject.NinjectSettings() { LoadExtensions = false };
            var kernel = new Ninject.StandardKernel(settings, new BaseContractModule());
            App.Container = kernel;

            Kernel = kernel;

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
            var per = CheckAppPermissions().Result;
            /*
           var per = CheckAppPermissions().Result;
            if (per)
            {
                Intent serviceStart = new Intent(this, typeof(MyMediaPlayer));
                StartService(serviceStart);
            }
            */


        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private Task<bool> CheckAppPermissions()
        {
            return Task<bool>.Run(()=>
            { 
                if ((int)Build.VERSION.SdkInt < 23)
                {
                    return true;
                }
                else
                {
                    if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != Permission.Granted
                        && ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
                    {
                        var permissions = new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage };
                        ActivityCompat.RequestPermissions(this, permissions, 1);
                        return true;
                    }
                    else
                        return false;
                }
            });
        }

    }


}