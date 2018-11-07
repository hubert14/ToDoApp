using Android.App;
using Android.Content;
using ToDoApp.Activities;

namespace ToDoApp.Presenters
{
    public class StartPresenter : BasePresenter
    {
        private readonly ISharedPreferences _sp;

        public StartPresenter(Activity activity)
        {
            Activity = activity;
            _sp = activity.GetPreferences(FileCreationMode.Private);

            StartNextActivity();
        }

        private void StartNextActivity()
        {
            if (CheckLoggedUser())
            {
                InitUser();
                Activity.StartActivity(typeof(MainActivity));
            }
            else
            {
                Activity.StartActivity(typeof(LoginActivity));
            }
        }

        private bool CheckLoggedUser()
        {
            return _sp.Contains("loggedUser");
        }

        private void InitUser()
        {
            var email = _sp.GetString("loggedUser", string.Empty);
            User = Repository.GetUser(email);
        }
    }
}