using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using p2p.Controls;
using p2p.iOS;
using UIKit;
using System;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace p2p.iOS
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null)
                return;
            var element = e.NewElement as CustomDatePicker;
            if (!string.IsNullOrWhiteSpace(element.Placeholder))
            {
                Control.Text = element.Placeholder;
            }
            Control.BorderStyle = UITextBorderStyle.RoundedRect;
            Control.Layer.BorderColor = UIColor.FromRGB(83, 63, 149).CGColor;   
            Control.Layer.CornerRadius = 10;
            Control.Layer.BorderWidth = 1f;
            Control.AdjustsFontSizeToFitWidth = true;
            Control.TextColor = UIColor.FromRGB(83, 63, 149);   

           Control.ShouldEndEditing += (textField) => {
               var seletedDate = (UITextField)textField;
               var text = seletedDate.Text;
               if (text == element.Placeholder)
               {
                   Control.Text = DateTime.Now.ToString("dd/MM/yyyy");
               }
               return true;
           };
        }

        private void OnCanceled(object sender, EventArgs e)
        {
            Control.ResignFirstResponder();
        }

        private void OnDone(object sender, EventArgs e)
        {
            Control.ResignFirstResponder();
        }
    }
}