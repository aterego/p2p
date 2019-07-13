using p2p.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace p2p.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /* ***AVA*** working version
    public partial class CustomerSettingsTab : ContentPage
    {

        public CustomerSettingsTab()
        {
            InitializeComponent();

            List<string> items = new List<string>();
            items.Add("Test1");
            items.Add("Test2");

            WrappedItems = items.Select(item => new WrappedSelection()
            {
                Item = item,
                IsSelected = false
            }).ToList();

            ListView mainList = new ListView()
            {
                ItemsSource = WrappedItems,
                ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate)),
            };
            mainList.ItemSelected += (sender, e) =>
            {
                //Supresses highlighting of ListItems
                if (e.SelectedItem == null) return;
                var o = (WrappedSelection)e.SelectedItem;
                o.IsSelected = !o.IsSelected;
                ((ListView)sender).SelectedItem = null;
            };
            Content = mainList;
        }

        

        public class WrappedSelection : INotifyPropertyChanged
        {
            public String Item { get; set; }
            bool isSelected = false;
            public bool IsSelected
            {
                get
                {
                    return isSelected;
                }
                set
                {
                    if (isSelected != value)
                    {
                        isSelected = value;
                        PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
                    }
                }
            }
            public event PropertyChangedEventHandler PropertyChanged = delegate { };
        }

        public class WrappedItemSelectionTemplate : ViewCell
        {
            public WrappedItemSelectionTemplate() : base()
            {
                Label name = new Label();
                name.SetBinding(
                    Label.TextProperty,
                    new Binding("Item")
                );
                Switch mainSwitch = new Switch();
                mainSwitch.SetBinding(
                    Switch.IsToggledProperty,
                    new Binding("IsSelected")
                );
                RelativeLayout layout = new RelativeLayout();
                layout.Children.Add(
                    name,
                    Constraint.Constant(5),
                    Constraint.Constant(5),
                    Constraint.RelativeToParent(p => p.Width - 60),
                    Constraint.RelativeToParent(p => p.Height - 10)
                );
                layout.Children.Add(
                    mainSwitch,
                    Constraint.RelativeToParent(p => p.Width - 55),
                    Constraint.Constant(5),
                    Constraint.Constant(50),
                    Constraint.RelativeToParent(p => p.Height - 10)
                );
                View = layout;
            }
        }

        public List<WrappedSelection> WrappedItems = new List<WrappedSelection>();
       
        public void DeselectAll()
        {
            foreach (var wi in WrappedItems)
            {
                wi.IsSelected = false;
            }
        }
        public List<String> GetSelection()
        {
            return WrappedItems.Where(item => item.IsSelected).Select(wrappedItem => wrappedItem.Item).ToList();
        }
    */

    public partial class CustomerSettingsTab : ContentPage
    {

        public CustomerSettingsTab()
        {
            var vm = App.Container.GetService<CustomerSettingsViewModel>();
            this.BindingContext = vm; //new CustomerSettingsViewModel();
            vm.DisplaySelected += (string[] arg) => DisplayAlert("Error", string.Join(", ", arg), "OK");
            InitializeComponent();

        }
    }
}


