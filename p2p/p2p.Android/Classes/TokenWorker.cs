using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Work;

namespace p2p.Droid
{
    public class TokenWorker : Worker
    {
        public TokenWorker(Context context, WorkerParameters workerParameters) : base(context, workerParameters)
        {

        }

        public override Result DoWork()
        {
            var taxReturn = CalculateTaxes();
            //Android.Util.Log.Debug("CalculatorWorker", $"Your Tax Return is: {taxReturn}");
            SaveTextAsync("testWork.txt", DateTime.Now.ToString() +  $", Your Tax Return is: {taxReturn}").Wait();
            return Result.InvokeSuccess();
        }

        public double CalculateTaxes()
        {
            return 2000;
        }

        private async Task SaveTextAsync(string filename, string text)
        {
            //string pathToFile = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, filename);
            string pathToFile = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath, filename);
            using (var fileStream = new FileStream(pathToFile, FileMode.Append, FileAccess.Write, FileShare.None))
            {
               fileStream.Write(Encoding.UTF8.GetBytes(text), 0, Encoding.UTF8.GetByteCount(text));
            }
            

        }

    }
}