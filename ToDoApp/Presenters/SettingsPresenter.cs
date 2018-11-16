using ToDoApp.Interfaces.Views;

namespace ToDoApp.Presenters
{
    public class SettingsPresenter : BasePresenter
    {
        private readonly ISettingsView _view;

        public SettingsPresenter(ISettingsView view)
        {
            _view = view;
            _view.ShowUserInfo(User);
        }

        public void DeleteUserRequest()
        {
            _view.ShowDeleteUserAlert();
        }

        public void EditUserRequest()
        {
            var user = _view.GetUserInfo();

            User.FirstName = user.FirstName;
            User.LastName = user.LastName;

            UserService.UpdateUser(User);

            _view.ShowMessage("User info was successfully changed");
            _view.ShowUserInfo(User);
        }

        public void DeleteUser()
        {
            UserService.DeleteUser(User);
            User = null;
            _view.GoToLoginActivity();
            LogOutUser();
        }
    }
}