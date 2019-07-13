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
    public partial class CustomerTabbedPage : TabbedPage
    {
        public CustomerTabbedPage()
        {
            Title = "F";
            InitializeComponent();
        }
    }
}