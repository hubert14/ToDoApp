using Android.App;
using Android.OS;
using Android.Support.V7.App;
using ToDoApp.Presenters;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace ToDoApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class StartActivity : AppCompatActivity
    {
        private StartPresenter _presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCenter.Start("dfaba3e4-1876-4b13-b2dc-bad81b1c64f5",
                typeof(Analytics), typeof(Crashes));
            
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