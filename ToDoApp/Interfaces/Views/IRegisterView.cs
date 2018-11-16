using ToDoApp.Common.Models.Authorize;

namespace ToDoApp.Interfaces.Views
{
    public interface IRegisterView
    {
        RegisterRequestModel GetData();
        void SendSuccess();
        void ShowSnackBar(string message);
        void ShowProgressBar();
        void HideProgressBar();
    }
}