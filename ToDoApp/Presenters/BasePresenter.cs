using Android.Content;
using ToDoApp.Common.Models;
using ToDoApp.Common.Services;

namespace ToDoApp.Presenters
{
    public class BasePresenter
    {
        protected static ISharedPreferences SharedPreferences;

        private static UserModel _user;
        protected static UserModel User
        {
            get => _user;
            set
            {
                _user = value;
                UserService = new UserService();
                TaskListService = new TaskListService(value.Id);
                TaskService = new TaskService();
            }
        }

        protected static UserService UserService { get; private set; } = new UserService();

        protected static TaskService TaskService { get; private set; }
        protected static TaskListService TaskListService { get; private set; }

        protected static void SaveLoggedUser(string email)
        {
            SharedPreferences.Edit().Remove("loggedUser").Apply();
            SharedPreferences.Edit().PutString("loggedUser", email).Apply();
        }

        protected static string GetLoggedUser()
        {
            return SharedPreferences.GetString("loggedUser", string.Empty);
        }

        protected static void LogOutUser()
        {
            SharedPreferences.Edit().Remove("loggedUser").Apply();
        }
    }
}