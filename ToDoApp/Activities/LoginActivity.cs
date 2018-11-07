using Android.App;
using Android.OS;
using Android.Support.V7.App;
using ToDoApp.Presenters;

namespace ToDoApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class LoginActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);
            var presenter = new LoginPresenter(this);
        }
    }
}