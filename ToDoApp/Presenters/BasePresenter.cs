using Android.Content;
using ToDoApp.Common.Models;
using ToDoApp.Common.Repositories;

namespace ToDoApp.Presenters
{
    public abstract class BasePresenter
    {
        protected static ISharedPreferences SharedPreferences;
        protected static UserRepository UserRepository { get; private set; }
        protected static TaskListRepository TaskListRepository { get; private set; }

        protected static User User;

        protected static void InitRepositories()
        {
            UserRepository = new UserRepository();
            TaskListRepository = new TaskListRepository(User.Id);
        }
    }
}