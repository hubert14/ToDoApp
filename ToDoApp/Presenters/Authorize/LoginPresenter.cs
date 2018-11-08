using ToDoApp.Activities.Interfaces;
using ToDoApp.Common.Models;

namespace ToDoApp.Presenters.Authorize
{
    public class LoginPresenter : BasePresenter
    {
        private ILoginView _view;

        public LoginPresenter(ILoginView view)
        {
            _view = view;
        }

        public void DetachView()
        {
            _view = null;
        }

        public void SendLogin()
        {
            var model = _view.GetData();

            var user = Login(model.Email, model.Password);

            if (user == null)
            {
                _view.ShowSnackBar("Invalid password or email");
                return;
            }

            User = user;
            var email = user.Email;
            SharedPreferences.Edit().PutString("loggedUser", email).Apply();

            _view.SendSuccess();
        }

        private UserModel Login(string email, string password)
        {
            return UserService.Login(email, password);
            
        }
    }
}