using Android.Content;
using ToDoApp.Common.Models;
using ToDoApp.Common.Repositories;

namespace ToDoApp.Presenters
{
    public abstract class BasePresenter
    {
        protected static ISharedPreferences SharedPreferences;
        protected static readonly UserRepository Repository = new UserRepository();
        protected static User User;
    }
}