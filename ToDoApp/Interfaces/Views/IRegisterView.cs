using ToDoApp.Common.Models.Authorize;

namespace ToDoApp.Interfaces.Views
{
    public interface IRegisterView
    {
        RegisterRequestModel GetData();
        void SendSuccess();
        void ShowSnackBar(string message);
        void ShowSnackBar(int resId);
        void ShowProgressBar();
        void HideProgressBar();
    }
}