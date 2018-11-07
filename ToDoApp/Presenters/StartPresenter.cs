using Android.App;
using Android.Content;

namespace ToDoApp.Presenters
{
    public class StartPresenter : BasePresenter
    {
        public StartPresenter(Activity activity)
        {
            SharedPreferences = activity.GetPreferences(FileCreationMode.Private);
        }

        public bool CheckLoggedUser()
        {
            return SharedPreferences.Contains("loggedUser");
        }

        public void InitUser()
        {
            var email = SharedPreferences.GetString("loggedUser", string.Empty);
            User = Repository.GetUser(email);
        }
    }
}