using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using AndroidX.Work;
using Xamarin.Forms;

namespace p2p.Droid
{
    public class TokenWorker : Worker
    {

        Context _context;
        


        public TokenWorker(Context context, WorkerParameters workerParameters) : base(context, workerParameters)
        {
            _context = context;

        }

        public override Result DoWork()
        {
            CheckAppPermissions().Wait();

            var taxReturn = CalculateTaxes();
            //Android.Util.Log.Debug("CalculatorWorker", $"Your Tax Return is: {taxReturn}");
            //SaveTextAsync("testWork.txt", DateTime.Now.ToString() +  $", Your Tax Return is: {taxReturn}\n").Wait();
            return Result.InvokeSuccess();
        }

        public double CalculateTaxes()
        {
            return 2000;
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

        private async Task CheckAppPermissions()
        {
            if ((int)Build.VERSION.SdkInt < 23)
            {
                return;
            }
            else
            {
                if (ContextCompat.CheckSelfPermission(_context, Manifest.Permission.ReadExternalStorage) != Permission.Granted
                    && ContextCompat.CheckSelfPermission(_context, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
                {
                    var permissions = new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage };
                    ActivityCompat.RequestPermissions((Activity)Forms.Context, permissions, 1);
                }
            }
        }

    }
}