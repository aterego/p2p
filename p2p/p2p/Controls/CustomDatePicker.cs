using System.ComponentModel;
using Xamarin.Forms;

namespace p2p.Controls
{
    public class CustomDatePicker : DatePicker, INotifyPropertyChanged
    {
        public static readonly BindableProperty EnterTextProperty = BindableProperty.Create(propertyName: "Placeholder", returnType: typeof(string), declaringType: typeof(CustomDatePicker), defaultValue: default(string));


        public string Placeholder
        {
            get;
            set;
        }


    }
}
