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
            return !string.IsNullOrEmpty(GetLoggedUser());
        }

        public void InitUser()
        {
            var email = GetLoggedUser();
            User = UserService.GetUser(email);
        }
    }
}