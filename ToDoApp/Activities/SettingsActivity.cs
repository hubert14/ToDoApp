using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ToDoApp.Common.Models;
using ToDoApp.Interfaces.Views;
using ToDoApp.Presenters;
using AlertDialog = Android.Support.V7.App.AlertDialog;

namespace ToDoApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class SettingsActivity : AppCompatActivity, ISettingsView
    {
        private SettingsPresenter _presenter;

        private TextView _headerEmail;
        private TextView _headerName;
        
        private EditText _firstName;
        private EditText _lastName;
        private Button _confirmButton;
        private Button _deleteButton;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(ToDoApp.Resources.Resource.Layout.activity_settings);
            Initialize();
        }

        private void Initialize()
        {
            InitFields();
            InitButtons();
            _presenter = new SettingsPresenter(this);
        }

        private void InitFields()
        {
            _firstName = FindViewById<EditText>(ToDoApp.Resources.Resource.Id.settings_firstName);
            _lastName = FindViewById<EditText>(ToDoApp.Resources.Resource.Id.settings_lastName);
        }

        private void InitButtons()
        {
            _confirmButton = FindViewById<Button>(ToDoApp.Resources.Resource.Id.settings_confirm_button);
            _deleteButton = FindViewById<Button>(ToDoApp.Resources.Resource.Id.settings_delete_button);

            _confirmButton.Click += (s,e) => { _presenter.EditUserRequest(); };
            _deleteButton.Click += (s,e) => { _presenter.DeleteUserRequest(); };
        }

        public void ShowDeleteUserAlert()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage("Are you sure? This action is irreversible");
            builder.SetPositiveButton("Yes", (sender, args) => { _presenter.DeleteUser(); });
            builder.SetNegativeButton("No", (sender, args) => { });
            builder.Create().Show();
        }

        public void ShowUserInfo(UserModel user)
        {
            _firstName.Text = user.FirstName;
            _lastName.Text = user.LastName;
        }

        public UserModel GetUserInfo()
        {
            var user = new UserModel()
            {
                FirstName =  _firstName.Text,
                LastName = _lastName.Text,
            };
            return user;
        }

        public void ShowMessage(string message)
        {
            var view = FindViewById<View>(ToDoApp.Resources.Resource.Id.settings_layout);
            Snackbar.Make(view, message, Snackbar.LengthLong).Show();
        }

        public void GoToLoginActivity()
        {
            FinishAffinity();
            StartActivity(typeof(LoginActivity));
        }
    }
}