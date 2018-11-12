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
        protected static TaskService TaskService { get; } = new TaskService();
        protected static TaskListService TaskListService => new TaskListService(User.Id);
    }
}