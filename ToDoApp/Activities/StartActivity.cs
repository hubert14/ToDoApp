using Android.App;
using Android.OS;
using Android.Support.V7.App;
using ToDoApp.Activities.Authorize;
using ToDoApp.Presenters;

namespace ToDoApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class StartActivity : AppCompatActivity
    {
        private StartPresenter _presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _presenter = new StartPresenter(this);

            StartNextActivity();
        }

        private void StartNextActivity()
        {
            if (_presenter.CheckLoggedUser())
            {
                _presenter.InitUser();
                StartActivity(typeof(MainActivity));
            }
            else
            {
                StartActivity(typeof(LoginActivity));
            }
            Finish();
        }
    }
}