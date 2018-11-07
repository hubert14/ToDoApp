using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace ToDoApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class ForgotPasswordActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_forgotPassword);
            // Create your application here
        }
    }
}