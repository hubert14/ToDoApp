using ToDoApp.Common.Models;
using ToDoApp.Interfaces.Views;

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

        public async void SendRegister()
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
            
            var user = new UserModel()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password
            };

            if (RegisterUser(user))
            {
                User = UserService.GetUser(user.Email);
                SaveLoggedUser(User.Email);

                _view.SendSuccess();
            }
            else _view.ShowSnackBar("Error while registering user. Please, try later");
        }

        private bool CheckEmailFree(string email)
        {
            var user = UserService.GetUser(email);
            return user.Email != email;
        }

        private bool CheckPasswordsEquals(string password, string confirmPassword)
        {
            return string.Equals(password, confirmPassword);
        }

        private bool RegisterUser(UserModel user)
        {
            var isCreated = UserService.Register(user);
            if (!isCreated) return false;

            User = UserService.GetUser(user.Email);
            return true;
        }
    }
}