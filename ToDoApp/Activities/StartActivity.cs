using Android.App;
using Android.OS;
using Android.Support.V7.App;
using ToDoApp.Presenters;

namespace ToDoApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class StartActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var presenter = new StartPresenter(this);
        }
    }
}