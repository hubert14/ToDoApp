using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using ToDoApp.Activities.Interfaces;
using ToDoApp.Models.Authorize;
using ToDoApp.Presenters.Authorize;

namespace ToDoApp.Activities.Authorize
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class RegisterActivity : AppCompatActivity, IRegisterView
    {
        private RegisterPresenter _presenter;
        private EditText _email;
        private EditText _password;
        private EditText _confirmPassword;
        private EditText _firstName;
        private EditText _lastName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);

            Initialize();
        }

        private void Initialize()
        {
            _presenter = new RegisterPresenter(this);

            InitFields();
            InitButtons();
        }

        private void InitFields()
        {
            _email = FindViewById<EditText>(Resource.Id.register_email);

            _password = FindViewById<EditText>(Resource.Id.register_password);
            _confirmPassword = FindViewById<EditText>(Resource.Id.register_confirm_password);

            _firstName = FindViewById<EditText>(Resource.Id.register_first_name);
            _lastName = FindViewById<EditText>(Resource.Id.register_last_name);
        }

        private void InitButtons()
        {
            var registerButton = FindViewById<Button>(Resource.Id.register_register_button);
            var goToLoginButton = FindViewById<Button>(Resource.Id.register_backToLogin_button);

            registerButton.Click += (s,e) => 
            {
                _presenter.SendRegister();
                HideKeyboard();
            };

            goToLoginButton.Click += (s, e) => { Finish(); };
        }

        private void HideKeyboard()
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
            imm.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
        }

        public RegisterRequestModel GetData()
        {
            return new RegisterRequestModel
            {
                Email = _email.Text,

                Password = _password.Text,
                ConfirmPassword = _confirmPassword.Text,

                FirstName = _firstName.Text,
                LastName = _lastName.Text
            };
        }

        public void SendSuccess()
        {
            Finish();
            _presenter.DetachView();
            StartActivity(typeof(MainActivity));
        }

        public void ShowSnackBar(string message)
        {
            var view = FindViewById<View>(Resource.Id.register_layout);
            Snackbar.Make(view, message, Snackbar.LengthLong).Show();
        }

        public void ShowProgressBar()
        {
            throw new NotImplementedException();
        }

        public void HideProgressBar()
        {
            throw new NotImplementedException();
        }
    }
}