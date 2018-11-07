using Android.App;
using ToDoApp.Common.Models;
using ToDoApp.Common.Repositories;

namespace ToDoApp.Presenters
{
    public class BasePresenter
    {
        protected Activity Activity;
        protected static UserRepository Repository = new UserRepository();
        protected static User User;
    }
}