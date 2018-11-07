namespace ToDoApp.Activities.Interfaces
{
    public interface IForgotPasswordView
    {
        string GetEmail();
        void ShowSnackBar(string message);
        void ShowProgressBar();
        void HideProgressBar();
    }
}