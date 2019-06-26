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
    public partial class ProfiView : ContentPage
    {
        public ProfiView()
        {
            var vm = App.Container.GetService<ProfiViewModel>();
            this.BindingContext = vm;
            InitializeComponent();
        }
    }
}