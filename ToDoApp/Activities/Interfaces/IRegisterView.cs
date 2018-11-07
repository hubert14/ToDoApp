using ToDoApp.Models.Authorize;

namespace ToDoApp.Activities.Interfaces
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