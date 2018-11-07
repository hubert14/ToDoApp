using System;
using Android.App;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using ToDoApp.Activities;
using Snackbar = Android.Support.Design.Widget.Snackbar;

namespace ToDoApp.Presenters
{
    public class LoginPresenter : BasePresenter
    {
        public LoginPresenter(Activity activity)
        {
            Activity = activity;
            InitButtons();
        }

        private void InitButtons()
        {
            var loginButton = Activity.FindViewById<Button>(Resource.Id.login_login_button);
            var forgotPasswordButton = Activity.FindViewById<Button>(Resource.Id.login_forgotPassword_button);
            var registerButton = Activity.FindViewById<Button>(Resource.Id.login_register_button);

            loginButton.Click += Login;
            forgotPasswordButton.Click += GoToForgotPasswordPage;
            registerButton.Click += GoToRegister;
        }

        private void GoToRegister(object sender, EventArgs eventArgs)
        {
            Activity.StartActivity(typeof(RegisterActivity));
        }

        private void GoToForgotPasswordPage(object sender, EventArgs eventArgs)
        {
            Activity.StartActivity(typeof(ForgotPasswordActivity));
        }

        private void Login(object sender, EventArgs eventArgs)
        {
            var email = Activity.FindViewById<EditText>(Resource.Id.login_email);
            var password = Activity.FindViewById<EditText>(Resource.Id.login_password);

            var user = Repository.Login(email.Text, password.Text);

            if (user != null)
            {
                User = user;
                Activity.StartActivity(typeof(MainActivity));
            }
            else
            {
                //TODO: Display snackbar invalid email or password
            }
        }
    }
}