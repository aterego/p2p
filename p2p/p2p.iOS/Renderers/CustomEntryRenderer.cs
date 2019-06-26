using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using p2p.Controls;
using p2p.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace p2p.iOS
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // do whatever you want to the UITextField here!
                Control.BackgroundColor = UIColor.FromRGB(204, 153, 255);
                Control.BorderStyle = UITextBorderStyle.Line;
                
                Control.Layer.CornerRadius = 10;
                Control.TextColor = UIColor.Black;

            }
        }
    }
}