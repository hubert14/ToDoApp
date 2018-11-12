using Android.Content;
using ToDoApp.Common.Models;
using ToDoApp.Common.Services;

namespace ToDoApp.Presenters
{
    public class BasePresenter
    {
        protected static ISharedPreferences SharedPreferences;
        protected static UserModel User;

        protected static UserService UserService { get; } = new UserService();
        protected static TaskService TaskService { get; } = new TaskService();
        protected static TaskListService TaskListService => new TaskListService(User.Id);

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