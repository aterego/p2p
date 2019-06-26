using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using p2p.Controls;
using Android.Content;
using p2p.Droid;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Content.Res;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace p2p.Droid
{
    class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.White);
                GradientDrawable gd = new GradientDrawable();

                //this line sets the bordercolor
                gd.SetColor(global::Android.Graphics.Color.Red);

                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.Gray));
                Control.SetPadding(Control.PaddingLeft, Control.PaddingTop, Control.PaddingRight, 7);

            }
        }
    }
}