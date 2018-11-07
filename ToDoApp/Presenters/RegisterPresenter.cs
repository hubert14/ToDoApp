using ToDoApp.Activities.Authorize;
using ToDoApp.Activities.Interfaces;
using ToDoApp.Common.Models;

namespace ToDoApp.Presenters
{
    public class RegisterPresenter : BasePresenter
    {
        private IRegisterView _view;

        public RegisterPresenter(IRegisterView view)
        {
            _view = view;
        }

        public void DetachView()
        {
            _view = null;
        }

        public void SendRegister()
        {
            var model = _view.GetData();

            if(!CheckPasswordsEquals(model.Password, model.ConfirmPassword))
            {
                _view.ShowSnackBar("Passwords do not match");
                return;
            }

            if(!CheckEmailFree(model.Email))
            {
                _view.ShowSnackBar("User with this email was already exists");
                return;
            }
            
            var user = new User()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password
            };

            if (RegisterUser(user))
            {
                User = UserRepository.GetUser(user.Email);
                SharedPreferences.Edit().PutString("loggedUser", user.Email).Apply(); ;

                _view.SendSuccess();
            }
            else _view.ShowSnackBar("Error while registering user. Please, try later");
        }

        private bool CheckEmailFree(string email)
        {
            var user = UserRepository.GetUser(email);
            return user == null;
        }

        private bool CheckPasswordsEquals(string password, string confirmPassword)
        {
            return string.Equals(password, confirmPassword);
        }

        private bool RegisterUser(User user)
        {
            var isCreated = UserRepository.CreateUser(user);
            if (!isCreated) return false;

            User = UserRepository.GetUser(user.Email);
            return true;
        }
    }
}