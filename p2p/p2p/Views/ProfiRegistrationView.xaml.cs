using p2p.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace p2p.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfiRegistrationView : ContentPage
    {
        public ProfiRegistrationView()
        {
            Title = "P2P - Customer Registration";
            var vm = App.Container.GetService<ProfiRegistrationViewModel>();
            //var vm = new LoginViewModel();
            vm.DisplayInvalidRegistrationPrompt += (string message) => DisplayAlert("Error", message, "OK");
            vm.DisplaySuccessRegistrationPrompt += async (string message) =>
            {
              var answer = await DisplayAlert("Congratulations!", message, "OK", "Cancel");
              if (answer)
              {
                    await Navigation.PopToRootAsync();
              }
            };
            this.BindingContext = vm;
            InitializeComponent();

            vm.PropertyChanged += ViewModel_PropertyChanged;

        }


        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "FirstName":
                    FirstNameEmptyError.Text = "";
                    FirstNameIncorrect.Text = "";
                    FirstNameEmptyError.IsVisible = false;
                    FirstNameIncorrect.IsVisible = false;
                    break;
                case "FirstNameEmptyError":
                    FirstNameEmptyError.IsVisible = true;
                    break;
                case "FirstNameIncorrect":
                    FirstNameIncorrect.IsVisible = true;
                    break;
                case "LastName":
                    LastNameEmptyError.Text = "";
                    LastNameIncorrect.Text = "";
                    LastNameEmptyError.IsVisible = false;
                    LastNameIncorrect.IsVisible = false;
                    break;
                case "LastNameEmptyError":
                    LastNameEmptyError.IsVisible = true;
                    break;
                case "LastNameIncorrect":
                    LastNameIncorrect.IsVisible = true;
                    break;
                case "SelectedGender":
                    GenderEmptyError.Text = "";
                    GenderEmptyError.IsVisible = false;
                    break;
                case "GenderEmptyError":
                    GenderEmptyError.IsVisible = true;
                    break;
                case "BirthDate":
                    BirthDateEmptyError.Text = "";
                    BirthDateEmptyError.IsVisible = false;
                    break;
                case "BirthDateEmptyError":
                    BirthDateEmptyError.IsVisible = true;
                    break;
                case "PassportId":
                    PassportIdEmptyError.Text = "";
                    PassportIdEmptyError.IsVisible = false;
                    break;
                case "PassportIdEmptyError":
                    PassportIdEmptyError.IsVisible = true;
                    break;
                case "Phone1":
                    Phone1EmptyError.Text = "";
                    Phone1Incorrect.Text = "";
                    Phone1EmptyError.IsVisible = false;
                    Phone1Incorrect.IsVisible = false;
                    break;
                case "Phone1EmptyError":
                    Phone1EmptyError.IsVisible = true;
                    break;
                case "Phone1Incorrect":
                    Phone1Incorrect.IsVisible = true;
                    break;
                case "Address1":
                    Address1EmptyError.Text = "";
                    Address1EmptyError.IsVisible = false;
                    break;
                case "Address1EmptyError":
                    Address1EmptyError.IsVisible = true;
                    break;
                case "LicenceId":
                    LicenceIdEmptyError.Text = "";
                    LicenceIdEmptyError.IsVisible = false;
                    break;
                case "LicenceIdEmptyError":
                    LicenceIdEmptyError.IsVisible = true;
                    break;
                case "Email":
                    EmailEmptyError.Text = "";
                    EmailIncorrect.Text = "";
                    EmailEmptyError.IsVisible = false;
                    EmailIncorrect.IsVisible = false;
                    break;
                case "EmailEmptyError":
                    EmailEmptyError.IsVisible = true;
                    break;
                case "EmailIncorrect":
                    EmailIncorrect.IsVisible = true;
                    break;
                case "Password":
                    PasswordEmptyError.Text = "";
                    PasswordShortError.Text = "";
                    PasswordEmptyError.IsVisible = false;
                    PasswordShortError.IsVisible = false;
                    break;
                case "PasswordEmptyError":
                    PasswordEmptyError.IsVisible = true;
                    break;
                case "PasswordShortError":
                    PasswordShortError.IsVisible = true;
                    break;
                case "ConfirmPassword":
                    PasswordNotMatch.Text = "";
                    PasswordNotMatch.IsVisible = false;
                    break;
                case "PasswordNotMatch":
                    PasswordNotMatch.IsVisible = true;
                    break;

            }
        }
    }
}