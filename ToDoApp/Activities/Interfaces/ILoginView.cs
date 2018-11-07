using ToDoApp.Models.Authorize;

namespace ToDoApp.Activities.Interfaces
{
    public interface ILoginView
    {
        LoginRequestModel GetData();
        void SendSuccess();
        void ShowSnackBar(string message);
        void ShowProgressBar();
        void HideProgressBar();
    }
}