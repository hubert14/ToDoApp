using Android.App;
using ToDoApp.Activities;
using ToDoApp.Common.Models;

namespace ToDoApp.Presenters
{
    public class RegisterPresenter : BasePresenter
    {
        public RegisterPresenter(Activity activity)
        {
            Activity = activity;
        }

        private void RegisterUser(User user)
        {
            var isCreated = Repository.CreateUser(user);
            if (!isCreated) return;

            User = Repository.GetUser(user.Email);
            StartMainActivity();
        }

        private void StartMainActivity()
        {
            Activity.Finish();
            Activity.StartActivity(typeof(MainActivity));
        }
    }
}