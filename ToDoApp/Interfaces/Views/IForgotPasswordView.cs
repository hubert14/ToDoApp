namespace ToDoApp.Interfaces.Views
{
    public interface IForgotPasswordView
    {
        string GetEmail();
        void ShowSnackBar(string message);
        void ShowSnackBar(int resId);
        void ShowProgressBar();
        void HideProgressBar();
    }
}