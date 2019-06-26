using p2p.Services;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using p2p.Helpers;
using System.Linq;

namespace p2p.ViewModels
{
    public class ProfiRegistrationViewModel : INotifyPropertyChanged
    {
        public Action<string> DisplayInvalidRegistrationPrompt;
        public Action<string> DisplaySuccessRegistrationPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private IBackendProxy _backendProxy;
        private IBackendSessionManager _backendSessionManager;
        private string _regmail = "";
        private int _error = 0;



        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FirstName"));
            }
        }

        private string firstNameEmptyError;
        public string FirstNameEmptyError
        {
            get { return firstNameEmptyError; }
            set
            {
                firstNameEmptyError = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FirstNameEmptyError"));
            }
        }

        private string firstNameIncorrect;
        public string FirstNameIncorrect
        {
            get { return firstNameIncorrect; }
            set
            {
                firstNameIncorrect = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FirstNameIncorrect"));
            }
        }


        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LastName"));
            }
        }


        private string lastNameEmptyError;
        public string LastNameEmptyError
        {
            get { return lastNameEmptyError; }
            set
            {
                lastNameEmptyError = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LastNameEmptyError"));
            }
        }

        private string lastNameIncorrect;
        public string LastNameIncorrect
        {
            get { return lastNameIncorrect; }
            set
            {
                lastNameIncorrect = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LastNameIncorrect"));
            }
        }


        private int selectedGender;
        public int SelectedGender
        {
            get { return selectedGender; }
            set
            {
                selectedGender = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedGender"));
            }
        }

        private string genderEmptyError;
        public string GenderEmptyError
        {
            get { return genderEmptyError; }
            set
            {
                genderEmptyError = value;
                PropertyChanged(this, new PropertyChangedEventArgs("GenderEmptyError"));
            }
        }


        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("BirthDate"));
            }
        }

        private string birthDateEmptyError;
        public string BirthDateEmptyError
        {
            get { return birthDateEmptyError; }
            set
            {
                birthDateEmptyError = value;
                PropertyChanged(this, new PropertyChangedEventArgs("BirthDateEmptyError"));
            }
        }


        private string passportId;
        public string PassportId
        {
            get { return passportId; }
            set
            {
                passportId = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PassportId"));
            }
        }

        private string passportIdEmptyError;
        public string PassportIdEmptyError
        {
            get { return passportIdEmptyError; }
            set
            {
                passportIdEmptyError = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PassportIdEmptyError"));
            }
        }


        private string phone1;
        public string Phone1
        {
            get { return phone1; }
            set
            {
                phone1 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Phone1"));
            }
        }

        private string phone1EmptyError;
        public string Phone1EmptyError
        {
            get { return phone1EmptyError; }
            set
            {
                phone1EmptyError = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Phone1EmptyError"));
            }
        }

 
        private string phone1Incorrect;
        public string Phone1Incorrect
        {
            get { return phone1Incorrect; }
            set
            {
                phone1Incorrect = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Phone1Incorrect"));
            }
        }
   

        private string company;
        public string Company
        {
            get { return company; }
            set
            {
                company = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Company"));
            }
        }

        private string address1;
        public string Address1
        {
            get { return address1; }
            set
            {
                address1 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Address1"));
            }
        }

        private string address1EmptyError;
        public string Address1EmptyError
        {
            get { return address1EmptyError; }
            set
            {
                address1EmptyError = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Address1EmptyError"));
            }
        }


        private string licenceId;
        public string LicenceId
        {
            get { return licenceId; }
            set
            {
                licenceId = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LicenceId"));
            }
        }

        private string licenceIdEmptyError;
        public string LicenceIdEmptyError
        {
            get { return licenceIdEmptyError; }
            set
            {
                licenceIdEmptyError = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LicenceIdEmptyError"));
            }
        }


        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        private string emailEmptyError;
        public string EmailEmptyError
        {
            get { return emailEmptyError; }
            set
            {
                emailEmptyError = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EmailEmptyError"));
            }
        }

        private string emailIncorrect;
        public string EmailIncorrect
        {
            get { return emailIncorrect; }
            set
            {
                emailIncorrect = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EmailIncorrect"));
            }
        }


        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        private string passwordEmptyError;
        public string PasswordEmptyError
        {
            get { return passwordEmptyError; }
            set
            {
                passwordEmptyError = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PasswordEmptyError"));
            }
        }

        private string passwordShortError;
        public string PasswordShortError
        {
            get { return passwordShortError; }
            set
            {
                passwordShortError = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PasswordShortError"));
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ConfirmPassword"));
            }
        }

        private string passwordNotMatch;
        public string PasswordNotMatch
        {
            get { return passwordNotMatch; }
            set
            {
                passwordNotMatch = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PasswordNotMatch"));
            }
        }


        public ICommand SubmitCommand { protected set; get; }
        public ProfiRegistrationViewModel(IBackendProxy backendProxy, IBackendSessionManager backendSessionManager)
        {
             SelectedGender = -1;

            _backendProxy = backendProxy;
            _backendSessionManager = backendSessionManager;
           
            SubmitCommand = new Command(OnSubmit);

            Company = "";
            //Phone2 = "";
        }
        public async void OnSubmit()
        {
            _error = 0;

            if (string.IsNullOrEmpty(FirstName))
            {
                FirstNameEmptyError = "Please enter First Name.";
                _error = 1;
            }

            if (!string.IsNullOrEmpty(FirstName) && !Validators.NameIsValid(FirstName))
            {
                FirstNameIncorrect = "Please enter valid First Name (Latin characters only).";
                _error = 1;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                LastNameEmptyError = "Please enter Last Name.";
                _error = 1;
            }

            if (!string.IsNullOrEmpty(LastName) && !Validators.NameIsValid(LastName))
            {
                LastNameIncorrect = "Please enter valid Last Name (Latin characters only).";
                _error = 1;
            }

            if (SelectedGender == -1)
            {
                GenderEmptyError = "Please select Gender";
                _error = 1;
            }


            var minDate = new DateTime(1900, 1, 1);
            if (BirthDate == minDate)
            {
                BirthDateEmptyError = "Please select Birth Date";
                _error = 1;
            }

            if (string.IsNullOrEmpty(PassportId))
            {
                PassportIdEmptyError = "Please enter Passport ID.";
                _error = 1;
            }

            if (string.IsNullOrEmpty(Phone1))
            {
                Phone1EmptyError = "Please enter Phone Number.";
                _error = 1;
            }
            
            if (!string.IsNullOrEmpty(Phone1) && !Validators.PhoneIsValid(Phone1))
            {
                Phone1Incorrect = "Please enter valid Phone Number.";
                _error = 1;
            }
            

            if (string.IsNullOrEmpty(Address1))
            {
                Address1EmptyError = "Please enter Address.";
                _error = 1;
            }

            if (string.IsNullOrEmpty(LicenceId))
            {
                LicenceIdEmptyError = "Please enter Licence ID.";
                _error = 1;
            }

            if (string.IsNullOrEmpty(Email))
            {
                EmailEmptyError = "Please enter Email.";
                _error = 1;
            }

            if (!string.IsNullOrEmpty(Email) && !Validators.EmailIsValid(Email))
            {
                EmailIncorrect = "Please enter valid Email.";
                _error = 1;
            }

            if (string.IsNullOrEmpty(Password))
            {
                PasswordEmptyError = "Please enter Password.";
                _error = 1;
            }

            if (!string.IsNullOrEmpty(Password) && Password.Length < 6)
            {
                PasswordShortError = "Password is too short - minimum length is 6 characters.";
                _error = 1;
                return;
            }

            if (!string.IsNullOrEmpty(Password) && Password != ConfirmPassword)
            {
                PasswordNotMatch = "Password and Confirm Password do not match";
                _error = 1;
            }

            if (_error == 1)
            {
                return;
            }
            else
            {

                //***AVA*** Capitalize first letter
                FirstName = FirstName.First().ToString().ToUpper() + FirstName.Substring(1);
                LastName = LastName.First().ToString().ToUpper() + LastName.Substring(1);

                FirstName = FirstName.Replace("'", "&#39;");
                LastName = LastName.Replace("'", "&#39;");
                if (SelectedGender == 0)
                    SelectedGender = 1;
                else if (SelectedGender == 1)
                    SelectedGender = 0;

                string[] parms = { FirstName, LastName, SelectedGender.ToString(), BirthDate.ToString("yyyy-MM-ddT00:00:00"),
                                    PassportId, Phone1,"",Company, Address1,"", LicenceId, "", Password, Email};
                var response = await _backendProxy.RegisterProfiAsync(parms);
                if (response.Success)
                {
                    DisplaySuccessRegistrationPrompt(response.Message);
                }
                else
                {
                    DisplayInvalidRegistrationPrompt(response.Message);

                }
            }





        }
    }
}
