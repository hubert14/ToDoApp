using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using ToDoApp.Activities.Interfaces;
using ToDoApp.Presenters;

namespace ToDoApp.Activities.Authorize
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class ForgotPasswordActivity : AppCompatActivity, IForgotPasswordView
    {
        private ForgotPasswordPresenter _presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_forgotPassword);

            _presenter = new ForgotPasswordPresenter(this);
            InitButtons();
        }

        private void InitButtons()
        {
            var confirmButton = FindViewById<Button>(Resource.Id.forgot_password_confirm_button);
            confirmButton.Click += (s, e) =>
            {
                HideKeyboard();
                _presenter.SendLinkRequest();
            };
        }

        private void HideKeyboard()
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
            imm.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
        }


        public string GetEmail()
        {
            var email = FindViewById<EditText>(Resource.Id.forgot_password_email);
            return email.Text;
        }

        public void ShowSnackBar(string message)
        {
            var view = FindViewById<View>(Resource.Id.forgot_password_layout);
            Snackbar.Make(view, message, Snackbar.LengthLong);
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