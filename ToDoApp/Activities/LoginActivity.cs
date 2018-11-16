using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using ToDoApp.Common.Models.Authorize;
using ToDoApp.Interfaces.Views;
using ToDoApp.Presenters;

namespace ToDoApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class LoginActivity : AppCompatActivity, ILoginView
    {
        private ProgressBar _progress;
        private LoginPresenter _presenter;
        private EditText _email;
        private EditText _password;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);

            Initialize();
        }

        private void Initialize()
        {
            InitFields();
            InitButtons();

            _progress = FindViewById<ProgressBar>(Resource.Id.login_progress_bar);
            _presenter = new LoginPresenter(this);
        }

        private void InitFields()
        {
            _email = FindViewById<EditText>(Resource.Id.login_email);

            _password = FindViewById<EditText>(Resource.Id.login_password);
            _password.EditorAction += (s,e) => {
                if (e.ActionId == ImeAction.Done)
                {
                    Login(this, EventArgs.Empty);
                }
            };
        }

        private void InitButtons()
        {
            var loginButton = FindViewById<Button>(Resource.Id.login_login_button);
            var forgotPasswordButton = FindViewById<Button>(Resource.Id.login_forgotPassword_button);
            var registerButton = FindViewById<Button>(Resource.Id.login_register_button);

            loginButton.Click += Login;
            forgotPasswordButton.Click += (s, e) => { StartActivity(typeof(ForgotPasswordActivity)); };
            registerButton.Click += (s,e) => { StartActivity(typeof(RegisterActivity)); };
        }

        private void Login(object sender, EventArgs eventArgs)
        {
            HideKeyboard();
            _presenter.SendLogin();
        }

        private void HideKeyboard()
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
            imm.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
        }


        public LoginRequestModel GetData()
        {
            return new LoginRequestModel()
            {
                Email = _email.Text,
                Password = _password.Text
            };
        }

        public void SendSuccess()
        {
            Finish();
            _presenter.DetachView();
            StartActivity(typeof(MainActivity));
            Dispose();
        }

        public void ShowSnackBar(string message)
        {
            var view = FindViewById<View>(Resource.Id.login_layout);
            Snackbar.Make(view, message, Snackbar.LengthLong).Show();
        }

        public void ShowSnackBar(int resId)
        {
            var view = FindViewById<View>(Resource.Id.login_layout);
            Snackbar.Make(view, resId, Snackbar.LengthLong).Show();
        }

        public void ShowProgressBar()
        {
            _progress.Visibility = ViewStates.Visible;
        }

        public void HideProgressBar()
        {
            _progress.Visibility = ViewStates.Gone;
        }
    }
}