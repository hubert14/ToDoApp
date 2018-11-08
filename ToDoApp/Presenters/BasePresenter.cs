using Android.Content;
using ToDoApp.Common.Models;
using ToDoApp.Common.Services;

namespace ToDoApp.Presenters
{
    public abstract class BasePresenter
    {
        protected static ISharedPreferences SharedPreferences;
        protected static UserModel User;

        protected static UserService UserService { get; } = new UserService();
        protected static TaskListService TaskListService => new TaskListService(User.Id);
        protected static TaskService TaskService => new TaskService();
        
    }
}