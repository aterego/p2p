using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Text;
using p2p.Controls;
using p2p.Droid;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace p2p.Droid
{
    class CustomDatePickerRenderer : DatePickerRenderer
    {
        public CustomDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.White);
                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.Blue));
                Control.SetPadding(Control.PaddingLeft, Control.PaddingTop, Control.PaddingRight, 7);
                

                CustomDatePicker element = Element as CustomDatePicker;

                if (!string.IsNullOrWhiteSpace(element.Placeholder))
                {
                    Control.SetTextColor(global::Android.Graphics.Color.Gray);
                    Control.Text = element.Placeholder;
                }
                this.Control.TextChanged += (sender, arg) => {
                    var selectedDate = arg.Text.ToString();

                    if (selectedDate == element.Placeholder)
                    {
                        Control.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }

                    this.Control.SetTextColor(this.Element.TextColor.ToAndroid(Xamarin.Forms.Color.FromHex("#000000")));
                };
            }
        }
    }
}