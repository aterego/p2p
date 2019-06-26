using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using p2p.Controls;
using Android.Content;
using p2p.Droid;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Content.Res;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace p2p.Droid
{
    class CustomEditorRenderer : EditorRenderer
    {
        public CustomEditorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.White);
                GradientDrawable gd = new GradientDrawable();

                //this line sets the bordercolor
                gd.SetColor(global::Android.Graphics.Color.Red);

                //this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.Gray));
                Control.SetPadding(Control.PaddingLeft, 3, Control.PaddingRight, Control.PaddingBottom);

            }
        }
    }
}