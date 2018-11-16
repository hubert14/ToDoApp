using ToDoApp.Common.Models;

namespace ToDoApp.Interfaces.Views
{
    public interface ISettingsView
    {
        void ShowDeleteUserAlert();
        void ShowUserInfo(UserModel user);
        UserModel GetUserInfo();
        void ShowMessage(string message);
        void ShowMessage(int resId);
        void GoToLoginActivity();
    }
}