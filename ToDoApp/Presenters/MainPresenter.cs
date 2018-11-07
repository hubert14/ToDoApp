using Android.App;
using ToDoApp.Activities.Interfaces;

namespace ToDoApp.Presenters
{
    public class MainPresenter : BasePresenter
    {
        private readonly IMainView _view;

        public MainPresenter(IMainView view)
        {
            _view = view;
        }
        
        public void GetUserInfo()
        {
            _view.SendUserInfo(User.Email, User.FirstName + " " + User.LastName);
        }
    }
}