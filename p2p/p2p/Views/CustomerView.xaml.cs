using p2p.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace p2p.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerView : ContentPage
    {
        public CustomerView()
        {
            var vm = App.Container.GetService<CustomerViewModel>();
            this.BindingContext = vm;
            InitializeComponent();
        }
    }
}